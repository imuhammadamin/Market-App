using ConsoleTables;
using Market_App.Enums;
using Market_App.IRepository;
using Market_App.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Models
{
    internal class CommerceAdmin
    {
        private static IUserRepository userRepo = new UserRepository();
        private static IList<User> Users = userRepo.GetAllUsers();
        private static IList<Product> products = ProductRepository.GetAllProducts();
        private static Registration.Regist regist = new Registration.Regist();
        public static void Execute()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Browse all products | 2. Add product | 3. Update product | 4. Add Admin | 5. Show all admins | 6. Back | 7. Exit");
                Console.Write("Enter your option: ");
                string choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        ShowProducts();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        UpdateProduct();
                        break;
                    case "4":
                        AddAdmin();
                        break;
                    case "5":
                        ShowUsers();
                        break;
                    case "6":
                        regist.Execute();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        Execute();
                        break;
                }
            }
        }
        private static void AddAdmin()
        {
            Console.Write("Enter First name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter login: ");
            string login = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            User admin = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Role = UserRole.Admin,
                Login = login,
                Password = password
            };
            userRepo.Create(admin);

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Succes!");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Do you want add admin again? [y, n]");
            string choose = Console.ReadLine();

            if (choose == "Y" || choose == "y")
                AddAdmin();
            else Execute();
        }
        private static void ShowProducts()
        {
            Console.Clear();

            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var product in products)
            {
                table.AddRow(product.Id, product.Name, product.Price, product.Unit, product.Residue, product.Type);
            }

            OptionMenu(table);
        }
        private static void AddProduct()
        {
            Console.Write("\nEnter product name: ");
            string productName = Console.ReadLine();
            productName = char.ToUpper(productName[0]) + productName.Substring(1);

            var product = products.Where(x => x.Name == productName).FirstOrDefault();
            if (product != null)
            {
                Console.Write("Enter price: ");
                product.Price = decimal.Parse(Console.ReadLine());

                Console.Write("Enter residue: ");
                product.Residue = float.Parse(Console.ReadLine());

                ProductRepository.Update(product);

                Console.WriteLine("Product edited.\n");
            }
            else
            {
                Product product1 = new Product();
                Console.Write("Enter name: ");
                product1.Name = Console.ReadLine();

                Console.Write("Enter price: ");
                product1.Price = decimal.Parse(Console.ReadLine());

                Console.Write("Enter unit: ");
                product1.Unit = Console.ReadLine();

                Console.Write("Enter residue: ");
                product1.Residue = float.Parse(Console.ReadLine());

                Console.Write("Enter type: ");
                product1.Type = Console.ReadLine();

                ProductRepository.AddProduct(product1);

                Console.WriteLine("Product added.\n");
            }

            ShowProducts();
        }
        private static void UpdateProduct()
        {
            Console.Clear();

            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var product in products)
            {
                table.AddRow(product.Id, product.Name, product.Price, product.Unit, product.Residue, product.Type);
            }

            table.Write();

            Console.WriteLine("\n1. Delete product");
            Console.WriteLine("2. Edit product");
            Console.WriteLine("3. Back to Menu");

            Console.Write("\nEnter option: ");

            int choose = int.Parse(Console.ReadLine());
            if (choose == 1)
            {
                DeleteProduct(table);
            }
            else if (choose == 2)
            {
                EditProduct(table);
            }
            else if (choose == 3)
                Execute();
            else
            {
                Console.WriteLine("You entered incorrectly! Please enter again.");
                UpdateProduct(table);
            }

        }
        private static void UpdateProduct(ConsoleTable table)
        {
            table.Write();

            Console.WriteLine("\n1. Delete product");
            Console.WriteLine("2. Edit product");
            Console.WriteLine("3. Back to Menu");

            Console.Write("\nEnter option: ");

            int choose = int.Parse(Console.ReadLine());
            if (choose == 1)
            {
                DeleteProduct(table);
            }
            else if (choose == 2)
            {
                EditProduct(table);
            }
            else if (choose == 3)
                Execute();
            else
            {
                Console.WriteLine("You entered incorrectly! Please enter again.");
                UpdateProduct(table);
            }

        }
        private static void SearchProduct()
        {
            Console.Write("\nEnter the product name: ");
            string nameProduct = Console.ReadLine();
            string nameProduct1 = char.ToUpper(nameProduct[0]) + nameProduct.Substring(1);
            var product = Sales.GetProductsForSelling().Where(x => x.Name.Equals(nameProduct1)).FirstOrDefault();
            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            table.AddRow(product.Id, product.Name, product.Price, product.Unit, product.Residue, product.Type);

            OptionMenu("Search product", table);
        }
        private static void DeleteProduct(ConsoleTable table)
        {
            Console.Clear();

            table.Write();

            Console.Write("Enter №: ");
            int id = int.Parse(Console.ReadLine());

            var product = products.Where(x => x.Id == id).FirstOrDefault();
            if (product != null)
            {
                ProductRepository.RemoveProduct(product);
                ShowProducts();
            }
            else
            {
                Console.WriteLine("Such a product is not available in the basket!");
                DeleteProduct(table);
            }
        }
        private static void OptionMenu(ConsoleTable table)
        {
            Console.Clear();

            table.Write();

            Console.WriteLine("1. Add product");
            Console.WriteLine("2. Update product");
            Console.WriteLine("3. Search product");
            Console.WriteLine("4. Delete product");
            Console.WriteLine("5. Back to Menu");
            Console.Write("\nEnter option: ");

            string choose = Console.ReadLine();
            switch (choose)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    UpdateProduct(table);
                    break;
                case "3":
                    SearchProduct();
                    break;
                case "4":
                    DeleteProduct(table);
                    break;
                case "5":
                    Execute();
                    break;
                default:
                    Console.WriteLine("You entered incorrectly! Please enter again.");
                    OptionMenu(table);
                    break;
            }
        }
        private static void OptionMenu(string option, ConsoleTable table)
        {
            if (option == "Search product")
            {
                Console.Clear();

                table.Write();

                Console.WriteLine("1. Show all products");
                Console.WriteLine("2. Update product");
                Console.WriteLine("3. Delete product");
                Console.WriteLine("4. Back to Menu");
                Console.Write("\nEnter option: ");

                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        ShowProducts();
                        break;
                    case "2":
                        UpdateProduct(table);
                        break;
                    case "3":
                        DeleteProduct(table);
                        break;
                    case "4":
                        Execute();
                        break;
                    default:
                        Console.WriteLine("You entered incorrectly! Please enter again.");
                        OptionMenu(option, table);
                        break;
                }
            }
            else if (option == "Show all users")
            {
                Console.Clear();

                table.Write();

                Console.WriteLine("1. Add admin");
                Console.WriteLine("2. Edit admin");
                Console.WriteLine("3. Delete admin");
                Console.WriteLine("4. Back to Menu");
                Console.Write("\nEnter option: ");

                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        AddAdmin();
                        break;
                    case "2":
                        EditUser(table);
                        break;
                    case "3":
                        DeleteUser(table);
                        break;
                    case "4":
                        Execute();
                        break;
                    default:
                        Console.WriteLine("You entered incorrectly! Please enter again.");
                        OptionMenu(option, table);
                        break;
                }
            }
        }
        private static void EditProduct(ConsoleTable table)
        {
            Console.Clear();

            table.Write();

            Console.Write("\nEnter №: ");
            int id = int.Parse(Console.ReadLine());

            var product = products.Where(x => x.Id.Equals(id)).FirstOrDefault();

            Console.Clear();
            table.Write();

            Console.WriteLine("\n1. Name | 2. Price | 3. Unit | 4. Residue | 5. Type | 6. Back ");
            Console.Write("Select: ");
            int choose = int.Parse(Console.ReadLine());

            if (product != null)
            {
                switch (choose)
                {
                    case 1:
                        Console.Write("Enter name: ");
                        product.Name = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter price: ");
                        product.Price = int.Parse(Console.ReadLine());
                        break;
                    case 3:
                        Console.Write("Enter unit: ");
                        product.Unit = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Enter residue: ");
                        product.Residue = int.Parse(Console.ReadLine());
                        break;
                    case 5:
                        Console.Write("Enter type: ");
                        product.Type = Console.ReadLine();
                        break;
                    case 6:
                        UpdateProduct(table);
                        break;
                    default:
                        EditProduct(table);
                        break;
                }
                ProductRepository.Update(product);
                UpdateProduct();

            }
        }
        private static void ShowUsers()
        {
            var users = userRepo.GetAllUsers();

            var table = new ConsoleTable("№", "First Name", "Last Name", "Role", "Login", "Password");

            foreach (var user in users)
            {
                table.AddRow(user.Id, user.FirstName, user.LastName, user.Role, user.Login, user.Password);
            }

            OptionMenu("Show all users", table);
        }
        private static void EditUser(ConsoleTable table)
        {
            Console.Clear();

            table.Write();

            Console.Write("\nEnter №: ");
            int id = int.Parse(Console.ReadLine());

            var admin = Users.Where(x => x.Id.Equals(id)).FirstOrDefault();

            Console.Clear();
            table.Write();

            Console.WriteLine("\n1. First Name | 2. Last Name | 3. Login | 4. Password | 5. Back ");
            Console.Write("Select: ");
            int choose = int.Parse(Console.ReadLine());

            if (admin != null)
            {
                switch (choose)
                {
                    case 1:
                        Console.Write("Enter name: ");
                        admin.FirstName = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter price: ");
                        admin.LastName = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Enter unit: ");
                        admin.Login = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Enter residue: ");
                        admin.Password = Console.ReadLine();
                        break;
                    default:
                        EditUser(table);
                        break;
                }
                userRepo.EditUser(admin);
                ShowUsers();
            }
        }
        private static void DeleteUser(ConsoleTable table)
        {
            Console.Clear();

            table.Write();

            Console.Write("Enter №: ");
            int id = int.Parse(Console.ReadLine());

            var admin = Users.Where(x => x.Id == id).FirstOrDefault();
            if (admin != null)
            {
                userRepo.RemoveUser(admin);
                ShowUsers();
            }
            else
            {
                Console.WriteLine("Such a product is not available in the basket!");
                DeleteUser(table);
            }
        }
    }
}
