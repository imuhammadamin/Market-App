using ConsoleTables;
using Market_App.Enums;
using Market_App.IRepository;
using Market_App.Service;
using System;
using System.Data;
using System.Linq;
using Market_App.Registration;
using Market_App.Extensions;
using System.Threading;
using Market_App.Repositories;

namespace Market_App.Models
{
    internal class AdminPanel
    {
        static IProductRepository productRepo = new ProductRepository();

        static IUserRepository userRepo = new UserRepository();

        static IHistoryRepository historyRepo = new HistoryRepository();

        static Sales sales = new Sales();

        public void Execute()
        {
            while (true)
            {
                Regist regist = new Regist();
                
                Console.Clear();

                Console.WriteLine("1. Browse all products | 2. Sales information | 3. Create Admin | 4. Show all users | 5. Log out | 6. Exit");
                
                Console.Write("\n> ");
                
                string choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        ShowProducts();
                        break;
                    case "2":
                        SalesInformation();
                        break;
                    case "3":
                        AddAdmin();
                        break;
                    case "4":
                        ShowUsers();
                        break;
                    case "5":
                        regist.Menu();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        CatchErrors.InputError();
                        Execute();
                        break;
                }
            }
        }

        private void AddAdmin()
        {
            try
            {

                Console.Write("Enter First name: ");
                string firstName = Console.ReadLine().Capitalize();

                Console.Write("Enter Last name: ");
                string lastName = Console.ReadLine().Capitalize();

                Console.Write("Enter login: ");
                string login = Console.ReadLine();

                if (!AdminInspection(login))
                {

                    Console.Write("Enter password: ");
                    string password = MethodService.HashPassword(Console.ReadLine());
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
                    Console.WriteLine("Succes!\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("Do you want add admin again? [y, n]: ");
                    string choose = Console.ReadLine();

                    if (choose == "Y" || choose == "y")
                        AddAdmin();
                    else Execute();
                }
                else
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Such a user exists!");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("\nWould you like to try again? [y, n]: ");
                    string choose = Console.ReadLine();

                    if (choose == "y" || choose == "Y") AddAdmin();
                    else Execute();
                }
            }
            catch
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Input error! Please try again.");
                Console.ForegroundColor = ConsoleColor.White;

                Thread.Sleep(1300);

                ShowUsers();
            }
        }
  
        private void ShowProducts()
        {

            Console.Clear();

            var products = productRepo.GetAllProducts();

            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var product in products)
            {
                table.AddRow(product.Id, product.Name, product.Price, product.Unit, product.Residue, product.Type);
            }

            OptionMenu(table);
        }
        
        private void AddProduct()
        {
            try
            {

                var products = productRepo.GetAllProducts();

                Console.Write("\nEnter product name: ");

                string productName = Console.ReadLine();

                var product = products.Where(x => x.Name == productName.Capitalize()).FirstOrDefault();

                if (product != null)
                {
                    Console.Write("Enter price: ");
                    product.Price = decimal.Parse(Console.ReadLine());

                    Console.Write("Enter residue: ");
                    product.Residue = float.Parse(Console.ReadLine());

                    productRepo.Update(product);

                    Console.WriteLine("Product edited.\n");
                }
                else
                {
                    Product product1 = new Product();
                    Console.Write("Enter name: ");
                    product1.Name = Console.ReadLine().Capitalize();

                    Console.Write("Enter price: ");
                    product1.Price = decimal.Parse(Console.ReadLine());

                    Console.Write("Enter unit: ");
                    product1.Unit = Console.ReadLine();

                    Console.Write("Enter residue: ");
                    product1.Residue = float.Parse(Console.ReadLine());

                    Console.Write("Enter type: ");
                    product1.Type = Console.ReadLine().Capitalize();

                    productRepo.AddProduct(product1);

                    Console.WriteLine("Product added.\n");
                }

                ShowProducts();
            }
            catch
            {
                CatchErrors.InputError();
                ShowProducts();
            }
        }
        
        private void UpdateProduct()
        {

            Console.Clear();

            var products = productRepo.GetAllProducts();

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
        
        private void UpdateProduct(ConsoleTable table)
        {
            table.Write();

            Console.WriteLine("\n1. Delete product");
            Console.WriteLine("2. Edit product");
            Console.WriteLine("3. Back to Menu");

            Console.Write("\n> ");

            string choose = Console.ReadLine();
            switch(choose)
            {
                case "1":
                    DeleteProduct(table);
                    break;
                case "2":
                    EditProduct(table);
                    break;
                case "3":
                    Execute();
                    break;
                default:
                    CatchErrors.InputError();
                    UpdateProduct(table);
                    break;
            }
        }
        
        private void SearchProduct()
        {
            try
            {

                Console.Write("\nEnter the product name: ");

                string nameProduct = Console.ReadLine();

                var products = sales.GetProductsForSelling().Where(x => x.Name.Contains(nameProduct.Capitalize())).ToList();

                if (products == null)
                {
                    products = sales.GetProductsForSelling().Where(x => x.Name.Contains(nameProduct)).ToList();
                }

                var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

                foreach (var product in products)
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Unit, product.Residue, product.Type);
                }

                OptionMenu("Search product", table);
            }
            catch
            {
                CatchErrors.InputError();

                ShowProducts();
            }
        }
        
        private void DeleteProduct(ConsoleTable table)
        {
            try
            {

                Console.Clear();

                table.Write();

                Console.Write("Enter №: ");
                int id = int.Parse(Console.ReadLine());

                var product = productRepo.GetAllProducts().Where(x => x.Id == id).FirstOrDefault();

                if (product != null)
                {
                    productRepo.RemoveProduct(product);
                    ShowProducts();
                }
                else
                {
                    Console.WriteLine("Such a product is not available in the basket!");
                    DeleteProduct(table);
                }
            }
            catch
            {
                CatchErrors.InputError();

                UpdateProduct(table);
            }
        }
        
        private void OptionMenu(ConsoleTable table)
        {
            Console.Clear();

            table.Write();

            Console.WriteLine("\n1. Add product");
            Console.WriteLine("2. Update product");
            Console.WriteLine("3. Search product");
            Console.WriteLine("4. Back to Menu");
            Console.Write("\n> ");

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
                    Execute();
                    break;
                default:
                    CatchErrors.InputError();
                    ShowProducts();
                    break;
            }
        }
        
        private void OptionMenu(string option, ConsoleTable table)
        {
            if (option == "Search product")
            {
                Console.Clear();

                table.Write();

                Console.WriteLine("\n1. Show all products");
                Console.WriteLine("2. Update product");
                Console.WriteLine("3. Delete product");
                Console.WriteLine("4. Back to Menu");
                Console.Write("\n> ");

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
                        CatchErrors.InputError();
                        OptionMenu(option, table);
                        break;
                }
            }

            else if (option == "Show all users")
            {
                Console.Clear();

                table.Write();

                Console.WriteLine("\n1. Create admin");
                Console.WriteLine("2. Edit");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. Back to Menu");
                Console.Write("\n> ");

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
                        CatchErrors.InputError();
                        OptionMenu(option, table);
                        break;
                }
            }
        }
        
        private void EditProduct(ConsoleTable table)
        {
            try
            {

                Console.Clear();

                table.Write();

                Console.Write("\nEnter №: ");

                int id = int.Parse(Console.ReadLine());

                var product = productRepo.GetAllProducts().Where(x => x.Id.Equals(id)).FirstOrDefault();

                Console.Clear();

                table.Write();

                Console.WriteLine("\n1. Name | 2. Price | 3. Unit | 4. Residue | 5. Type | 6. Back ");

                Console.Write("\n> ");

                int choose = int.Parse(Console.ReadLine());

                if (product != null)
                {
                    switch (choose)
                    {
                        case 1:
                            Console.Write("Enter name: ");
                            product.Name = Console.ReadLine().Capitalize();
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
                            product.Type = Console.ReadLine().Capitalize();
                            break;
                        case 6:
                            UpdateProduct(table);
                            break;
                        default:
                            EditProduct(table);
                            break;
                    }

                    productRepo.Update(product);

                    UpdateProduct();

                }
            }
            catch
            {
                CatchErrors.InputError();

                UpdateProduct();
            }
        }
        
        private void ShowUsers()
        {
            var users = userRepo.GetAllUsers();

            var table = new ConsoleTable("№", "First Name", "Last Name", "Role", "Login", "Password");

            foreach (var user in users)
            {
                table.AddRow(user.Id, user.FirstName, user.LastName, user.Role, user.Login, user.Password);
            }

            OptionMenu("Show all users", table);
        }
        
        private void EditUser(ConsoleTable table)
        {
            try
            {

                Console.Clear();

                table.Write();

                Console.Write("\nEnter №: ");

                int id = int.Parse(Console.ReadLine());

                var admin = userRepo.GetAllUsers().Where(x => x.Id.Equals(id)).FirstOrDefault();

                Console.Clear();

                table.Write();

                Console.WriteLine("\n1. First Name | 2. Last Name | 3. Login | 4. Password | 5. Back ");

                Console.Write("\n> ");

                int choose = int.Parse(Console.ReadLine());

                if (admin != null)
                {
                    switch (choose)
                    {
                        case 1:
                            Console.Write("First name: ");
                            admin.FirstName = Console.ReadLine().Capitalize();
                            break;
                        case 2:
                            Console.Write("Last name: ");
                            admin.LastName = Console.ReadLine().Capitalize();
                            break;
                        case 3:
                            Console.Write("Login: ");
                            admin.Login = Console.ReadLine();
                            break;
                        case 4:
                            Console.Write("Password: ");
                            admin.Password = MethodService.HashPassword(Console.ReadLine());
                            break;
                        default:
                            EditUser(table);
                            break;
                    }
                    userRepo.EditUser(admin);
                    ShowUsers();
                }
            }
            catch
            {
                CatchErrors.InputError();

                ShowUsers();
            }
        }
        
        private void DeleteUser(ConsoleTable table)
        {
            try
            {

                Console.Clear();

                table.Write();

                Console.Write("Enter №: ");

                int id = int.Parse(Console.ReadLine());

                var admin = userRepo.GetAllUsers().Where(x => x.Id == id).FirstOrDefault();

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
            catch
            {
                CatchErrors.InputError();

                ShowUsers();
            }
        }
        
        private bool AdminInspection(string login)
        {
            return userRepo.GetAllUsers().Any(x => x.Login == login);
        }

        private void SalesInformation()
        {
            ConsoleTable table = new ConsoleTable("№", "Customer", "Product name", "Product price", "Product residue", "Summ", "Date");
            var histories = historyRepo.GetHistories();
            decimal totalSumm = 0;

            foreach(var history in histories)
            {
                for (int i = 0; i < history.Products.Count; i++)
                {
                    if (i == 0)
                    {
                        table.AddRow(
                      history.Customer.Id,
                      history.Customer.FirstName,
                      history.Products[i].Name,
                      history.Products[i].Price,
                      history.Products[i].Residue,
                      history.Products[i].Price * (decimal)history.Products[i].Residue,
                        history.Date);
                    }
                    else
                    {
                        table.AddRow(
                      " ",
                      "" ,
                      history.Products[i].Name,
                      history.Products[i].Price,
                      history.Products[i].Residue,
                      history.Products[i].Price * (decimal)history.Products[i].Residue, " ");
                    }

                    totalSumm += history.Products[i].Price * (decimal)history.Products[i].Residue;
                }

                table.AddRow(" ", " ", " ", " ", "Total summ: ", totalSumm, " ");
            }

            Console.Clear();

            table.Write();

            Console.WriteLine("\n0. Back\n");

            Console.Write("> ");

            Console.ReadLine();
        }

    }
}
