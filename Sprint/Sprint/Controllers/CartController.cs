using System;
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
    public class CartController : Controller
    {
        public const string ADD_FRIENDS = "Add friends to gift item.";

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
                        CartItem = c,
                        
                        Image = c.Game.GameImages.FirstOrDefault(i => i.ImageType == ImageType.Banner),
                        
                        Discount = c.Game.Discounts.Where(d => d.DiscountPrice < c.Game.RegularPrice && d.DiscountStart <= now && d.DiscountFinish > now)
                            .OrderBy(d => d.DiscountPrice)
                            .FirstOrDefault(),
                    })
                    .ToListAsync(),
                User = user,
            };

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

            User recipientUser;

            // if null - item is for this User (aka: not a gift)
            if (recipientUserId == null || recipientUserId == user.Id)
            {
                recipientUserId = user.Id;
                recipientUser = user;
            } 
            else
            {
                recipientUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == recipientUserId);
            }

            if (recipientUser == null)
            {
                return NotFound();
            }

            // add to cart if not already in cart
            bool isInCart = await _context.CartGames
                .AnyAsync(w => w.GameId == gameId && w.CartUserId == user.Id && w.ReceivingUserId == recipientUser.Id);

            if (!isInCart)
            {
                _context.CartGames.Add(new CartGame
                {
                    GameId = gameId.Value,
                    CartUserId = user.Id,
                    ReceivingUserId = recipientUser.Id,
                    AddedOn = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }

            TempData["CartAdded"] = $"{game.Name} was added to cart";

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

            User recipientUser;

            // if null - item is for this User (aka: not a gift)
            if (recipientUserId == null || recipientUserId == user.Id)
            {
                recipientUserId = user.Id;
                recipientUser = user;
            }
            else
            {
                recipientUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == recipientUserId);
            }

            if (recipientUser == null)
            {
                return NotFound();
            }

            var cartGame = await _context.CartGames
                .FirstOrDefaultAsync(w => w.GameId == gameId && w.ReceivingUserId == recipientUser.Id && w.CartUserId == user.Id);

            if (cartGame != null)
            {
                _context.CartGames.Remove(cartGame);
                await _context.SaveChangesAsync();
            }

            Game game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
            if (game != null)
            {
                TempData["CartRemoved"] = $"{game.Name} was removed from cart";
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        //GET: Cart/Gift/5
        [HttpGet]
        public async Task<IActionResult> Gift(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CartGame cartItem = await _context.CartGames
                .Include(c => c.Game)
                .FirstOrDefaultAsync(c => c.CartGameId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            var relationships = await _context.UserRelationships
                .Include(r => r.RelatedUser)
                .Where(r => r.RelatingUserId == user.Id && r.Type == Relationship.Friend && 
                    !r.RelatedUser.ReceivingCartItems.Any(c => c.GameId == cartItem.GameId && c.CartUserId == r.RelatingUserId))
                .Select(r => new
                {
                    r.RelatedUserId,
                    r.RelatedUser.UserName
                })
                .ToListAsync();

            // user has no friends - redirect to Friends list with message popup
            if (!relationships.Any())
            {
                TempData["FriendsMessage"] = ADD_FRIENDS;
                return RedirectToAction(nameof(FriendsController.Index), "Friends");
            }

            ViewData["RelatedUserId"] = new SelectList(relationships, "RelatedUserId", "UserName");

            return View(cartItem);
        }

        // POST: Cart/Gift/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gift(int id, [Bind("CartGameId,CartUserId,ReceivingUserId,GameId,AddedOn")] CartGame cartItem)
        {
            if (id != cartItem.CartGameId)
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            if (cartItem.CartUserId != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User recipientUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == cartItem.ReceivingUserId);
                    if (recipientUser == null)
                    {
                        return NotFound();
                    }

                    bool duplicateIndexExists = await _context.CartGames
                        .AnyAsync(c => c.CartGameId != cartItem.CartGameId && c.GameId == cartItem.GameId && c.CartUserId == user.Id && c.ReceivingUserId == recipientUser.Id);

                    if (duplicateIndexExists)
                    {
                        TempData["CartError"] = $"Gift for friend already exists in your cart.";
                        return RedirectToAction(nameof(Index));
                    }

                    _context.CartGames.Update(cartItem);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.CartGames.AnyAsync(c => c.CartGameId == cartItem.CartGameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            var relationships = await _context.UserRelationships
                .Include(r => r.RelatedUser)
                .Where(r => r.RelatingUserId == user.Id && r.Type == Relationship.Friend &&
                    !r.RelatedUser.ReceivingCartItems.Any(c => c.GameId == cartItem.GameId && c.CartUserId == r.RelatingUserId))
                .Select(r => new
                {
                    r.RelatedUserId,
                    r.RelatedUser.UserName
                })
                .ToListAsync();

            // user has no friends - redirect to Friends list with message popup
            if (!relationships.Any())
            {
                TempData["FriendsMessage"] = ADD_FRIENDS;
                return RedirectToAction(nameof(FriendsController.Index), "Friends");
            }

            ViewData["RelatedUserId"] = new SelectList(relationships, "RelatedUserId", "UserName");

            return View(cartItem);
        }
    }
}
