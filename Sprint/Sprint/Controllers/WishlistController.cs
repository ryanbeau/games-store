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

namespace Sprint.Controllers
{
    public class WishlistController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public WishlistController(SignInManager<User> signInManager, UserManager<User> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
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

            var wishlistGames = await _context.UserGameWishlist
                .Include(w => w.Game)
                .Where(w => w.UserId == user.Id)
                .ToListAsync();

            return View(new Wishlist
            {
                Authorized = true,
                Username = User.Identity.Name,
                WishlistVisibility = user.WishlistVisibility,
                Games = wishlistGames
            });
        }

        // GET: Wishlist
        [HttpGet("Wishlist/{username}")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> UserWishlist(string username)
        {
            // if viewing your own page - redirect to index
            if (username == User.Identity.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            User wishlistUser = await _signInManager.UserManager.FindByNameAsync(username);
            if (wishlistUser == null)
            {
                return NotFound();
            }

            var wishlistGames = await _context.UserGameWishlist
                .Include(w => w.Game)
                .Where(w => w.UserId == wishlistUser.Id)
                .ToListAsync();

            return View("Index", new Wishlist
            {
                Authorized = false,
                Username = wishlistUser.Name,
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

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
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

            var userGameWishlist = await _context.UserGameWishlist.FirstOrDefaultAsync(w => w.GameId == gameId && w.UserId == user.Id);
            _context.UserGameWishlist.Remove(userGameWishlist);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
