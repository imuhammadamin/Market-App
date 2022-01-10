using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Models
{
    internal class History
    {
        [JsonProperty("Customer")]
        public User Customer { get; set; }
        
        [JsonProperty("Products")]
        public IList<Product> Products { get; set; }
        
        [JsonProperty("Date")]
        public DateTime Date { get; set; }
        
        [JsonProperty("Summ")]
        public decimal Summ { get; set; }
    }
}
