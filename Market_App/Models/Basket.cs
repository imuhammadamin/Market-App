using System;
using System.Collections.Generic;
using System.Linq;
using Market_App.IModels;

namespace Market_App.Models
{
    internal class Basket
    {
        private static IList<Product> _basket = new List<Product>();
        public static void AddToBasket(Product product)
        {
            _basket.Add(product);
        }
        public static void RemoveToBasket(int item)
        {
            _basket.RemoveAt(item);
        }
        public static IList<Product> GetBasket()
        {
            return _basket;
        }
    }
}
