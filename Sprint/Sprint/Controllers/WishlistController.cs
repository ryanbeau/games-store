using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint.Data;
using Sprint.Models;

namespace Sprint.Controllers
{
    public class WishlistController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public WishlistController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Wishlist
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            var applicationDbContext = await _context.UserGameWishlist
                .Include(w => w.Game)
                .Where(w => w.UserId == user.Id)
                .ToListAsync();
            return View(applicationDbContext);
        }

        // POST: Wishlist/Add/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int? gameId)
        {
            if (gameId == null)
            {
                return NotFound();
            }

            bool gameExists = await _context.Games.AnyAsync(g => g.GameId == gameId);
            if (!gameExists)
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            bool wishlisted = await _context.UserGameWishlist.AnyAsync(w => w.GameId == gameId && w.UserId == user.Id);
            if (!wishlisted)
            {
                _context.UserGameWishlist.Add(new UserGameWishlist { GameId = gameId.Value, UserId = user.Id, AddedOn = DateTime.Now });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Wishlist/Remove/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int gameId)
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            var userGameWishlist = await _context.UserGameWishlist.FirstOrDefaultAsync(w => w.GameId == gameId && w.UserId == user.Id);
            _context.UserGameWishlist.Remove(userGameWishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
