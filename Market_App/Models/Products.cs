using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;   

namespace Market_App.Models
{
    internal class Products
    {
        public static string path = "Database.txt";
        
        public static IList<Product> allProducts = new List<Product>();


        #region GetAllProductsFromDB
        public static IList<Product> GetAllProducts ()
        {
            string[] lines = File.ReadAllLines(path).ToArray();
            int count = 1;
            foreach (string line in lines)
            {
                string[] prod = line.Replace(" ", "|").Split("|");
                allProducts.Add(new Product()
                {
                    ID = count,
                    Name = prod[0],
                    Price = int.Parse(prod[1]),
                    Unit = prod[2],
                    Residue = int.Parse(prod[3]),
                    Type = prod[4]
                });
                count++;
            }
            return allProducts;
        }
        #endregion
    }
}