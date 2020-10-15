using System;
using System.ComponentModel;

namespace Sprint.Models
{
    public class UserGameWishlist
    {
        public int UserGameId { get; set; }        
        public int UserId { get; set; }
        public int GameId { get; set; }
        
        [DisplayName("Added on")]
        public DateTime? AddedOn { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
    }
}
