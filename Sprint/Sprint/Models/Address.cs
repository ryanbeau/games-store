using System;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class Address
    {
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
        [RegularExpression(@"[A-Z][0-9][A-Z] ?[0-9][A-Z][0-9]", ErrorMessage = "Not a valid Postal Code")]
        public string Postal { get; set; }

        public User User { get; set; }

    }
}
