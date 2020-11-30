using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sprint.Data;
using Sprint.Enums;
using Sprint.Models;
using Sprint.ViewModels;

namespace Sprint.Controllers
{
    public class OrderDetails
    {
        public List<OrderDetailsItem> OrderItems { get; set; }
        public int? ShippingId { get; set; }
        public int? BillingId { get; set; }
        public int? WalletId { get; set; }
        public bool? SameAsShippingAddress { get; set; }
        public decimal ItemsTotalPrice { get; set; }
    }

    public class OrderDetailsItem
    {
        public int CartGameId { get; set; }
        public bool ShipItem { get; set; }
    }

    [Authorize(Roles = "Admin,Member")]
    public class CheckoutController : Controller
    {
        public const decimal INDIVIDUAL_SHIPPING_COST = 4.40m; // TODO: this is a fixed cost for practical example
        public const decimal TAX_PERCENT = 0.13m; // TODO: this is a fixed cost for practical example

        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public CheckoutController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Checkout
        [HttpGet, ActionName("Index")]
        public async Task<IActionResult> Checkout()
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            DateTime now = DateTime.Now;

            // cart
            var cart = new CartViewModel
            {
                Items = await _context.CartGames
                    .Include(c => c.Game)
                    .Include(c => c.ReceivingUser)
                    .Where(c => c.CartUserId == user.Id)
                    .Select(c => new CartItemViewModel
                    {
                        CartItem = c,

                        Image = c.Game.GameImages.FirstOrDefault(i => i.ImageType == ImageType.Banner),

                        Discount = c.Game.Discounts.Where(d => d.DiscountPrice < c.Game.RegularPrice && d.DiscountStart <= now && d.DiscountFinish > now)
                            .OrderBy(d => d.DiscountPrice)
                            .FirstOrDefault(),
                    })
                    .ToListAsync(),
                User = user,
            };

            if (!cart.Items.Any())
            {
                return RedirectToAction(nameof(CartController.Index), "Cart");
            }

            // checkout
            var checkoutOrder = new CheckoutViewModel
            {
                CartCheckout = cart,

                ItemsTotalPrice = cart.Items.Sum(i => i.Discount?.DiscountPrice ?? i.CartItem.Game.RegularPrice),

                TaxPercent = TAX_PERCENT,

                IndividualShippingCost = INDIVIDUAL_SHIPPING_COST,

                Addresses = await _context.Address
                    .Where(a => a.UserId == user.Id)
                    .ToListAsync(),

                WalletCreditCards = await _context.Wallet
                    .Where(w => w.UserId == user.Id)
                    .ToListAsync(),
            };

            return View(checkoutOrder);
        }

        // POST: Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(OrderDetails orderDetails)
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            // wallet verify
            Wallet wallet = await _context.Wallet
                .FirstOrDefaultAsync(w => w.WalletId == orderDetails.WalletId && w.UserId == user.Id);

            if (wallet == null)
            {
                TempData["CheckoutAlert"] = "Payment type is required.";
                return RedirectToAction(nameof(Index));
            }

            // does order contain shipped items
            int orderShippedItemsCount = orderDetails.OrderItems.Count(i => i.ShipItem);

            // address verify
            Address shippingAddress = orderShippedItemsCount > 0
                ? await _context.Address
                    .FirstOrDefaultAsync(a => a.AddressId == orderDetails.ShippingId && a.UserId == user.Id)
                : default;

            if (orderShippedItemsCount > 0 && shippingAddress == null)
            {
                TempData["CheckoutAlert"] = "Shipping address is required to ship a game.";
                return RedirectToAction(nameof(Index));
            }

            // address verify
            Address billingAddress = orderShippedItemsCount > 0 && orderDetails.SameAsShippingAddress != true 
                ? await _context.Address
                    .FirstOrDefaultAsync(a => a.AddressId == orderDetails.BillingId && a.UserId == user.Id)
                : shippingAddress;

            if (orderShippedItemsCount > 0 && billingAddress == null)
            {
                TempData["CheckoutAlert"] = "Billing address is required.";
                return RedirectToAction(nameof(Index));
            }

            var cartGamesCheckedOut = orderDetails.OrderItems
                .Select(i => i.CartGameId)
                .ToHashSet();

            DateTime now = DateTime.Now;

