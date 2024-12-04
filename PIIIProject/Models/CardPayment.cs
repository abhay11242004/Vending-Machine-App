using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PIIIProject.Models
{
    internal class CardPayment : Payment
    {
        public CardPayment(List<ShoppingCart> list) : base(list)
        {

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

            receiptBuilder.AppendLine("Payment Type: Card");
            receiptBuilder.AppendLine();

            receiptBuilder.AppendLine($"Total Cost: {Total:c}");

            receiptBuilder.AppendLine();
            receiptBuilder.AppendLine("Thank you for using our vending machine");
            receiptBuilder.AppendLine();
            receiptBuilder.AppendLine("===================");

            // Write the content to a file
            File.WriteAllText(_filePath, receiptBuilder.ToString());
        }
    }
}
