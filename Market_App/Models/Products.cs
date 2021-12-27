using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;   

namespace Market_App.Models
{
    internal class Products
    {
        public static IList<Product> allProducts = new List<Product>();
        public static string path = @".\Database\Database.txt";
        public static string[] lines = File.ReadAllLines(path).ToArray();

        #region GetAllProducts
        public static List<Product> GetAllProducts ()
        {
            foreach (string line in lines)
            {
                string[] prod = line.Replace(" ", "").Split('|');
                allProducts.Add(new Product()
                {
                    Name = prod[0],
                    Price = int.Parse(prod[1]),
                    Residue = int.Parse(prod[2]),
                    Type = prod[3]
                });
            }
            return (List<Product>)allProducts;
        }
        #endregion
    }
}