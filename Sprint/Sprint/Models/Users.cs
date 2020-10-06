using System;
using System.Collections.Generic;

namespace Sprint.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string Username { get; set; }
        public int AccountNum { get; set; }
        public string FName { get; set; }
        public string Address { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
