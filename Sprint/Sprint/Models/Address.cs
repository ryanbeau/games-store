using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class Address
    {
        public Address()
        {
            ShippedOrders = new HashSet<Order>();
            BilledOrders = new HashSet<Order>();
        }

        public int AddressId { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength (30, ErrorMessage = "Street must be 30 Characters or less")]
        [MinLength(3, ErrorMessage = "Minimum 3 Characters")]
        public string Street {get; set;}
        [Required]
        [StringLength(30, ErrorMessage = "City must be 30 Characters or less")]
        [MinLength(3, ErrorMessage = "Minimum 3 Characters")]
        public string City { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Province must be 30 Characters or less")]
        [MinLength(3, ErrorMessage = "Minimum 3 Characters")]
        public string Province { get; set; }
        [Required]
        [StringLength(6, ErrorMessage = "Postal Code must be 6 Characters or less")]
        [MinLength(6, ErrorMessage = "Minimum 6 Characters")]

        public string Postal { get; set; }

        public User User { get; set; }

        public virtual ICollection<Order> ShippedOrders { get; set; }
        public virtual ICollection<Order> BilledOrders { get; set; }
    }
}
