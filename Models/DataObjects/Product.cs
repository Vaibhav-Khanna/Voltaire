using System;
using System.Collections.Generic;

namespace voltaire.Models
{
    public class Product
    {
     

        public string Name { get; set; }

        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public List<ProductProperty> Properties { get; set; } = new List<ProductProperty>();

    }

}
