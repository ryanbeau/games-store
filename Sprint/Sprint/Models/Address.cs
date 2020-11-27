using System;

namespace Sprint.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }

        public string MailingStreet {get; set;}

        public string MailingCity { get; set; }

        public string MailingProvince { get; set; }

        public string MailingPostal { get; set; }

        public string ShippingStreet { get; set; }

        public string ShippingCity { get; set; }

        public string ShippingProvince { get; set; }

        public string ShippingPostal { get; set; }

        public User User { get; set; }

    }
}
