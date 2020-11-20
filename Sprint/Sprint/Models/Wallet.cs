using System;

namespace Sprint.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }

        public int UserId {get; set;}

        public int CardNumber { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public User User { get; set; }

    }
}
