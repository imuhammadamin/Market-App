using System;
using System.IO;
using System.Linq;
using Market_App.Enums;
using Market_App.IRepository;
using Market_App.Models;

namespace Market_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
              IUserRepository userRepo = new UserRepository();

            BackTo:
            Console.WriteLine("\t\t\t\t\t1.Sign In\t|\t2.Sign Up");
            Console.Write("Select: ");
            string SignInOrSignUp = Console.ReadLine();
            switch (SignInOrSignUp)
            {
                
                case "1":
              
                Console.Write("Enter Login: ");
                string login = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                
                User result = userRepo.Login(login, password);
                    if (result != null)
                    {
                        Commerce.Execute();
                    }
                    else
                    {
                        Console.WriteLine("\nNot Found!\n1.Try again\t|\t2.Exit ");
                        Console.Write("Select: ");
                        string AgainOrExit = Console.ReadLine();

                        if (AgainOrExit == "1")
                        {
                            Console.Clear();
                            goto BackTo;
                        }
                            
                        else if(AgainOrExit == "2")
                            Environment.Exit(0);

                    }

                break;
              
                
                case "2":
                    Console.Write("Name: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Surname: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Login: ");
                    string userslogin = Console.ReadLine();
                    backPass:
                    Console.Write("Password: ");
                    string userspassword = Console.ReadLine();
                    if(userspassword.Length<5)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter more than 5 items");
                        goto backPass;
                    }
                    

                    User user = new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstName,
                        LastName = lastName,
                        Role = UserRole.User,
                        Login = userslogin,
                        Password = userspassword
                    };
                    userRepo.Create(user);
                    Console.WriteLine("\n1.Back To\t|\t2.Exit ");
                    Console.Write("Select: ");
                    string BackOrExit = Console.ReadLine();

                    if (BackOrExit == "1")
                    {
                        Console.Clear();
                        goto BackTo;
                    }
                    else if (BackOrExit == "2")
                        Environment.Exit(0);
                    break;
                 
            }

        }
    }
}
