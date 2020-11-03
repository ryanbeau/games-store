using Sprint.Enums;
using System.Collections.Generic;

namespace Sprint.ViewModels
{
    public class WishlistViewModel
    {
        public bool Authorized { get; set; }
        public string Username { get; set; }
        public WishlistVisibility WishlistVisibility { get; set; }
        public List<WishlistItemViewModel> Games { get; set; }
    }
}
