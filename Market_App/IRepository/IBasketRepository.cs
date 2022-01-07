using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IRepository

{
    interface IBasketRepository
    {
        public void AddToBasket(Product product);
        public bool RemoveFromBasket(Product product);
        public IList<Product> GetBasket();
        public void ClearBasket();
    }
}
