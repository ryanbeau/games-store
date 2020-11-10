using Microsoft.AspNetCore.Identity;
using Sprint.Enums;
using System;
using System.Collections.Generic;

namespace Sprint.Models
{
    public partial class User : IdentityUser<int>
    {
        public User()
        {
            RelatedRelationships = new HashSet<UserRelationship>();
            RelatingRelationships = new HashSet<UserRelationship>();
            Reviews = new HashSet<Review>();
            CartItems = new HashSet<CartGame>();
            ReceivingCartItems = new HashSet<CartGame>();
            Wishlists = new HashSet<UserGameWishlist>();
        }

        public string AccountNum { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public WishlistVisibility WishlistVisibility { get; set; }

        /// <summary>
        /// Relationships where this User has Friended a User
        /// </summary>
        public virtual ICollection<UserRelationship> RelatedRelationships { get; set; }
        /// <summary>
        /// Relationships where this User was Friended by another User
        /// </summary>
        public virtual ICollection<UserRelationship> RelatingRelationships { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<CartGame> CartItems { get; set; }
        public virtual ICollection<CartGame> ReceivingCartItems { get; set; }
        public virtual ICollection<UserGameWishlist> Wishlists { get; set; }
    }
}
