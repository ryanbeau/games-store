using Sprint.Models;

namespace Sprint.ViewModels
{
    public class ConfirmationItemViewModel
    {
        public GameImage Image { get; set; }
        public Game Game { get; set; }
        public string ItemNumber { get; set; }
        public bool PhysicallyOwned { get; set; }
        public User GiftedUser { get; set; }
    }
}
