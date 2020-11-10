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
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            DateTime now = DateTime.Now;

            var wishlistGames = await _context.UserGameWishlists
                .Include(w => w.Game)
                .Where(w => w.UserId == user.Id)
                .Select(w => new WishlistItemViewModel
                {
                    // image
                    Image = w.Game.GameImages.FirstOrDefault(i => i.GameId == w.GameId && i.ImageType == ImageType.Banner),
                    // discount
                    Discount = w.Game.Discounts.Where(d => d.DiscountPrice < w.Game.RegularPrice && d.DiscountStart <= now && d.DiscountFinish > now)
                        .OrderBy(d => d.DiscountPrice)
                        .FirstOrDefault(),
                    IsInCart = _context.CartGames.Any(c => c.GameId == w.Game.GameId && c.CartUserId == user.Id && c.ReceivingUserId == user.Id),
                    WishlistItem = w,
                })
                .ToListAsync();

            return View(new WishlistViewModel
            {
                User = user,
                WishlistVisibility = user.WishlistVisibility,
                Games = wishlistGames
            });
        }

        // GET: Wishlist
        [HttpGet, Route("Wishlist/{username}"), ActionName("Index")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> UserWishlist(string username)
        {
            User user = await _userManager.GetUserAsync(User);

            // if viewing your own page - redirect to index
            if (username == user.UserName)
            {
                return RedirectToAction(nameof(Index));
            }

            User wishlistUser = await _userManager.FindByNameAsync(username);
            if (wishlistUser == null)
            {
                return NotFound();
            }

            // redirect if private wishlist
            if (wishlistUser.WishlistVisibility == WishlistVisibility.OnlyMe)
            {
                return RedirectToAction("Index", "Friends"); // somehow we got here - redirect
            }

            // redirect if friends-only wishlist & we are either blocked or not friends
            if (wishlistUser.WishlistVisibility == WishlistVisibility.FriendsOnly)
            {
                bool areFriendsOrPending = await _context.UserRelationships
                    .AnyAsync(r => r.Type != Relationship.Blocked && r.RelatingUserId == wishlistUser.Id && r.RelatedUserId == user.Id);

                if (!areFriendsOrPending)
                {
                    return RedirectToAction("Index", "Friends"); // somehow we got here - redirect
                }
            }

            var wishlistGames = await _context.UserGameWishlists
                .Include(w => w.Game)
                .Where(w => w.UserId == wishlistUser.Id)
                .Select(w => new WishlistItemViewModel
                {
                    Image = _context.GameImages.FirstOrDefault(i => i.GameId == w.GameId && i.ImageType == ImageType.Banner),
                    IsInCart = _context.CartGames.Any(c => c.GameId == w.Game.GameId && c.CartUserId == user.Id && c.ReceivingUserId == wishlistUser.Id),
                    WishlistItem = w,
                })
                .ToListAsync();

            return View("Index", new WishlistViewModel
            {
                User = wishlistUser,
                WishlistVisibility = wishlistUser.WishlistVisibility,
                Games = wishlistGames
            });
        }

        // GET: Wishlist/Edit -- redirect user
        public IActionResult Edit()
        {
            return RedirectToAction("Index");
        }

        // POST: Wishlist/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Edit([FromForm] WishlistVisibility wishlistVisibility)
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            user.WishlistVisibility = wishlistVisibility;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Wishlist/Add -- redirect user
        [HttpGet]
        public IActionResult Add(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Wishlist/Add/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Add(int? gameId, string returnUrl)
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

            bool wishlisted = await _context.UserGameWishlists.AnyAsync(w => w.GameId == gameId && w.UserId == user.Id);
            if (!wishlisted)
            {
                _context.UserGameWishlists.Add(new UserGameWishlist { GameId = gameId.Value, UserId = user.Id, AddedOn = DateTime.Now });
                await _context.SaveChangesAsync();
            }

            TempData["WishlistAdded"] = $"Added {game.Name} to wishlist";

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Game");
        }

        // GET: Wishlist/Remove -- redirect user
        [HttpGet]
        public IActionResult Remove(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Wishlist/Remove/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Remove(int gameId, string returnUrl)
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            var userGameWishlist = await _context.UserGameWishlists.FirstOrDefaultAsync(w => w.GameId == gameId && w.UserId == user.Id);

            if (userGameWishlist == null) {
                return NotFound();
            }

            _context.UserGameWishlists.Remove(userGameWishlist);

            await _context.SaveChangesAsync();

            Game game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
            if (game != null)
            {
                TempData["WishlistRemoved"] = $"Removed {game.Name} from wishlist";
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
