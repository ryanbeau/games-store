using Sprint.Models;

namespace Sprint.ViewModels
{
    public class WishlistItemViewModel
    {
        public bool IsInCart { get; set; }
        public GameImage Image { get; set; }

        public UserGameWishlist WishlistItem { get; set; }
    }
}
