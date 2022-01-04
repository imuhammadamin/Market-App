using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Market_App.Enums;
using Market_App.IRepository;
using Market_App.Service;

namespace Market_App.Models
{
    internal class UserRepository : IUserRepository
    {
        public User Create(User user)
        {
            string fileName = MethodService.GetUserPath(user.Id);
            string userData = user.ToString();
            File.WriteAllText(fileName, userData);

            return user;
        }

        public User Login(string login, string password)
        {
            string[] files = Directory.GetFiles(Constants.UsersDbPath);

            foreach (string file in files)
            {
                string[] userDetails = File.ReadAllLines(file);
                string userLogin = userDetails[4];
                string userPassword = userDetails[5];

                if (login == userLogin && password == userPassword)
                {
                    return new User
                    {
                        Id = Guid.Parse(userDetails[0]),
                        FirstName = userDetails[1],
                        LastName = userDetails[2],
                        Role = (UserRole)Enum.Parse(typeof(UserRole), userDetails[3]),
                        Login = userDetails[4],
                        Password = userDetails[5]
                    };
                }
            }

            return null;
        }
    }
}
