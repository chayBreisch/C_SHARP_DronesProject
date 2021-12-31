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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Window
    {
        BlApi.Bl BLObject;
        Window ParentWindow;
        public FirstPage(BlApi.Bl blobject, Window parentWindow)
        {
            InitializeComponent();
            BLObject = blobject;
            ParentWindow = parentWindow;
        }

        private void Button_ClickNewCustomer(object sender, RoutedEventArgs e)
        {
            new Customer(BLObject, new MainWindow(), 'c').Show();
                    this.Close();
        }

        private void Button_ClickCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                new Checking(BLObject, 'c').Show();
                    this.Close();
            }
            catch
            {

            }
        }

        private void Button_ClickWorker(object sender, RoutedEventArgs e)
        {
            try
            {
                new Checking(BLObject, 'w').Show();
                this.Close();

            }
            catch
            {

            }
        }
    }
}