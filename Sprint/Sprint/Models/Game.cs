using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public partial class Game
    {
        public int GameId { get; set; }
        public int GameTypeId { get; set; }
        public string GameName { get; set; }
        public string Developer { get; set; }
        public int Rating { get; set; }

        public virtual GameType GameType { get; set; }
    }
}
