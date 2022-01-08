using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IRepository

{
    interface IUserRepository
    {
        void Create(User user);
        
        IList<User> GetAllUsers();
        
        IList<User> GetClients();
        
        IList<User> GetAdmins();
        
        User Login(string login, string password);
        
        void EditUser(User user);
        
        void RemoveUser(User user);
    }
}
