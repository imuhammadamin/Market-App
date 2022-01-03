using System;
using System.IO;
using System.Linq;
using Market_App.Models;
using System.Collections.Generic;
using ConsoleTables;
using System.Data;
using Market_App.Service;

namespace Market_App.Models
{
    internal class Commerce
    {
        public static void Execute()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Browse all products | 2. Add product | 3. Search product | 4. Update product | 5. Basket | 6. Exit");
                Console.Write("Enter your option: ");
                string option = Console.ReadLine();

                switch(option)
                {
                    case "1":
                        ShowProducts();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        SearchProduct();
                        break;
                    case "4":
                        UpdateProduct();
                        break;
                    case "5":
                        ShowBasket();
                        break;
                    case "6":
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
                table.AddRow(product.ID, product.Name, product.Price,product.Unit, product.Residue, product.Type);
            }

            table.Write();
            OptionMenu("Add to Basket");
        }
        private static void AddProduct()
        {
            int count = ProductRepository.GetAllProducts().Count + 1;

            Console.Write("\nEnter product name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter price: ");
            int price = int.Parse(Console.ReadLine());

            Console.Write("Enter unit: ");
            string unit = Console.ReadLine();

            Console.Write("Enter residue: ");
            string residue = Console.ReadLine();

            Console.Write("Enter type: ");
            string type = Console.ReadLine();

            File.AppendAllText(Constants.ProductsDbPath, count + " " + productName + " " + price + " " + unit + " " + residue + " " + type + Environment.NewLine);

            Console.WriteLine("Product added.\n"); 
            OptionMenu("Add product");
        }
        private static void SearchProduct()
        {
            Console.Write("\nEnter the product name: ");
            string nameProduct = Console.ReadLine();
            string nameProduct1 = char.ToUpper(nameProduct[0]) + nameProduct.Substring(1);
            var prod = Sales.GetProductsForSelling().Where(x => x.Name.Equals(nameProduct1));
            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var pr in prod)
                table.AddRow(pr.ID, pr.Name, pr.Price, pr.Unit, pr.Residue, pr.Type);

            table.Write();
            OptionMenu("Add to Basket");
        }
        private static void UpdateProduct()
        {
            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var product in ProductRepository.GetAllProducts())
            {
                table.AddRow(product.ID, product.Name, product.Price, product.Unit, product.Residue, product.Type);
            }

            table.Write();
            
        }
        private static void ShowBasket()
        {
            Console.Clear();
            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");
            
            foreach (var basket in BasketRepository.GetBasket())
            {
                table.AddRow(basket.ID, basket.Name, basket.Price, basket.Unit, basket.Residue, basket.Type);
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
            var product = Sales.GetProductsForSelling().Where(x => x.ID.Equals(id));
            Product prod = new Product();

            Console.Write("Enter the amount you want to buy: ");
            int amount = int.Parse(Console.ReadLine());
            foreach(var pr in product)
            {
                prod.ID = pr.ID;
                prod.Name = pr.Name;
                prod.Price = pr.Price;
                prod.Unit = pr.Unit;
                prod.Residue = amount;
                prod.Type = pr.Type;
            }
            BasketRepository.AddToBasket(prod);
            ShowProducts();
        }
        private static void Calculation(int id, int amount)
        {
            var products = ProductRepository.GetAllProducts();

            foreach (var pr in products)
            {
                if (pr.ID == id)
                {
                    pr.Residue -= amount;
                }
            }
            using (TextWriter tw = new StreamWriter("Data.txt"))
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
                        else if (firstOption == "Add product")
                            AddProduct();
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
            var product = BasketRepository.GetBasket().Where(x => x.ID.Equals(id));
          
            foreach (var pr in product)
            {
                Calculation(id, pr.Residue);
            }
            
            BasketRepository.ClearBasket();
        }
        private static void RemoveFromBasket()
        {
            Console.Write("Enter №: ");
            int id = int.Parse(Console.ReadLine());
            foreach(var product in BasketRepository.GetBasket())
            {
                if (product.ID.Equals(id))
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
