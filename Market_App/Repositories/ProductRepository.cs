using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Market_App.Service;
using Market_App.IRepository;

namespace Market_App.Models
{
    internal class ProductRepository : IProductRepository
    {
        private DbContextApp _Db = new DbContextApp();

        private IList<Product> _products = new List<Product>();

        public IList<Product> GetAllProducts()
        {
            _products = _Db.Products.ToList();
            return _products;
        }
        
        public void RemoveProduct(Product product)
        {
            AdminPage adminPanel = new AdminPage();
            
            var prod = _Db.Products.FirstOrDefault(x => x.Id == product.Id);
            
            if (product != null)
            {
                _Db.Products.Remove(prod);
            }
            else
            {
                adminPanel.Execute();
            }
            _Db.SaveChanges();
        }

        public void Update(Product prod)
        {
            AdminPage adminPanel = new AdminPage();

            var product = _products.Where(x => x.Id == prod.Id).FirstOrDefault();

            if (product != null)
            {
                product.Name = prod.Name;
                product.Price = prod.Price;
                product.Unit = prod.Unit;
                product.Type = prod.Type;
                product.Residue = prod.Residue;
             }
            else
            {
                adminPanel.Execute();
            }
            _Db.Products.UpdateRange(_products);
            _Db.SaveChanges();
        }

        public void Calculation(IList<Product> products)
        {
            foreach(var product in products)
            {
                var prod = _Db.Products.Where(x => x.Id == product.Id).FirstOrDefault();

                prod.Residue -= product.Residue;
                
                _Db.Products.Update(prod);
                
                _Db.SaveChanges();
            }
        }

        public void AddProduct(Product product)
        {
            _Db.Products.Update(product);
            _Db.SaveChanges();
        }
    }
}