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
        private static DbContextApp _Db = new DbContextApp();
        private static IList<Product> _Products;
        public static IList<Product> GetAllProducts()
        {
            _Products = _Db.Products.ToList();
            return _Products;
        }
        
        public static void RemoveProduct(Product product)
        {
            var prod = _Db.Products.FirstOrDefault(x => x.Id == product.Id);
            if (product != null)
            {
                _Db.Products.Remove(prod);
            }
            else
            {
                CommerceAdmin.Execute();
            }
            _Db.SaveChanges();
        }

        public static void Update(Product prod)
        {
            var product = _Products.Where(x => x.Id == prod.Id).FirstOrDefault();
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
                CommerceAdmin.Execute();
            }
            _Db.Products.UpdateRange(_Products);
            _Db.SaveChanges();
        }

        public static void Calculation(int id, float amount)
        {
            var product = _Products.Where(x => x.Id == id).FirstOrDefault();

            if (product != null)
            {
                product.Residue -= amount;
            }
            _Db.UpdateRange(_Products);
            _Db.SaveChanges();
        }

        public static void AddProduct(Product product)
        {
            _Db.Products.Update(product);
            _Db.SaveChanges();
        }
    }
}