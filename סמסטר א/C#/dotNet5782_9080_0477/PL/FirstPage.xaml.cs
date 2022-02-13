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
        BlApi.IBL BLObject;
        Window ParentWindow;
        PLLists PLLists;
        public FirstPage(BlApi.IBL blobject, Window parentWindow, PLLists pllists)
        {
            InitializeComponent();
            BLObject = blobject;
            ParentWindow = parentWindow;
            PLLists = pllists;
        }

        private void Button_ClickNewCustomer(object sender, RoutedEventArgs e)
        {
            new Customer(BLObject, PLLists).Show();
                    this.Close();
        }

        private void Button_ClickCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                new Checking(BLObject, 'c', PLLists).Show();
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
                new Checking(BLObject, 'w', PLLists).Show();
                this.Close();

            }
            catch
            {

            }
        }
    }
}