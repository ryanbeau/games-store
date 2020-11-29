using System;
using System.Collections.Generic;

namespace Sprint.Models
{
    public class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int WalletId { get; set; }
        public int? ShippingAddressId { get; set; }
        public int? BillingAddressId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User User { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public virtual Address BillingAddress { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
