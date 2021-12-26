using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IModels
{
    interface IBasket
    {
        public void AddToBasket (string name, int price, int residue, string type, float purchased);
        public void RemoveToBasket(int item);
        public IList<Product> GetBasket();
    }
}
