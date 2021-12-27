using System;
using System.Collections.Generic;
using System.Linq;
using Market_App.IModels;

namespace Market_App.Models
{
    internal class Customer : SignIn, ICustomer
    {
        public int ID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
