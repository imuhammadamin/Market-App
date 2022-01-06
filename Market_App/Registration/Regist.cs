using Market_App.Enums;
using Market_App.IRepository;
using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Registration
{
    internal class Regist
    {
        public static void Sign()
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
                        string AgainOrExit = Console.ReadLine();

                        if (AgainOrExit == "1")
                        {
                            Console.Clear();
                            goto BackTo;
                        }

                        else if (AgainOrExit == "2")
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
                    if (userspassword.Length < 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter more than 5 items");
                        goto backPass;
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
