using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sprint.Data;
using Sprint.Models;
using Sprint.ViewModels;

namespace Sprint.Controllers
{
    public class EventController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public EventController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Event.Include(e => e.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);

            var activity = new EventViewModel
            {
                Event = await _context.Event
                    .Include(e => e.User)
                    .FirstOrDefaultAsync(m => m.EventId == id),

                JoinedUserCount = await _context.EventUsers
                    .CountAsync(e => e.EventId == id),

                IsUserJoined = user != null && await _context.EventUsers
                    .AnyAsync(e => e.UserId == user.Id),
            };

            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // GET: Event/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "AccountNum");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,UserId,EventName,EventDescription")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "AccountNum", @event.UserId);
            return View(@event);
        }

        // POST: Add user to Event
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Add(int id, string returnUrl)
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            Event activity = _context.Event.FirstOrDefault(e => e.EventId == id);
            if (activity == null)
            {
                return NotFound();
            }

            EventUser eventUser = new EventUser
            {
                EventId = activity.EventId,
                UserId = user.Id,
            };

            _context.EventUsers.Add(eventUser);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Event", new { id = activity.EventId  });
        }

        // POST: Remove user from Event
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Remove(int id, string returnUrl)
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            Event activity = _context.Event.FirstOrDefault(e => e.EventId == id);
            if (activity == null)
            {
                return NotFound();
            }

            EventUser eventUser = await _context.EventUsers
                .FirstOrDefaultAsync(e => e.EventId == activity.EventId);
            if (eventUser != null)
            {
                _context.EventUsers.Remove(eventUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Event", new { id = activity.EventId });
        }

        // GET: Event/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Event.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "AccountNum", activity.UserId);
            return View(activity);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,UserId,EventName,EventDescription")] Event activity)
        {
            if (id != activity.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(activity.EventId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "AccountNum", activity.UserId);
            return View(activity);
        }

        // GET: Event/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Event
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await _context.Event.FindAsync(id);
            _context.Event.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }
    }
}