using Market_App.Service;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.Models
{
    internal class Sales
    {
        private IList<Product> SellingProducts()
        {
            DbContextApp _Db = new DbContextApp();

            IList <Product> _products = _Db.Products.ToList();

            foreach (var res in _products)
            {
                if (res.Price < 8000)
                    res.Price += 500;
                else if (res.Price < 20000)
                    res.Price += 2000;
                else if (res.Price < 50000)
                    res.Price += 5000;
                else if (res.Price < 80000)
                    res.Price += 10000;
            }
            return _products;
        }

        public IList<Product> GetProductsForSelling()
        {
            return SellingProducts();
        }
    }
}
