using System;
using System.Collections.Generic;

namespace Sprint.Models
{
    public partial class Reviews
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public string ReviewContent { get; set; }
    }
}
