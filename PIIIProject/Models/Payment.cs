using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal abstract class Payment
    {

        protected List<ShoppingCart> _products = new List<ShoppingCart>();
        protected const string _filePath = "./Receipt.txt";

        public Payment(List<ShoppingCart> list)
        {
            _products = list;
        }

        public abstract void Receipt();

        public double Total
        {
            get
            {
                double total = 0;
                foreach (ShoppingCart e in _products)
                {
                    total += e.TotalPrice;
                }
                return total;
            }

        }
    }
}
