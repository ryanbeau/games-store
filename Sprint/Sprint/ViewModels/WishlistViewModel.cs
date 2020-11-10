using Sprint.Enums;
using Sprint.Models;
using System.Collections.Generic;

namespace Sprint.ViewModels
{
    public class WishlistViewModel
    {
        public User User { get; set; }
        public WishlistVisibility WishlistVisibility { get; set; }
        public List<WishlistItemViewModel> Games { get; set; }
    }
}
