using ConsoleTables;
using Market_App.IRepository;
using Market_App.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Models
{
    internal class CommerceUser
    {
        public static void Execute()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Browse all products | 2. Search product | 3. Basket | 4. Exit");
                Console.Write("Enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ShowProducts();
                        break;
                    case "2":
                        SearchProduct();
                        break;
                    case "3":
                        ShowBasket();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        private static void ShowProducts()
        {
            Console.Clear();

            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var product in Sales.GetProductsForSelling())
            {
                table.AddRow(product.Id, product.Name, product.Price, product.Unit, product.Residue, product.Type);
            }

            table.Write();
            OptionMenu("Add to Basket");
        }
<<<<<<< HEAD:Market_App/Service/CommerceUser.cs
=======
        private static void AddProduct()
        {
            int count = ProductRepository.GetAllProducts().Count + 1;

            Console.Write("\nEnter product name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter price: ");
            int price = int.Parse(Console.ReadLine());

            Console.Write("Enter unit[pcs or kgs]: ");
            string unit = Console.ReadLine();

            Console.Write("Enter residue: ");
            string residue = Console.ReadLine();

            Console.Write("Enter type: ");
            string type = Console.ReadLine();

            File.AppendAllText(Constants.ProductsDbPath, count + " " + productName + " " + price + " " + unit + " " + residue + " " + type + Environment.NewLine);

            Console.WriteLine("Product added.\n"); 
            OptionMenu("Add product");
        }
>>>>>>> c4b117fddbe0173f59c41ec95074345dd9e0372a:Market_App/Service/Commerce.cs
        private static void SearchProduct()
        {
            try
            {
                Console.Write("\nEnter the product name: ");
                string nameProduct = Console.ReadLine();
                string nameProduct1 = char.ToUpper(nameProduct[0]) + nameProduct.Substring(1);
                var prod = Sales.GetProductsForSelling().Where(x => x.Name.Equals(nameProduct1));
                var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

<<<<<<< HEAD:Market_App/Service/CommerceUser.cs
            foreach (var pr in prod)
                table.AddRow(pr.Id, pr.Name, pr.Price, pr.Unit, pr.Residue, pr.Type);
=======
                foreach (var pr in prod)
                    table.AddRow(pr.ID, pr.Name, pr.Price, pr.Unit, pr.Residue, pr.Type);
>>>>>>> c4b117fddbe0173f59c41ec95074345dd9e0372a:Market_App/Service/Commerce.cs

                Console.Clear();

                table.Write();
                if (table.Rows.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nProduct not found! Please search again.");
                    Console.ForegroundColor = ConsoleColor.White;
                    OptionMenu("Search again");
                }
                else OptionMenu("Search");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInput error! Please enter again.");
                Console.ForegroundColor = ConsoleColor.White;
                SearchProduct();
            }
        }
        private static void ShowBasket()
        {
            Console.Clear();
            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var basket in BasketRepository.GetBasket())
            {
                table.AddRow(basket.Id, basket.Name, basket.Price, basket.Unit, basket.Residue, basket.Type);
            }

            table.Write();
            if (BasketRepository.GetBasket().Count == 0)
                OptionMenu("Show all products");
            else OptionMenu("Buy");
        }
        private static void AddToBasket(string type)
        {
<<<<<<< HEAD:Market_App/Service/CommerceUser.cs
            Console.Write("Enter №: ");
            int id = int.Parse(Console.ReadLine());
            var product = Sales.GetProductsForSelling().Where(x => x.Id.Equals(id));
            Product prod = new Product();

            Console.Write("Enter the amount you want to buy: ");
            int amount = int.Parse(Console.ReadLine());
            foreach (var pr in product)
            {
                prod.Id = pr.Id;
                prod.Name = pr.Name;
                prod.Price = pr.Price;
                prod.Unit = pr.Unit;
                prod.Residue = amount;
                prod.Type = pr.Type;
=======
            try
            {
                Console.Write("Enter №: ");
                int id = int.Parse(Console.ReadLine());

                var product = Sales.GetProductsForSelling().Where(x => x.ID.Equals(id));
                Product prod = new Product();
                if(id > ProductRepository.GetAllProducts().Count ||
                    id <= ProductRepository.GetAllProducts().Count - ProductRepository.GetAllProducts().Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo such product available!\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    AddToBasket("");
                }
                Console.Write("Enter the amount you want to buy: ");
                float amount = float.Parse(Console.ReadLine());
                    foreach (var pr in product)
                    {
                        prod.ID = pr.ID;
                        prod.Name = pr.Name;
                        prod.Price = pr.Price;
                        prod.Unit = pr.Unit;
                        prod.Residue = pr.Residue;
                        prod.Type = pr.Type;
                        if (amount <= prod.Residue && amount > 0)
                        {
                            prod.Residue = amount;
                            BasketRepository.AddToBasket(prod);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThis amount of product is not available\n");
                            Console.ForegroundColor = ConsoleColor.White;

                            AddToBasket("");
                        }
                    }

                if (type == "Search again")
                    OptionMenu(type);
                else ShowProducts();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInput error! Please enter again.\n");
                Console.ForegroundColor= ConsoleColor.White;
                AddToBasket("");
>>>>>>> c4b117fddbe0173f59c41ec95074345dd9e0372a:Market_App/Service/Commerce.cs
            }
        }
<<<<<<< HEAD:Market_App/Service/CommerceUser.cs
        
        public static void OptionMenu(string firstOption)
=======
        private static void Calculation(int id, float amount)
        {
            var products = ProductRepository.GetAllProducts();

            foreach (var pr in products)
            {
                if(pr.ID == id)
                    pr.Residue -= amount;
            }
            using (TextWriter tw = new StreamWriter(Constants.ProductsDbPath))
            {
                for (int i = 0; i < products.Count; i++)
                    tw.WriteLine(
                          products[i].ID + " "
                        + products[i].Name + " "
                        + products[i].Price + " "
                        + products[i].Unit + " "
                        + products[i].Residue + " "
                        + products[i].Type);
            }

        }
        private static void OptionMenu(string firstOption)
>>>>>>> c4b117fddbe0173f59c41ec95074345dd9e0372a:Market_App/Service/Commerce.cs
        {
            Console.WriteLine($"1. {firstOption}");
            if (firstOption == "Add to Basket")
            {
                Console.WriteLine("2. Search products");
                Console.WriteLine("3. Show Basket");
                Console.WriteLine("4. Back to Menu");
                Console.Write("Enter option: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AddToBasket("");
                        break;
                    case "2":
                        SearchProduct();
                        break;
                    case "3":
                        ShowBasket();
                        break;
                    case "4":
                        Execute();
                        break;
                    default:
                        Console.WriteLine("Please enter only 1, 2, 3 or 4!");
                        OptionMenu(firstOption);
                        break;
                }
            }
            else if (firstOption == "Buy")
            {
                Console.WriteLine("2. Remove from basket");
                Console.WriteLine("3. Remove all products from the basket");
                Console.WriteLine("4. Back to Menu");
                Console.Write("Enter option: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Buy();
                        break;
                    case "2":
                        RemoveFromBasket();
                        break;
                    case "3":
                        BasketRepository.ClearBasket();
                        ShowBasket();
                        break;
                    case "4":
                        Execute();
                        break;
                    default:
                        Console.WriteLine("Please enter only 1, 2, 3 or 4!");
                        OptionMenu(firstOption);
                        break;
                }
            }
            else if (firstOption == "Search")
            {
                Console.WriteLine("2. Add to Basket");
                Console.WriteLine("3. Show all products");
                Console.WriteLine("4. Show Basket");
                Console.WriteLine("5. Back to Menu");
                Console.Write("Enter option: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        SearchProduct();
                        break;
                    case "2":
                        AddToBasket(firstOption);
                        break;
                    case "3":
                        ShowProducts();
                        break;
                    case "4":
                        ShowBasket();
                        break;
                    case "5":
                        Execute();
                        break;
                    default:
                        Console.WriteLine("Please enter only 1, 2, 3, 4 or 5!");
                        OptionMenu(firstOption);
                        break;
                }
            }
            else if (firstOption == "Search again")
            {
                Console.WriteLine("2. Show all products");
                Console.WriteLine("3. Back to Menu");
                Console.Write("Enter option: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        SearchProduct();
                        break;
                    case "2":
                        ShowProducts();
                        break;
                    case "3":
                        Execute();
                        break;
                    default:
                        Console.WriteLine("Please enter only 1, 2 or 3!");
                        OptionMenu(firstOption);
                        break;
                }
            }
            else
            {
                Console.WriteLine("2. Back to Menu");
                Console.Write("Enter option: ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        if (firstOption == "Show all products")
                            ShowProducts();
                        break;
                    case "2":
                        Execute();
                        break;
                    default:
                        Console.WriteLine("Please enter only 1 or 2!");
                        OptionMenu(firstOption);
                        break;
                }
            }
        }
        private static void Buy()
        {
<<<<<<< HEAD:Market_App/Service/CommerceUser.cs
            Console.Write("Enter №: ");
            int id = int.Parse(Console.ReadLine());
            var product = BasketRepository.GetBasket().Where(x => x.Id.Equals(id));

            foreach (var pr in product)
            {
                ProductRepository.Calculation(id, pr.Residue);
            }

            BasketRepository.ClearBasket();
=======
            Console.Write("Do you want to buy all the products?[y, n]: ");
            string choose = Console.ReadLine();

            if (choose == "y" || choose == "Y")
            {
                var product = BasketRepository.GetBasket();

                foreach (var pr in product)
                {
                    Calculation(pr.ID, pr.Residue);
                }

                BasketRepository.ClearBasket();

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Thank you for your purchase!");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Want to buy again?[y, n]");
                string choose1 = Console.ReadLine();
                if (choose1 == "y" || choose1 == "Y")
                    ShowProducts();
                else
                    Environment.Exit(0);
            }
            else if (choose == "n" || choose == "N") OptionMenu("Buy");
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You are entered error! Please enter again.");
                Console.ForegroundColor = ConsoleColor.White;
                Buy();
            }
            
>>>>>>> c4b117fddbe0173f59c41ec95074345dd9e0372a:Market_App/Service/Commerce.cs
        }
        private static void RemoveFromBasket()
        {
            Console.Write("Enter №: ");
            int id = int.Parse(Console.ReadLine());
            foreach (var product in BasketRepository.GetBasket())
            {
                if (product.Id.Equals(id))
                    if (BasketRepository.RemoveFromBasket(product))
                        ShowBasket();
                    else
                    {
                        Console.WriteLine("Such a product is not available in the basket!");
                        RemoveFromBasket();
                    }
            }
        }
    }
}
