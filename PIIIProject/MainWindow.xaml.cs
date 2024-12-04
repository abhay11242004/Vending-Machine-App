using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using PIIIProject.Models;
using System.IO;
using PIIIProject.Views;
using PIIIProject.Models;

namespace PIIIProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static List<ShoppingCart> _totalProducts = new List<ShoppingCart>();
        private CashPayment cash;

        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
            cash = new CashPayment(_totalProducts);
            lbProducts.ItemsSource = _totalProducts;
            tbfiveDollarTextBox.Text = $"{cash.Amount5}";
            tbfiftyDollarTextBox.Text = $"{cash.Amount50}";
            tbtwentyDollarTextBox.Text = $"{cash.Amount20}";
            tbtenDollarTextBox.Text = $"{cash.Amount10}";
            tbhundredDollarTextBox.Text = $"{cash.Amount100}";
        }


        private void LoadProducts()
        {

            string[] products = File.ReadAllLines("./../../../Products/snacks.csv");

            foreach (string product in products)
            {
                string[] productDetails = product.Split(',');
                if (productDetails.Length == 4)
                {
                    ShoppingCart a = new ShoppingCart(productDetails[0], int.Parse(productDetails[1]), double.Parse(productDetails[2]), productDetails[3]);
                    _totalProducts.Add(a);
                }

            }
        }
        private void Btn_AddQuantity(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {

                ShoppingCart dataItem = clickedButton.DataContext as ShoppingCart;

                if (dataItem != null)
                {

                    foreach (ShoppingCart data in _totalProducts)
                    {
                        if (data == dataItem)
                        {
                            data.Add++;
                        }
                    }

                }
            }
            lbProducts.Items.Refresh();
            tbTotal.Text = $"{cash.Total:c}";
        }
        private void Btn_SubstractQuantity(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {

                ShoppingCart dataItem = clickedButton.DataContext as ShoppingCart;

                if (dataItem != null)
                {

                    foreach (ShoppingCart data in _totalProducts)
                    {
                        if (data == dataItem)
                        {
                            data.Remove--;
                        }
                    }
                }
            }
            lbProducts.Items.Refresh();
            tbTotal.Text = $"{cash.Total:c}";
        }
        private void AddFiveDollar_Click(object sender, RoutedEventArgs e)
        {
            cash.Amount5++;
            UpdateTextBoxes();
        }
        private void SubtractFiveDollar_Click(object sender, RoutedEventArgs e)
        {
            if (cash.Amount5 > 0)
            {
                cash.Amount5--;
                UpdateTextBoxes();
            }
        }
        private void AddTenDollar_Click(object sender, RoutedEventArgs e)
        {
            cash.Amount10++;
            UpdateTextBoxes();
        }
        private void SubtractTenDollar_Click(object sender, RoutedEventArgs e)
        {
            if (cash.Amount10 > 0)
            {
                cash.Amount10--;
                UpdateTextBoxes();
            }
        }
        private void AddTwentyDollar_Click(object sender, RoutedEventArgs e)
        {
            cash.Amount20++;
            UpdateTextBoxes();
        }
        private void SubtractTwentyDollar_Click(object sender, RoutedEventArgs e)
        {
            if (cash.Amount20 > 0)
            {
                cash.Amount20--;
                UpdateTextBoxes();
            }
        }
        private void AddFiftyDollar_Click(object sender, RoutedEventArgs e)
        {
            cash.Amount50++;
            UpdateTextBoxes();
        }
        private void SubtractFiftyDollar_Click(object sender, RoutedEventArgs e)
        {
            if (cash.Amount50 > 0)
            {
                cash.Amount50--;
                UpdateTextBoxes();
            }
        }
        private void AddHundredDollar_Click(object sender, RoutedEventArgs e)
        {
            cash.Amount100++;
            UpdateTextBoxes();
        }
        private void SubtractHundredDollar_Click(object sender, RoutedEventArgs e)
        {
            if (cash.Amount100 > 0)
            {
                cash.Amount100--;
                UpdateTextBoxes();
            }
        }
        private void UpdateTextBoxes()
        {
            tbfiveDollarTextBox.Text = $"{cash.Amount5}";
            tbfiftyDollarTextBox.Text = $"{cash.Amount50}";
            tbtwentyDollarTextBox.Text = $"{cash.Amount20}";
            tbtenDollarTextBox.Text = $"{cash.Amount10}";
            tbhundredDollarTextBox.Text = $"{cash.Amount100}";
        }

        private void PayWithCash_Click(object sender, RoutedEventArgs e)
        {
            if (cash.Total <= 0)
            {
                MessageBox.Show("You did not buy anything", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cash.AmountReceived <= 0)
            {
                MessageBox.Show("You did not pay", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cash.AmountReceived - cash.Total < 0)
            {
                MessageBox.Show("You did not pay fully", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                cash.Receipt();
                NewProducts();

                Checkout checkout = new Checkout();
                checkout.Show();
                this.Close();
            }
        }
        private void PayWithCard_Click(object sender, RoutedEventArgs e)
        {
            CardPayment card = new CardPayment(_totalProducts);
            if (card.Total <= 5)
            {
                MessageBox.Show("You need to spend more than 5$", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                card.Receipt();
                NewProducts();

                Checkout checkout = new Checkout();
                checkout.Show();
                this.Close();
            }
        }

        public void NewProducts()
        {
            // Create a StringBuilder to build the receipt content
            StringBuilder builder = new StringBuilder();

            foreach (ShoppingCart item in _totalProducts)
            {

                builder.AppendLine($"{item.Name},{item.Quantity - item.SelectedItems},{item.Price},{item.Source}");

            }
            _totalProducts.Clear();

            File.WriteAllText("./../../../Products/snacks.csv", builder.ToString());
        }
    }
}
