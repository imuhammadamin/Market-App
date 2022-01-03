using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Market_App.Service;
using Market_App.IRepository;

namespace Market_App.Models
{
    internal class ProductRepository : IProductRepository
    {   
        private static IList<Product> _allProducts = new List<Product>();

        private static void AddAllProducts()
        {
            _allProducts.Clear();
            string[] lines = File.ReadAllLines(Constants.ProductsDbPath).ToArray();
            int count = 1;
            foreach (string line in lines)
            {
                string[] prod = line.Replace(" ", "|").Split("|");
                _allProducts.Add(new Product()
                {
                    ID = int.Parse(prod[0]),
                    Name = prod[1],
                    Price = int.Parse(prod[2]),
                    Unit = prod[3],
                    Residue = int.Parse(prod[4]),
                    Type = prod[5]
                });
                count++;
            }
        }
        #region GetAllProductsFromDB
        public static IList<Product> GetAllProducts ()
        {
            AddAllProducts();
            return _allProducts;
        }
        #endregion
    }
}