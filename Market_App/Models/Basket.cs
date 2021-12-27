using System;
using System.Collections.Generic;
using System.Linq;
using Market_App.IModels;

namespace Market_App.Models
{
    internal class Basket : IBasket
    {
        private IList<Product> _basket = new List<Product>();
        public void AddToBasket(Product product)
        {
            _basket.Add(product);
        }
        public void RemoveToBasket(int item)
        {
            _basket.RemoveAt(item);
        }
        public IList<Product> GetBasket()
        {
            return _basket;
        }
    }
}
