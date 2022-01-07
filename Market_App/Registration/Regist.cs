using Market_App.Enums;
using Market_App.IRepository;
using Market_App.Models;
using Market_App.Service;
using System;

namespace Market_App.Registration
{
    internal class Regist
    {
        private IUserRepository _userRepo = new UserRepository();

        static AdminPanel adminPanel = new AdminPanel();

        static ClientPanel clientPanel = new ClientPanel();

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
                    Menu();
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

            _userRepo.Create(
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



            User user = _userRepo.Login(login, password);
            try
            {
                if (login == user.Login && password == user.Password && user.Role == UserRole.Admin)
                {
                    adminPanel.Execute();
                }

                else if (login == user.Login && password == user.Password && user.Role == UserRole.User)
                {
                    clientPanel.Execute();
                }

            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no such user!");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("1. Try again | 2. Sig Up | 3. Exit ");
                Console.Write("\n> ");
                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        Menu();
                        break;
                    case "2":
                        SignUp();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Menu();
                        break;
                }
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
