using System;
using System.Collections.Generic;
using System.Linq;
using Market_App.IModels;

namespace Market_App.Models
{
    internal class Product : IProduct
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Residue { get; set; }
        public string Type { get; set; }
        public float Purchased { get; set; }
    }
}
