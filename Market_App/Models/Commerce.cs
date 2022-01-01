using System;
using System.IO;
using System.Linq;
using Market_App.Models;
using System.Collections.Generic;
using ConsoleTables;
using System.Data;

namespace Market_App.Models
{
    internal class Commerce
    {
        public static void Execute()
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("1. Browse all products | 2. Add product | 3. Search product | 4. Update product | 5. Basket | 6. Exit");
                Console.Write("Enter your option: ");
                string option = Console.ReadLine();

                switch(option)
                {
                    case "1":
                        Products.allProducts.Clear();
                        BrowseProducts();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        SearchProduct();
                        break;
                    case "4":
                        var allProducts = Products.GetAllProducts();
                        for (int i = 0; i < allProducts.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {allProducts[i].Name} | {allProducts[i].Price} | {allProducts[i].Type}");
                        }
                        for (int i = 1; i < allProducts.Count + 1; i++)
                        {
                            if (i == int.Parse(Console.ReadLine()))
                            {
                                Console.Write("\nMake changes to the product [Name, Price, Remainder]: ");
                                string makeProduct = Console.ReadLine();
                            }
                        }
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
        
        private static void BrowseProducts()
        {
            Console.Clear();
            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var product in Sales.GetProductsForSelling())
            {
                table.AddRow(product.ID, product.Name, product.Price,product.Unit, product.Residue, product.Type);
            }

            table.Write();
            SmallMenu("Buy");
        }

        public static void AddProduct()
        {
            Console.Write("\nEnter product: ");

            string product = Console.ReadLine();

            File.AppendAllText(Products.path, product + Environment.NewLine);

            Console.WriteLine("Product added.\n");
            SmallMenu("Add product");
        }

        public static void SearchProduct()
        {
            Console.Write("\nEnter the product name: ");
            string nameProduct = Console.ReadLine();

            var product = Sales.GetProductsForSelling().Where(x => x.Name.Equals(nameProduct));
            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");

            foreach (var pr in product)
                table.AddRow(pr.ID, pr.Name, pr.Price, pr.Unit, pr.Residue, pr.Type);

            table.Write();
            SmallMenu("Buy");
        }

        private static void SmallMenu(string firsOption)
        {
            Console.WriteLine($"1. {firsOption}");
            Console.WriteLine($"2. Back to Menu");

            Console.Write("Enter option: ");
            string option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    if (firsOption == "Add product")
                        AddProduct();
                    else if (firsOption == "Buy")
                        Buy();
                    break;
                case "2":
                    Execute();
                    break;
                default:
                    Console.WriteLine("Please enter only 1 or 2!");
                    SmallMenu(firsOption);
                    break;
            }
        }
        private static void ShowBasket()
        {
            var table = new ConsoleTable("№", "Product Name", "Price", "Unit", "Residue", "Type");
            
            foreach (var basket in Basket.GetBasket())
            {
                table.AddRow(basket.ID , basket.Name, basket.Price, basket.Unit, basket.Residue, basket.Type);
            }

            table.Write();
        }
        private static void Buy()
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
            Calculation(id, amount);
            Basket.AddToBasket(prod);

        }

        private static void Calculation(int id, int amount)
        {
            IList<Product> products = new List<Product>();
            Product product = new Product();

            foreach (var pr in Products.GetAllProducts())
            {
                product = pr;
                if (product.Price < 8000)
                    product.Price -= 500;
                else if (product.Price < 20000)
                    product.Price -= 2000;
                else if (product.Price < 50000)
                    product.Price -= 5000;
                else if (product.Price < 80000)
                    product.Price -= 10000;
                if (product.ID == id)
                {
                    product.Residue -= amount;
                }
                products.Add(product);
            }

            using (TextWriter tw = new StreamWriter("Data.txt"))
            {
                for(int i = 0; i < amount; i++)
                    tw.WriteLine(
                          products[i].ID + " "
                        + products[i].Name + " "
                        + products[i].Price + " "
                        + products[i].Unit + " "
                        + products[i].Residue + " "
                        + products[i].Type);
            }

        }
    }
}