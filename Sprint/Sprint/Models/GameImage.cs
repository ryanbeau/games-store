using Sprint.Enums;

namespace Sprint.Models
{
    public partial class GameImage
    {
        public int GameImageId { get; set; }
        public int GameId { get; set; }
        public string ImageURL { get; set; }
        public ImageType ImageType { get; set; }

        public virtual Game Game { get; set; }
    }
}
