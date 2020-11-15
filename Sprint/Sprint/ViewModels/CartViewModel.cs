using Sprint.Models;
using System.Collections.Generic;

namespace Sprint.ViewModels
{
    public class CartViewModel
    {
        public User User { get; set; }
        public List<CartItemViewModel> Items { get; set; }
    }
}
