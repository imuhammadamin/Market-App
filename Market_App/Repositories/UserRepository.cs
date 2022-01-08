﻿using System;
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
        static ClientPanel clientPanel = new ClientPanel();

        private DbContextApp _Db = new DbContextApp();

        public void Create(User user)
        {
            _Db.Users.Add(user);
            _Db.SaveChanges();
        }

        public IList<User> GetAllUsers()
        {
            return _Db.Users.ToList();
        }
        
        public IList<User> GetClients()
        {
            return _Db.Users.Where(x => x.Role == UserRole.User).ToList();
        }
        
        public IList<User> GetAdmins()
        {
            return _Db.Users.Where(x => x.Role == UserRole.Admin).ToList();
        } 

        public User Login(string login, string password)
        {
            var user = _Db.Users.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public void EditUser(User user)
        {

            var _allUsers = _Db.Users.ToList();

            var us = _allUsers.Where(x => x.Id == user.Id).FirstOrDefault();

            if(us != null)
            {
                us.FirstName = user.FirstName;
                us.LastName = user.LastName;
                us.Role = user.Role;
                us.Login = user.Login;
                us.Password = user.Password;
            }

            else
            {
                clientPanel.Execute();
            }

            _Db.Users.UpdateRange(_allUsers);
            
            _Db.SaveChanges();
        }

        public void RemoveUser(User user)
        {

            var us = _Db.Users.Where(x => x.Id == user.Id).FirstOrDefault();

            if (us != null)
            {
                _Db.Users.Remove(us);
            }
            else
            {
                clientPanel.Execute();
            }

            _Db.SaveChanges();
        }
    }
}
