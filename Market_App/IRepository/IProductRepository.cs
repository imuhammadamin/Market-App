using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IRepository
{
    interface IProductRepository
    {
        public IList<Product> GetAllProducts();
        public void RemoveProduct(Product product);
        public void AddProduct(Product product);
        public void Update(Product product);
        public void Calculation(int id, float amount);
    }
}
