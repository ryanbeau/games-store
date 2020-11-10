using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint.Data;
using Sprint.Enums;
using Sprint.Models;
using Sprint.ViewModels;

namespace Sprint.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class CartController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public CartController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            DateTime now = DateTime.Now;

            var cart = new CartViewModel
            {
                Items = await _context.CartGames
                    .Include(c => c.Game)
                    .Include(c => c.ReceivingUser)
                    .Where(c => c.CartUserId == user.Id)
                    .Select(c => new CartItemViewModel
                    {
                        // image
                        Image = c.Game.GameImages.FirstOrDefault(i => i.GameId == c.GameId && i.ImageType == ImageType.Banner),
                        // discount
                        Discount = c.Game.Discounts.Where(d => d.DiscountPrice < c.Game.RegularPrice && d.DiscountStart <= now && d.DiscountFinish > now)
                            .OrderBy(d => d.DiscountPrice)
                            .FirstOrDefault(),
                        CartItem = c,
                    })
                    .ToListAsync(),
                User = user,
            };

            cart.ContainsGiftItem = cart.Items
                .Any(i => i.CartItem.ReceivingUserId != user.Id);

            return View(cart);
        }

        // POST: Cart/Add/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int? gameId, int? recipientUserId, string returnUrl)
        {
            if (gameId == null)
            {
                return NotFound();
            }

            Game game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
            if (game == null)
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            // if null - item is for this User (aka: not a gift)
            if (recipientUserId == null)
            {
                recipientUserId = user.Id;
            }

            // add to cart if not already in cart
            bool isInCart = await _context.CartGames
                .AnyAsync(w => w.GameId == gameId && w.CartUserId == user.Id && w.ReceivingUserId == recipientUserId.Value);

            if (!isInCart)
            {
                _context.CartGames.Add(new CartGame 
                { 
                    GameId = gameId.Value, 
                    CartUserId = user.Id, 
                    ReceivingUserId = recipientUserId.Value,
                    AddedOn = DateTime.Now 
                });
                await _context.SaveChangesAsync();
            }

            TempData["CartAdded"] = $"Added {game.Name} to cart";

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/Remove/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int gameId, int? recipientUserId, string returnUrl)
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            // if null - item is for this User (aka: not a gift)
            if (recipientUserId == null)
            {
                recipientUserId = user.Id;
            }

            var cartGame = await _context.CartGames
                .FirstOrDefaultAsync(w => w.GameId == gameId && w.ReceivingUserId == recipientUserId.Value && w.CartUserId == user.Id);

            if (cartGame != null)
            {
                _context.CartGames.Remove(cartGame);
                await _context.SaveChangesAsync();
            }

            Game game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
            if (game != null)
            {
                TempData["CartRemoved"] = $"Removed {game.Name} from cart";
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
