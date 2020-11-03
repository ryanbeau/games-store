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
    public class ImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Images
        [HttpGet("[controller]/{gameId}")]
        public async Task<IActionResult> Index(int? gameId)
        {
            if (gameId == null)
            {
                return NotFound();
            }

            var applicationDbContext = _context.GameImages.Include(g => g.Game)
                .Where(g => g.GameId  == gameId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameImage = await _context.GameImages
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.GameImageId == id);
            if (gameImage == null)
            {
                return NotFound();
            }

            return View(gameImage);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Developer");
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameImageId,GameId,ImageURL,ImageType")] GameImage gameImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { gameId = gameImage.GameId });
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Developer", gameImage.GameId);
            return View(gameImage);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameImage = await _context.GameImages.FindAsync(id);
            if (gameImage == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Developer", gameImage.GameId);
            return View(gameImage);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameImageId,GameId,ImageURL,ImageType")] GameImage gameImage)
        {
            if (id != gameImage.GameImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameImageExists(gameImage.GameImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { gameId = gameImage.GameId });
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Developer", gameImage.GameId);
            return View(gameImage);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameImage = await _context.GameImages
                .Include(g => g.Game)
                .FirstOrDefaultAsync(m => m.GameImageId == id);
            if (gameImage == null)
            {
                return NotFound();
            }

            return View(gameImage);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameImage = await _context.GameImages.FindAsync(id);
            _context.GameImages.Remove(gameImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {gameId = gameImage.GameId });
        }

        private bool GameImageExists(int id)
        {
            return _context.GameImages.Any(e => e.GameImageId == id);
        }
    }
}
