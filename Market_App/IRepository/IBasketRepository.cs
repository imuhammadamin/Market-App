using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IModels
{
    interface IBasketRepository
    {
        private static IList<Product> _basket;
        public static void AddToBasket(Product product) { }
        public static bool RemoveFromBasket(Product product) { return false; }
        public static IList<Product> GetBasket() { return _basket; }
        public static void ClearBasket() { }
    }
}
