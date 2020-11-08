using Sprint.Models;

namespace Sprint.ViewModels
{
    public class GameItemViewModel
    {
        public bool IsOwned { get; set; }
        public bool IsInCart { get; set; }
        public bool IsWishlisted { get; set; }
        public Game Game { get; set; }
        public GameImage Image { get; set; }
        public GameDiscount Discount { get; set; }
    }
}
