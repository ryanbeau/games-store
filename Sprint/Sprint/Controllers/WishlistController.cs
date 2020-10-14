using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sprint.Data;
using Sprint.Models;

namespace Sprint.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wishlist
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserGameWishlist.Include(u => u.Game).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Wishlist/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGameWishlist = await _context.UserGameWishlist
                .Include(u => u.Game)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserGameId == id);
            if (userGameWishlist == null)
            {
                return NotFound();
            }

            return View(userGameWishlist);
        }

        // GET: Wishlist/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Developer");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "AccountNum");
            return View();
        }

        // POST: Wishlist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserGameId,UserId,GameId,AddedOn")] UserGameWishlist userGameWishlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userGameWishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Developer", userGameWishlist.GameId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "AccountNum", userGameWishlist.UserId);
            return View(userGameWishlist);
        }

        // GET: Wishlist/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGameWishlist = await _context.UserGameWishlist.FindAsync(id);
            if (userGameWishlist == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Developer", userGameWishlist.GameId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "AccountNum", userGameWishlist.UserId);
            return View(userGameWishlist);
        }

        // POST: Wishlist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserGameId,UserId,GameId,AddedOn")] UserGameWishlist userGameWishlist)
        {
            if (id != userGameWishlist.UserGameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userGameWishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGameWishlistExists(userGameWishlist.UserGameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Developer", userGameWishlist.GameId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "AccountNum", userGameWishlist.UserId);
            return View(userGameWishlist);
        }

        // GET: Wishlist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGameWishlist = await _context.UserGameWishlist
                .Include(u => u.Game)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserGameId == id);
            if (userGameWishlist == null)
            {
                return NotFound();
            }

            return View(userGameWishlist);
        }

        // POST: Wishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userGameWishlist = await _context.UserGameWishlist.FindAsync(id);
            _context.UserGameWishlist.Remove(userGameWishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserGameWishlistExists(int id)
        {
            return _context.UserGameWishlist.Any(e => e.UserGameId == id);
        }
    }
}
