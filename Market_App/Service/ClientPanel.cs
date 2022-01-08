using ConsoleTables;
using Market_App.IRepository;
using Market_App.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market_App.Registration;
using Market_App.Extensions;

namespace Market_App.Models
{
    internal class ClientPanel
    {
        static IProductRepository productRepo = new ProductRepository();

        static Sales sales = new Sales();

        static Regist regist = new Regist();

        IBasketRepository basketRepository = new BasketRepository();

        public void Execute()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("1. Browse all products | 2. Search product | 3. Basket | 4. Log out | 5. Exit");

                Console.Write("\n> ");

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
                        regist.Menu();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void ShowProducts()
        {

            Console.Clear();

            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var product in sales.GetProductsForSelling())
            {
                table.AddRow(product.Id, product.Name, product.Price, product.Unit, product.Residue, product.Type);
            }

            table.Write();
            OptionMenu("Add to Basket");
        }

        private void SearchProduct()
        {
            try
            {
                Console.Write("\nEnter the product name: ");

                string nameProduct = Console.ReadLine();

                var prod = sales.GetProductsForSelling().Where(x => x.Name.Equals(nameProduct.Capitalize()));

                var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

                foreach (var pr in prod)
                    table.AddRow(pr.Id, pr.Name, pr.Price, pr.Unit, pr.Residue, pr.Type);

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
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInput error! Please enter again.");
                Console.ForegroundColor = ConsoleColor.White;
                SearchProduct();
            }
        }

        private void ShowBasket()
        {
            Console.Clear();

            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            float summ = 0;
            decimal summPrice = 0;

            foreach (var basket in basketRepository.GetBasket())
            {
                table.AddRow(basket.Id, basket.Name, basket.Price, basket.Unit, basket.Residue, basket.Type);
                summ = (float)basket.Price * basket.Residue;
                summPrice += (decimal)summ;
            }
            table.AddRow(" ", "Summ: ", summPrice, " ", " ", " ");

            table.Write();

            if (basketRepository.GetBasket().Count == 0)
                OptionMenu("Show all products");
            else OptionMenu("Buy");
        }

        private void AddToBasket(string type)
        {
            try
            {
                Console.Write("Enter №: ");
                int id = int.Parse(Console.ReadLine());

                var product = sales.GetProductsForSelling().Where(x => x.Id.Equals(id)).FirstOrDefault();

                Product prod = new Product();
                
                if (product == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo such product available!\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    AddToBasket("");
                }

                Console.Write("Enter the amount you want to buy: ");

                float amount = float.Parse(Console.ReadLine());

                prod.Id = product.Id;
                prod.Name = product.Name;
                prod.Price = product.Price;
                prod.Unit = product.Unit;
                prod.Residue = product.Residue;
                prod.Type = product.Type;

                if (amount <= prod.Residue && amount > 0)
                {
                    prod.Residue = amount;

                    basketRepository.AddToBasket(prod);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThis amount of product is not available\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    AddToBasket("");
                }

                if (type == "Search again")
                    OptionMenu(type);
                else ShowProducts();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInput error! Please enter again.\n");
                Console.ForegroundColor = ConsoleColor.White;

                AddToBasket("");
            }
            
        }

        private void Calculation(int id, float amount)
        {

            var products = productRepo.GetAllProducts();

            foreach (var pr in products)
            {
                if (pr.Id == id)
                    pr.Residue -= amount;
            }

            using (TextWriter tw = new StreamWriter(Constants.ProductsDbPath))
            {
                for (int i = 0; i < products.Count; i++)
                    tw.WriteLine(
                          products[i].Id + " "
                        + products[i].Name + " "
                        + products[i].Price + " "
                        + products[i].Unit + " "
                        + products[i].Residue + " "
                        + products[i].Type);
            }

        }

        private void OptionMenu(string firstOption)
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
                        basketRepository.ClearBasket();
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

        private void Buy()
        {
            Console.Write("Enter №: ");

            int id = int.Parse(Console.ReadLine());

            var product = basketRepository.GetBasket().Where(x => x.Id.Equals(id));

            foreach (var pr in product)
            {
                productRepo.Calculation(id, pr.Residue);
            }

            basketRepository.ClearBasket();

            Console.Write("Do you want to buy all the products?[y, n]: ");

            string choose = Console.ReadLine();

            if (choose == "y" || choose == "Y")
            {
                var prod = basketRepository.GetBasket();

                foreach (var pr in prod)
                {
                    Calculation(pr.Id, pr.Residue);
                }

                basketRepository.ClearBasket();

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Thank you for your purchase!");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Want to buy again? [y, n]: ");

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

        }

        private void RemoveFromBasket()
        {
            Console.Write("Enter №: ");

            int id = int.Parse(Console.ReadLine());

            foreach (var product in basketRepository.GetBasket())
            {
                if (product.Id.Equals(id))
                    if (basketRepository.RemoveFromBasket(product))
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
