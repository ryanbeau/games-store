using System;
using System.Collections.Generic;

namespace Sprint.Models
{
    public partial class GameType
    {
        public GameType()
        {
            Games = new HashSet<Games>();
        }

        public int GameTypeId { get; set; }
        public string GameType1 { get; set; }

        public virtual ICollection<Games> Games { get; set; }
    }
}
