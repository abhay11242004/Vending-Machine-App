using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using PIIIProject.Models;
using PIIIProject.Views;

namespace PIIIProject.Views
{
    /// <summary>
    /// Logique d'interaction pour Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {

        public Checkout()
        {
            InitializeComponent();
            tbReceipt.Text = Receipt();
            
   
        }
        private string Receipt()
        {
            using (StreamReader s = new StreamReader("./Receipt.txt"))
            {
                return s.ReadToEnd();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
