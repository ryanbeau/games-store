using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int Rating { get; set; }
        [Display(Name = "Comment")]
        public string ReviewContent { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
    }
}
