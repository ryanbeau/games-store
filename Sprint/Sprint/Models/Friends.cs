using System.Collections.Generic;

namespace Sprint.Models
{
    public class Friends
    {
        public List<UserRelationship> PendingRelationships { get; set; }
        public List<UserRelationship> FriendRelationships { get; set; }
    }
}
