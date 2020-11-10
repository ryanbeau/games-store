using Sprint.Models;

namespace Sprint.ViewModels
{
    public class CartItemViewModel
    {
        public GameImage Image { get; set; }
        public GameDiscount Discount { get; set; }

        public CartGame CartItem { get; set; }
    }
}
