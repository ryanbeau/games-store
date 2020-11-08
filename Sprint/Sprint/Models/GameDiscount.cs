using System;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class GameDiscount
    {
        public int DiscountId { get; set; }
        public int GameId { get; set; }
        [Display(Name = "Price")]
        public decimal DiscountPrice { get; set; }
        public DateTime DiscountStart { get; set; }
        public DateTime DiscountFinish { get; set; }

        public Game Game { get; set; }
    }
}
