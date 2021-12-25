using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.IModels
{
    interface IProduct
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Residue { get; set; }
        public string Type { get; set; }
        public float Purchased { get; set; }
    }
}
