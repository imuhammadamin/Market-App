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
        private static void SearchProduct()
        {
            Console.Write("\nEnter the product name: ");
            string nameProduct = Console.ReadLine();
            string nameProduct1 = char.ToUpper(nameProduct[0]) + nameProduct.Substring(1);
            var prod = Sales.GetProductsForSelling().Where(x => x.Name.Equals(nameProduct1));
            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var pr in prod)
                table.AddRow(pr.Id, pr.Name, pr.Price, pr.Unit, pr.Residue, pr.Type);

            table.Write();
            OptionMenu("Add to Basket");
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
        private static void AddToBasket(string opt)
        {
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
            }
            BasketRepository.AddToBasket(prod);
            ShowProducts();
        }
        
        public static void OptionMenu(string firstOption)
        {
            Console.WriteLine($"1. {firstOption}");
            if (firstOption == "Add to Basket")
            {
                Console.WriteLine("2. Show Basket");
                Console.WriteLine("3. Back to Menu");
                Console.Write("Enter option: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AddToBasket("search");
                        break;
                    case "2":
                        ShowBasket();
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
            Console.Write("Enter №: ");
            int id = int.Parse(Console.ReadLine());
            var product = BasketRepository.GetBasket().Where(x => x.Id.Equals(id));

            foreach (var pr in product)
            {
                ProductRepository.Calculation(id, pr.Residue);
            }

            BasketRepository.ClearBasket();
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
