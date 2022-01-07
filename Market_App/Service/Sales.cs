using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.Models
{
    internal class Sales
    {
        private IList<Product> _sellingProducts = new List<Product>();

        private static ProductRepository productRepository = new ProductRepository();
        private void SellingProducts()
        {
            _sellingProducts.Clear();
            foreach (Product res in productRepository.GetAllProducts())
            {
                if (res.Price < 8000)
                    res.Price += 500;
                else if (res.Price < 20000)
                    res.Price += 2000;
                else if (res.Price < 50000)
                    res.Price += 5000;
                else if (res.Price < 80000)
                    res.Price += 10000;

                _sellingProducts.Add(res);
            }
        }
        public IList<Product> GetProductsForSelling()
        {
            SellingProducts();
            return _sellingProducts;
        }
    }
}
