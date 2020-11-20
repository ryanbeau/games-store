using Sprint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprint.ViewModels
{
    public class ReviewsViewModel
    {
        public User User { get; set; }
        public Game Game { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }

    public class ReviewViewModel
    {
        public User User { get; set; }
        public bool IsOwn { get; set; }
        public int Rating { get; set; }
        public string ReviewContent { get; set; }

    }
}
