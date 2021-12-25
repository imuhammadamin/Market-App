using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.Models
{
    internal class Sales
    {
        public static IList<Product> GetProductsForSelling ()
        {
            IList<Product> result = new List<Product>();
            Product res = new Product();

            foreach (Product pr in Products.allProducts)
            {
                res = pr;
                if (res.Price < 8000)
                    res.Price += 500;
                else if (res.Price < 20000)
                    res.Price += 2000;
                else if (res.Price < 50000)
                    res.Price += 5000;
                else if (res.Price < 80000)
                    res.Price += 10000;

                result.Add(res);
            }

            return result;
        }
    }
}
