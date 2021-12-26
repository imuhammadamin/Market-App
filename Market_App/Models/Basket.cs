using System;
using System.Collections.Generic;
using System.Linq;
using Market_App.IModels;

namespace Market_App.Models
{
    internal class Basket : IBasket
    {
        private IList<Product> _basket = new List<Product>();
        public void AddToBasket(string name, int price, int residue, string type, float purchased)
        {
            _basket.Add(
                new Product
                {
                    Name = name,
                    Price = price,
                    Residue = residue,
                    Type = type,
                    Purchased = purchased,
                });
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
