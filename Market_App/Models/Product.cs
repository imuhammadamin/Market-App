﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Market_App.Models
{
    internal class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }
        public int Residue { get; set; }
        public string Type { get; set; }
        public float Purchased { get; set; }
    }
}
