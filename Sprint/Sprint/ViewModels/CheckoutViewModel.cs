using Sprint.Models;
using System.Collections.Generic;

namespace Sprint.ViewModels
{
    public class CheckoutViewModel
    {
        public CartViewModel CartCheckout { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Wallet> WalletCreditCards { get; set; }

        public decimal IndividualShippingCost { get; set; } 
        public decimal ItemsTotalPrice { get; set; } 
        public decimal TaxPercent { get; set; }
    }
}
