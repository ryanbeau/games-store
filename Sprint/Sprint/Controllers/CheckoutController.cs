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
            public List<int> CartGameIds { get; set; }
            public decimal IndividualShippingCost { get; set; }
            public decimal TotalBeforeTax { get; set; }
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
        public async Task<IActionResult> Order([FromBody] OrderDetails orderDetails)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
