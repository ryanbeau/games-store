using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sprint.Data;
using Sprint.Enums;
using Sprint.Models;
using Sprint.ViewModels;

namespace Sprint.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class CheckoutController : Controller
    {
        private const decimal INDIVIDUAL_SHIPPING_COST = 4.40m; // TODO: this is a fixed cost for practical example

        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public CheckoutController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public class OrderDetails
        {
            public List<OrderItems> OrderItems { get; set; }
            public int? ShippingId { get; set; }
            public int? BillingId { get; set; }
            public int? WalletId { get; set; }
            public bool? SameAsShippingAddress { get; set; }
            public decimal ItemsTotalPrice { get; set; }
        }

        public class OrderItems
        {
            public int CartGameId { get; set; }
            public bool ShipItem { get; set; }
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

            if (cart.Items.Any(i => i.CartItem.CartUserId != user.Id))
            {
                return BadRequest(); //this should never happen unless a Cart Item is added maliciously or by server error?
            }

            // checkout
            var checkoutOrder = new CheckoutViewModel
            {
                CartCheckout = cart,

                ItemsTotalPrice = cart.Items.Sum(i => i.Discount?.DiscountPrice ?? i.CartItem.Game.RegularPrice),

                TaxPercent = 0.13m,

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
            bool orderContainsShippedItems = orderDetails.OrderItems.Any(i => i.ShipItem);

            // address verify
            Address shippingAddress = orderContainsShippedItems 
                ? await _context.Address
                    .FirstOrDefaultAsync(a => a.AddressId == orderDetails.ShippingId && a.UserId == user.Id)
                : default;

            if (orderContainsShippedItems && shippingAddress == null)
            {
                TempData["CheckoutAlert"] = "Shipping address is required to ship a game.";
                return RedirectToAction(nameof(Index));
            }

            // address verify
            Address billingAddress = orderContainsShippedItems && orderDetails.SameAsShippingAddress != true 
                ? await _context.Address
                    .FirstOrDefaultAsync(a => a.AddressId == orderDetails.BillingId && a.UserId == user.Id)
                : shippingAddress;

            if (orderContainsShippedItems && billingAddress == null)
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

            var order = new Order
            {
                UserId = user.Id,
                OrderNumber = Guid.NewGuid().ToString(),
                WalletId = orderDetails.WalletId.Value,
                ShippingAddressId = orderContainsShippedItems ? shippingAddress.AddressId : default(int?),
                BillingAddressId = orderContainsShippedItems ? billingAddress.AddressId : default(int?),
                OrderDate = now,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // add order items
            var orderItems = items.Select(i => new OrderItem
            {
                OrderId = order.OrderId,
                OwnerUserId = i.ReceivingUserId,
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
        public IActionResult Confirmation(string orderNumber)
        {
            return View();
        }
    }
}
