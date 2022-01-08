using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IRepository

{
    interface IBasketRepository
    {
        void AddToBasket(Product product);
        
        bool RemoveFromBasket(Product product);
        
        IList<Product> GetBasket();
        
        void ClearBasket();
    }
}
