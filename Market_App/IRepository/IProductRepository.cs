using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IModels
{
    interface IProductRepository
    {
        private static IList<Product> _allProducts;

        private static void AddAllProducts() { }

        public static IList<Product> GetAllProducts() { return _allProducts; }
    }
}
