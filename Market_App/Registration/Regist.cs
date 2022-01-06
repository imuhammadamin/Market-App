﻿using Market_App.Enums;
using Market_App.IRepository;
using Market_App.Models;
using System;

namespace Market_App.Registration
{
    internal class Regist
    {
        private IUserRepository userRepo = new UserRepository();
        public void Execute()
        {
            Console.Clear();

            Console.WriteLine("\t\t\t\t\t1.Sign In | 2.Sign Up | 3. Exit");
            Console.Write("Select: ");
            string choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    SignIn();
                    break;
                case "2":
                    SignUp();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Execute();
                    break;
            }
        }
        public void SignUp()
        {
            Console.Clear();

            Console.Write("Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Surname: ");
            string lastName = Console.ReadLine();

            Console.Write("Login: ");
            string userslogin = Console.ReadLine();

            Console.Write("Password: ");
            string userspassword = Console.ReadLine();

            if (userspassword.Length < 5)
            {
                Console.Clear();
                Console.WriteLine("Enter more than 5 items");
            }

            userRepo.Create(
                new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Role = UserRole.User,
                    Login = userslogin,
                    Password = userspassword
                });
            Console.WriteLine("\n1.Back To\t|\t2.Exit ");
            Console.Write("Select: ");
            string choose = Console.ReadLine();

            if (choose == "1")
            {
                Console.Clear();
            }
            else if (choose == "2")
                Environment.Exit(0);
        }
        public void SignIn()
        {
            Console.Clear();
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string password = ReadPassword();



            User user = userRepo.Login(login, password);
            if (login == user.Login && password == user.Password && user.Role == UserRole.Admin)
            {
                CommerceAdmin.Execute();
            }

            else if (login == user.Login && password == user.Password && user.Role == UserRole.User)
            {
                CommerceUser.Execute();
            }
            else
            {
                Console.WriteLine("\nNot Found!\n1.Try again\t|\t2.Exit ");
                Console.Write("Select: ");
                string choose = Console.ReadLine();

                if (choose == "1")
                {
                    Console.Clear();
                    Execute();
                }

                else if (choose == "2")
                    Environment.Exit(0);

            }

        }
        public string ReadPassword()
        {
            string password = "";
            try
            {
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            return null;
                        case ConsoleKey.Enter:
                            return password;
                        case ConsoleKey.Backspace:
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                            break;
                        default:
                            password += key.KeyChar;
                            Console.Write("*");
                            break;
                    }
                }
            }
            catch
            {
                ReadPassword();
            }
            return password;
        }
    }
}