            // order items
            var cartOrder = new CartViewModel
            {
                Items = await _context.CartGames
                    .Include(c => c.Game)
                    .Include(c => c.ReceivingUser)
                    .Where(c => cartGamesCheckedOut.Contains(c.CartGameId))
                    .Select(c => new CartItemViewModel
                    {
                        CartItem = c,

                        Discount = c.Game.Discounts.Where(d => d.DiscountPrice < c.Game.RegularPrice && d.DiscountStart <= now && d.DiscountFinish > now)
                            .OrderBy(d => d.DiscountPrice)
                            .FirstOrDefault(),
                    })
                    .ToListAsync(),
            };

            if (cartOrder.Items.Any(i => i.CartItem.CartUserId != user.Id))
            {
                return BadRequest(); // this should never happen unless malicious attempt or some type of error in server
            }

            // ensure current total matches original - ie: a game special price either ended/started in the time since the checkout process started
            decimal itemsTotalPrice = cartOrder.Items.Sum(i => i.Discount?.DiscountPrice ?? i.CartItem.Game.RegularPrice);
            if (itemsTotalPrice != orderDetails.ItemsTotalPrice)
            {
                TempData["CheckoutAlert"] = "Checkout price currently differs from original. Please reconfirm order.";
                return RedirectToAction(nameof(Index));
            }

            // get cart items
            var items = cartOrder.Items
                .Where(i => orderDetails.OrderItems.Any(o => o.CartGameId == i.CartItem.CartGameId))
                .Select(v => v.CartItem);

            decimal shippingHandlingAmount = orderShippedItemsCount * INDIVIDUAL_SHIPPING_COST;
            decimal taxAmount = (itemsTotalPrice + orderShippedItemsCount) * TAX_PERCENT;

            var order = new Order
            {
                UserId = user.Id,
                OrderNumber = Guid.NewGuid().ToString(),
                WalletId = orderDetails.WalletId.Value,
                OrderItemsAmount = itemsTotalPrice,
                OrderTaxAmount = taxAmount,
                OrderShippingHandlingAmount = shippingHandlingAmount,
                ShippingAddressId = shippingAddress?.AddressId ?? default(int?),
                BillingAddressId = billingAddress?.AddressId ?? default(int?),
                OrderDate = now,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // add order items
            var orderItems = items.Select(i => new OrderItem
            {
                OrderId = order.OrderId,
                GameId = i.GameId,
                OwnerUserId = i.ReceivingUserId,
                ItemNumber = Guid.NewGuid().ToString(),
                PhysicallyOwned = orderDetails.OrderItems.Any(o => o.ShipItem && o.CartGameId == i.CartGameId),
            });

            // remove cart items & add order items
            _context.CartGames.RemoveRange(items);
            _context.OrderItems.AddRange(orderItems);

            await _context.SaveChangesAsync();

            TempData["CheckoutSuccess"] = "Your order has been successfully processed.";
            return RedirectToAction(nameof(Confirmation), new { orderNumber = order.OrderNumber });
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(string orderNumber)
        {
            if (orderNumber == null)
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.OwnerUser)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Game)
                        .ThenInclude(g => g.GameImages)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber && o.UserId == user.Id);

            if (order == null)
            {
                return NotFound();
            }

            var library = order.OrderItems
                .Where(o => o.OwnerUserId == user.Id)
                .Select(o => new ConfirmationItemViewModel
                {
                    Game = o.Game,

                    Image = o.Game.GameImages.FirstOrDefault(i => i.ImageType == ImageType.Banner),

                    PhysicallyOwned = o.PhysicallyOwned,

                    ItemNumber = o.ItemNumber,

                    GiftedUser = o.OwnerUserId != user.Id ? o.OwnerUser : default,
                })
                .ToList();

            return View(library);
        }

        [HttpGet]
        public async Task<IActionResult> Download(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            OrderItem item = await _context.OrderItems
                .Include(o => o.Game)
                .FirstOrDefaultAsync(o => o.ItemNumber == id && o.OwnerUserId == user.Id && !o.PhysicallyOwned);

            if (item == null)
            {
                return NotFound();
            }

            var invalidChars = Path.GetInvalidFileNameChars().ToHashSet();

            // clean string - using Game's Name
            string name = item.Game.Name;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                if (invalidChars.Contains(name[i]))
                {
                    name = name.Remove(i, 1);
                }
            }

            // mock a file download with a png
            var path = Path.GetFullPath("./File/static-game-asset.png");

            return PhysicalFile(path, "image/png", $"{name}.png");
        }
    }
}
