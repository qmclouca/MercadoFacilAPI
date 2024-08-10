using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(string name, float price, string description, int? periodicity)
        {
            Name = name;
            Price = price;
            Description = description;
            Periodicity = periodicity;
        }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int? Periodicity { get; set; }
    }
}
