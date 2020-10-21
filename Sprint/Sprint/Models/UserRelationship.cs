using Sprint.Enums;

namespace Sprint.Models
{
    public class UserRelationship
    {
        public int UserRelationshipId { get; set; }
        public int RelatingUserId { get; set; }
        public int RelatedUserId { get; set; }
        public Relationship Type { get; set; }

        public User RelatingUser { get; set; }
        public User RelatedUser { get; set; }
    }
}
