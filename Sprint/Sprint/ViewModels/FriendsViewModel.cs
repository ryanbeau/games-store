using Sprint.Models;
using System.Collections.Generic;

namespace Sprint.ViewModels
{
    public class FriendsViewModel
    {
        public List<UserRelationship> PendingRelationships { get; set; }
        public List<UserRelationship> FriendRelationships { get; set; }
    }
}
