using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sprint.Data;
using Sprint.Models;

namespace Sprint.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Preferred platform")]
            public int? PreferredPlatformTypeId { get; set; }

            [Display(Name = "Preferred category")]
            public int? PreferredGameTypeId { get; set; }

            [Display(Name = "Receive promotional emails")]
            public bool ReceivePromotionalEmails { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                PreferredPlatformTypeId = user.PreferredPlatformTypeId,
                PreferredGameTypeId = user.PreferredGameTypeId,
                ReceivePromotionalEmails = user.ReceivePromotionalEmails,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ViewData["PreferredGameTypeId"] = new SelectList(_context.GameTypes.OrderBy(t => t.Name), "GameTypeId", "Name", user.PreferredGameTypeId);
            ViewData["PreferredPlatformTypeId"] = new SelectList(_context.PlatformTypes.OrderBy(t => t.Name), "PlatformTypeId", "Name", user.PreferredPlatformTypeId);

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                ViewData["PreferredGameTypeId"] = new SelectList(_context.GameTypes.OrderBy(t => t.Name), "GameTypeId", "Name", user.PreferredGameTypeId);
                ViewData["PreferredPlatformTypeId"] = new SelectList(_context.PlatformTypes.OrderBy(t => t.Name), "PlatformTypeId", "Name", user.PreferredPlatformTypeId);

                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.PreferredGameTypeId != user.PreferredGameTypeId 
                || Input.PreferredPlatformTypeId != user.PreferredPlatformTypeId
                || Input.ReceivePromotionalEmails != user.ReceivePromotionalEmails)
            {
                user.PreferredGameTypeId = Input.PreferredGameTypeId;
                user.PreferredPlatformTypeId = Input.PreferredPlatformTypeId;
                user.ReceivePromotionalEmails = Input.ReceivePromotionalEmails;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
