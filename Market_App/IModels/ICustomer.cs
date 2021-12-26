using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IModels
{
    interface ICustomer
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
