using System;
using System.IO;
using System.Linq;
using Market_App.Models;

namespace Market_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserRepository userRepo = new UserRepository();
            

            Console.WriteLine("\t\t\t\t\t1.Sign In\t|\t2.Sign Up");
            Console.Write("Select: ");
            string SignInOrSignUp = Console.ReadLine();
            switch (SignInOrSignUp)
            {
                
                case "1":
                    BackTo:
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
                        Console.WriteLine("1.Try again\t|\t2.Exit ");
                        Console.Write("Select: ");
                        string AgainOrExit = Console.ReadLine();

                        if (AgainOrExit == "1")
                            goto BackTo;
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
                    Console.Write("Password: ");
                    string userspassword = Console.ReadLine();

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
                    break;
                  
            }
        }
    }
}
