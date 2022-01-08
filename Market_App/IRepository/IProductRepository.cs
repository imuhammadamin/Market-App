using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IRepository
{
    interface IProductRepository
    {
        IList<Product> GetAllProducts();
        
        void RemoveProduct(Product product);
        
        void AddProduct(Product product);
        
        void Update(Product product);
        
        void Calculation(IList<Product> products);
    }
}
