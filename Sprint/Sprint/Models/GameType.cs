using System.Collections.Generic;

namespace Sprint.Models
{
    public partial class GameType
    {
        public GameType()
        {
            Games = new HashSet<Game>();
        }

        public int GameTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
