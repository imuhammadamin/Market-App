using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Market_App.Models
{
    internal class Products
    {
        public static IList<Product> allProducts = new List<Product>();

        public string[] lines = File.ReadAllLines
            (@"D:\Codes\.NET_Projects\Market_App\Database\Database.txt").ToArray();

        #region GetAllProducts
        public void GetAllProducts ()
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
        }
        #endregion
    }
}