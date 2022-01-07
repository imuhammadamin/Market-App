using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public float Residue { get; set; }
        public string Type { get; set; }
        public float Purchased { get; set; }
    }
}
