using Market_App.Enums;
using Market_App.Extensions;
using Market_App.IRepository;
using Market_App.Models;
using Market_App.Service;
using System;
using System.Linq;
using System.Text;
using XSystem.Security.Cryptography;

namespace Market_App.Registration
{
    internal class MainMenu
    {
        private static DbContextApp _Db = new DbContextApp();

        private IUserRepository _userRepo = new UserRepository();

        static AdminPage adminPanel = new AdminPage();

        static ClientPage clientPanel = new ClientPage();

        public static User us = new User();

        public void Menu()
        {
            
            Console.Clear();

            Console.WriteLine("\t\t\t\t\t1.Sign In | 2.Sign Up | 3. Exit");
            Console.Write("> ");
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
                    CatchErrors.InputError();
                    Menu();
                    break;
            }
        }
        
        private void SignUp()
        {
            Console.Clear();

            Console.Write("First name: ");
            string firstName = Console.ReadLine().Capitalize();

            Console.Write("Last name: ");
            string lastName = Console.ReadLine().Capitalize();

            Console.Write("Login: ");
            string Login = Console.ReadLine();

            if (!UserInspection(Login))
            {
                Console.Write("Password: ");
                string password = Console.ReadLine();

                if (password.Length < 5)
                {
                    Console.Clear();
                    Console.WriteLine("Enter more than 5 items");
                }
                else
                {
                    password = MethodService.HashPassword(password);
                    _userRepo.Create(
                        new User
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Role = UserRole.User,
                            Login = Login,
                            Password = password
                        });

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have successfully registered!");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("\n1.Menu\t|\t2.Exit ");
                    Console.Write("\n> ");
                    string choose = Console.ReadLine();

                    if (choose == "1")
                    {
                        Console.Clear();
                        Menu();
                    }
                    else if (choose == "2")
                        Environment.Exit(0);
                }
            }
            else
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Such a user exists!");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("\nWould you like to try again? [y, n]: ");
                string choose = Console.ReadLine();

                if (choose == "y" || choose == "Y") SignUp();
                else Menu();
            }
        }
        
        private void SignIn()
        {
            Console.Clear();

            Console.Write("Login: ");
            string Login = Console.ReadLine();

            Console.Write("Password: ");
            string password = MethodService.HashPassword(ReadPassword());

            User user = _userRepo.Login(new SignIn() { Login = Login, Password = password });

            try
            {
                if (Login == user.Login && password == user.Password && user.Role == UserRole.Admin)
                {
                    Console.Title = "Admin";
                    adminPanel.Execute();
                }

                else if (Login == user.Login && password == user.Password && user.Role == UserRole.User)
                {
                    us = user;
                    Console.Title = us.Login;
                    clientPanel.Execute();
                }
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no such user!\n");
                Console.ForegroundColor = ConsoleColor.White;

                Menu();
            }

        }
        
        private string ReadPassword()
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

        private bool UserInspection(string login)
        {
            return _Db.Users.Any(x => x.Login == login);
        }
    }
}
