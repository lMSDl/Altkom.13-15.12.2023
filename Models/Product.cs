using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public Product() { }
        public Product(string name, float price, DateTime? expirationdate)
        {

            Name = name;
            Price = price;
            ExpirationDate = expirationdate;
        }
        public Product(string name, float price) : this(name, price, null) { }

        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Summary
        {
            get
            {
                return $"{Name}: {Price}zł";
            }
        }
    }
}
