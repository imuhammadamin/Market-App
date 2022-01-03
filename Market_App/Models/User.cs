using Market_App.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public string Login { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{Id}\n{FirstName}\n{LastName}\n{Role}\n{Login}\n{Password}";
        }
    }
}
