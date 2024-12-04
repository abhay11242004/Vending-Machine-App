using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    public class Product
    { 
        private string _name;
        private int _quantity;
        private double _price;
        private string _source;

        public Product(string name, int quantity, double price, string source)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Source = source;
        }
        public string Name { get => _name; set => _name = value; }
        public virtual int Quantity 
        { 
            get => _quantity;
            set
            {
                if (value < 0)
                    throw new Exception("The quantity cannot be inferior to 0");
                _quantity = value;
            }
        }
        public double Price
        {
            get => _price;
            private set
            {
                if (value < 0)
                    throw new Exception("The quantity cannot be inferior to 0");
                _price = value;
            }
        }
        public string Source { get => _source; set => _source = value; }
    }
}
