using System;

namespace Sprint.Models
{
    public class UserGameWishlist
    {
        public int UserGameId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
    }
}
