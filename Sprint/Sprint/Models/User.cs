using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Sprint.Models
{
    public partial class User : IdentityUser<int>
    {
        public User()
        {
            Reviews = new HashSet<Review>();
            Wishlists = new HashSet<UserGameWishlist>();
        }

        public string AccountNum { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<UserGameWishlist> Wishlists { get; set; }
    }
}
