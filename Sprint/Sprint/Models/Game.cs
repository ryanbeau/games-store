using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public partial class Game
    {
        public Game()
        {
            Reviews = new HashSet<Review>();
            GameImages = new HashSet<GameImage>();
            Wishlists = new HashSet<UserGameWishlist>();
        }

        public int GameId { get; set; }
        public int GameTypeId { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }

        [Display(Name = "Game Type")]
        public virtual GameType GameType { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<GameImage> GameImages { get; set; }
        public virtual ICollection<UserGameWishlist> Wishlists { get; set; }
    }
}
