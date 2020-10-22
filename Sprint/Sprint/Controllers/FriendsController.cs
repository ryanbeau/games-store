using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint.Data;
using Sprint.Enums;
using Sprint.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Sprint.Controllers
{
    public class FriendsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public FriendsController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Friends
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            var friends = await _context.UserRelationships
                .Include(r => r.RelatedUser)
                .Where(r => r.RelatingUserId == user.Id && r.Type == Relationship.Friend)
                .ToListAsync();

            var pendings = await _context.UserRelationships
                .Include(r => r.RelatingUser)
                .Where(r => r.RelatedUserId == user.Id && r.Type == Relationship.Pending)
                .ToListAsync();

            return View(new Friends
            {
                FriendRelationships = friends,
                PendingRelationships = pendings,
            });
        }

        // GET: Friends/Add
        [Authorize(Roles = "Admin,Member")]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Friends/Add
        [HttpPost, ActionName("Add")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> AddUsername([FromForm]string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                ModelState.AddModelError("username", $"Username is required");
                return View();
            }

            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            User userInviting = await _userManager.FindByNameAsync(username);
            if (userInviting == null)
            {
                ModelState.AddModelError("username", $"No results found matching \"{username}\"");
                return View();
            }
            else if (userInviting.Id == user.Id)
            {
                ModelState.AddModelError("username", "Cannot invite yourself");
                return View();
            }

            TempData["FriendInvite"] = $"A friend invite was sent to {userInviting.UserName}.";

            // friend relationship (or invite) already exists
            if (await _context.UserRelationships.AnyAsync(r => r.RelatingUserId == user.Id && r.RelatedUserId == userInviting.Id))
            {
                return RedirectToAction(nameof(Index));
            }

            var relationship = new UserRelationship
            {
                RelatingUserId = user.Id,
                RelatedUserId = userInviting.Id,
                Type = Relationship.Pending,
            };

            // friend has already sent relationship invite - add them as a friend without invite
            var existingRelationship = await _context.UserRelationships.FirstOrDefaultAsync(r => r.RelatingUserId == userInviting.Id && r.RelatedUserId == user.Id);
            if (existingRelationship != null)
            {
                existingRelationship.Type = Relationship.Friend;
                _context.UserRelationships.Update(existingRelationship);

                relationship.Type = Relationship.Friend;
                TempData["FriendInvite"] = $"{userInviting.UserName} has been added to your friends.";
            }

            _context.UserRelationships.Add(relationship);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Friends/Remove/username
        [HttpGet, Route("Friends/Remove/{username}")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Remove(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            User userRemoving = await _userManager.FindByNameAsync(username);
            if (userRemoving.Id == user.Id)
            {
                return BadRequest();
            }

            var relationship = await _context.UserRelationships
                .Include(r => r.RelatedUser)
                .FirstOrDefaultAsync(r => r.RelatingUserId == user.Id && r.RelatedUserId == userRemoving.Id);
            if (relationship == null)
            {
                return NotFound();
            }

            return View(relationship);
        }

        // POST: Friends/Remove/username
        [HttpPost, ActionName("Remove")]
        [Authorize(Roles = "Admin,Member")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Problem();
            }

            User userRemoving = await _userManager.FindByNameAsync(username);
            if (user.Id == userRemoving.Id)
            {
                return BadRequest();
            }

            // get relationships
            var relationships = await _context.UserRelationships
                .Where(r => (r.RelatingUserId == user.Id && r.RelatedUserId == userRemoving.Id) || 
                    (r.RelatingUserId == userRemoving.Id && r.RelatedUserId == user.Id))
                .ToArrayAsync();
            _context.RemoveRange(relationships);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
