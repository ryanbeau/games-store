using System;
using System.Collections.Generic;

namespace Sprint.Models
{
    public partial class Games
    {
        public int GameId { get; set; }
        public int GameTypeId { get; set; }
        public string GameName { get; set; }
        public string Developer { get; set; }
        public int Rating { get; set; }

        public virtual GameType GameType { get; set; }
    }
}
