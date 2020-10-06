using System;
using System.Collections.Generic;

namespace Sprint.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<Users>();
        }

        public int UserTypeId { get; set; }
        public string UserType1 { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
