using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IModels
{
    interface IUserRepository
    {
        User Create(User user);
        User Login(string login, string password);
    }
}
