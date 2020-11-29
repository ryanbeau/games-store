using Microsoft.AspNetCore.Identity;
using Sprint.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            Wallets = new HashSet<Wallet>();
            Addresses = new HashSet<Address>();
            Orders = new HashSet<Order>();
            OwnedItems = new HashSet<OrderItem>();
        }

        [Display(Name = "Account Number")]
        public string AccountNum { get; set; }
        
        [Display(Name = "User Name")]
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Receive Promotional Emails")]
        public bool ReceivePromotionalEmails { get; set; }

        [Display(Name = "Preferred Platform")]
        public int? PreferredPlatformTypeId { get; set; }

        [Display(Name = "Preferred Category")]
        public int? PreferredGameTypeId { get; set; }

        [Display(Name = "Wishlist Visibility")]
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

        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<OrderItem> OwnedItems { get; set; }
    }
}
