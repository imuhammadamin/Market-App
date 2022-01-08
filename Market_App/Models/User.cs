using Market_App.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Models
{
    internal class User
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public UserRole Role { get; set; } = UserRole.User;
        
        public string Login { get; set; }
        
        public string Password { get; set; }
    }
}
