using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PIIIProject.Models
{
    public class ShoppingCart : Product
    {

        private int _selectedItems = 0;
        private int _itemQuantity;
        public ShoppingCart(string name, int quantity, double price, string source) : base(name, quantity, price, source)
        {

        }
        public int ItemQuantity
        {
            get => _itemQuantity;
            set
            {
                if (value <= Quantity && value >= 0)
                {
                    _itemQuantity = value;
                }

            }
        }
        public int SelectedItems
        {
            get { return _selectedItems; }
        }
        public int Add
        {
            get => ItemQuantity;
            set
            {
                if (_selectedItems + 1 <= Quantity)
                {
                    _selectedItems++;
                    ItemQuantity++;
                }



            }
        }
        public int Remove
        {
            get => ItemQuantity;
            set
            {
                if (_itemQuantity - 1 >= 0)
                {
                    _selectedItems--; ItemQuantity--;
                }

            }
        }
        public override int Quantity
        {
            get => base.Quantity;
            set
            {
                if (base.Quantity + value < 0)
                    throw new Exception("You cannot have a negative quantity");
                base.Quantity += value; ;
            }
        }
        public double TotalPrice
        {
            get
            {
                return Price * _itemQuantity;
            }
        }

    }
}
