using System;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }

        public int UserId {get; set;}

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Must be numeric")]
        [StringLength(16, ErrorMessage = "Card Number Must be only 16 digits")]
        [MinLength(16)]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Must be numeric")]
        [StringLength(2, ErrorMessage = "Month Value must only be 2 characters like '02'")]
        [MinLength(2)]
        public string Month { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Must be numeric")]
        [StringLength(2, ErrorMessage = " Year Value Must be only 2 Digits Like '02'")]
        [MinLength(2)]
        public string Year { get; set; }

        public User User { get; set; }

    }
}
