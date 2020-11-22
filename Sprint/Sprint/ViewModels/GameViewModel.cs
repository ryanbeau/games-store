using Sprint.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sprint.ViewModels
{
    public class GameViewModel
    {
        public bool IsOwned { get; set; }
        public bool IsInCart { get; set; }
        public bool IsWishlisted { get; set; }
        [Display(Name = "Average Rating")]
        public int AverageRating { get; set; }
        public List<GameImage> Images { get; set; }
        public Game Game { get; set; }
        public GameImage Image { get; set; }
        public GameDiscount Discount { get; set; }
    }
}
