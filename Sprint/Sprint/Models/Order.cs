namespace Sprint.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int WalletId { get; set; }
        public int? ShippingAddressId { get; set; }
        public int? BillingAddressId { get; set; }
        public string ShippingFullName { get; set; }
        public string BillingFullName { get; set; }

        public virtual User User { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public virtual Address BillingAddress { get; set; }
    }
}
