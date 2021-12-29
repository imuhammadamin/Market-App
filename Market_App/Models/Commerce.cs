using System;
using System.IO;
using System.Linq;
using Market_App.Models;
using System.Collections.Generic;

namespace Market_App.Models
{
    internal class Commerce
    {
        public static void Execute()
        {
            while (true)
            {
                Console.WriteLine(" 1. Browse all products " + "\n" + " 2. Add product " + "\n" + " 3. Search product " + "\n" + " 4. Update product " + "\n"+" 5. Exit");

                //Console.WriteLine("1. Browse all products | 2. Add product | 3. Search product | 4. Update product | 5. Exit");
                Console.Write("Enter your option: ");
                string option = Console.ReadLine();

                switch(option)
                {
                    case "1":
                        var AllProducts = Products.GetAllProducts();
                        foreach (var product in AllProducts)
                        {
                            Console.WriteLine($"{product.Name} | {product.Price} | {product.Type}");
                        }
                        break;
                    case "2":
                        Console.Write("Enter product: ");
                        AddProduct(Console.ReadLine());
                        break;
                    case "5":
                        Console.Write("Bless you!");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static void AddProduct(string product)
        {
            File.AppendAllText(Products.path, product);
            Console.WriteLine("Product added.");
        }
    }
}
