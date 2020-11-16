using System.Collections.Generic;

namespace Sprint.ViewModels
{
    public class HomeViewModel
    {
        public List<GameItemViewModel> BannerGames { get; set; }
        public List<GameItemViewModel> DiscountedGames { get; set; }
    }
}
