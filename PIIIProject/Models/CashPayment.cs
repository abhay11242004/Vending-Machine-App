using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class CashPayment : Payment
    {
        private int _amount5;
        private int _amount10;
        private int _amount20;
        private int _amount50;
        private int _amount100;

        public CashPayment(List<ShoppingCart> list) : base(list)
        {

        }
        public int AmountReceived
        {
            get { return Amount5 * 5 + Amount10 * 10 + Amount20 * 20 + Amount50 * 50 + Amount100 * 100; }

        }
        public int Amount5
        {
            get { return _amount5; }
            set
            {

                if (_amount5 + value > 0)
                {
                    _amount5 = value;
                }

            }
        }
        public int Amount10
        {
            get { return _amount10; }
            set
            {
                if (_amount10 + value > 0)
                {
                    _amount10 = value;
                }
            }
        }
        public int Amount20
        {
            get { return _amount20; }
            set
            {
                if (_amount20 + value > 0)
                {
                    _amount20 = value;
                }
            }
        }
        public int Amount50
        {
            get { return _amount50; }
            set
            {
                if (_amount50 + value > 0)
                {
                    _amount50 = value;
                }
            }
        }
        public int Amount100
        {
            get { return _amount100; }
            set
            {
                if (_amount100 + value > 0)
                {
                    _amount100 = value;
                }
            }
        }
        public override void Receipt()
        {
            // Create a StringBuilder to build the receipt content
            StringBuilder receiptBuilder = new StringBuilder();

            receiptBuilder.AppendLine("===== Receipt =====");
            receiptBuilder.AppendLine();

            // Items
            receiptBuilder.AppendLine("Items:");
            foreach (var item in _products)
            {
                if (item.SelectedItems > 0)
                {
                    receiptBuilder.AppendLine($"-{item.SelectedItems} {item.Name} ... {item.TotalPrice:c}");
                }

            }
            receiptBuilder.AppendLine();

            receiptBuilder.AppendLine("Payment Type: Cash");
            receiptBuilder.AppendLine();

            receiptBuilder.AppendLine($"Total Cost: {Total:c}");
            receiptBuilder.AppendLine($"Amount Received: {AmountReceived:c}");
            receiptBuilder.AppendLine($"Change: {(AmountReceived - Total):c}");
            receiptBuilder.AppendLine();
            receiptBuilder.AppendLine("Breakdown");

            double[] denominations = { 100, 50, 20, 10, 5, 1, 0.25, 0.10, 0.05, 0.01 }; // Dollar bills and coin denominations
            string[] names = { "Hundred", "Fifty", "Twenty", "Ten", "Five", "One", "Quarter", "Dime", "Nickel", "Penny" };

            double total = AmountReceived - Total;
            for (int i = 0; i < denominations.Length; i++)
            {
                int count = (int)(total / denominations[i]);
                if (count > 0)
                {
                    receiptBuilder.AppendLine($"{count} {names[i]}");
                    total -= count * denominations[i];
                }
            }
            receiptBuilder.AppendLine();
            receiptBuilder.AppendLine("Thank you for using our vending machine");
            receiptBuilder.AppendLine();
            receiptBuilder.AppendLine("===================");

            // Write the content to a file
            File.WriteAllText(_filePath, receiptBuilder.ToString());
        }
    }
}
