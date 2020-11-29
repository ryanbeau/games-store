using Sprint.Models;

namespace Sprint.ViewModels
{
    public class EventViewModel
    {
        public int JoinedUserCount { get; set; }
        public bool IsUserJoined { get; set; }

        public Event Event { get; set; }
    }
}
