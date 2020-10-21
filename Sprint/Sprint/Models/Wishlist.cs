using Sprint.Enums;
using System.Collections.Generic;

namespace Sprint.Models
{
    public class Wishlist
    {
        public bool Authorized { get; set; }
        public string Username { get; set; }
        public WishlistVisibility WishlistVisibility { get; set; }
        public List<UserGameWishlist> Games { get; set; }
    }
}
