using Sprint.Enums;
using System.Collections.Generic;

namespace Sprint.Models
{
    public class Wishlist
    {
        public WishlistVisibility WishlistVisibility { get; set; }
        public List<UserGameWishlist> Games { get; set; }
    }
}
