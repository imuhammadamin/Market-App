using System;
using System.Collections.Generic;
using System.Linq;
using Market_App.IRepository;

namespace Market_App.Models
{
    internal class BasketRepository : IBasketRepository
    {
        private IList<Product> _basket = new List<Product>();

        public void AddToBasket(Product product)
        {
            _basket.Add(product);
        }
        
        public bool RemoveFromBasket(Product product)
        {
            return _basket.Remove(product);
        }
        
        public IList<Product> GetBasket()
        {
            return _basket;
        }
        
        public void ClearBasket()
        {
            _basket.Clear();
        }
    }
}
