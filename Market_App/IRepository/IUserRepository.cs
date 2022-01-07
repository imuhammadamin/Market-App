using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IRepository

{
    interface IUserRepository
    {
        public void Create(User user);
        public IList<User> GetAllUsers();
        public IList<User> GetClients();
        public IList<User> GetAdmins();
        User Login(string login, string password);
        public void EditUser(User user);
        public void RemoveUser(User user);
    }
}
