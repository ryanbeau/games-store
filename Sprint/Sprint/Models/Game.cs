using System.Collections.Generic;

namespace Sprint.Models
{
    public partial class Game
    {
        public Game()
        {
            Reviews = new HashSet<Review>();
        }

        public int GameId { get; set; }
        public int GameTypeId { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }

        public virtual GameType GameType { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
