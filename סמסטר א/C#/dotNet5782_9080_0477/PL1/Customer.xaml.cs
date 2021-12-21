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

namespace PL1
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {

        BlApi.Bl blCustomer;
        BO.Customer customeBL;
        CustomerList CustomerList;
        BO.Customer customer = new BO.Customer();
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customerList"></param>
        public Customer(BlApi.Bl bl, CustomerList customerList)
        {
            CustomerList = customerList;
            blCustomer = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            addCustomer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customer"></param>
        /// <param name="customerList"></param>
        public Customer(BlApi.Bl bl, BO.Customer customer, CustomerList customerList)
        {
            CustomerList = customerList;
            blCustomer = bl;
            customeBL = customer;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            addCustomer.Visibility = Visibility.Hidden;
            actions.Visibility = Visibility.Visible;
            customerId.Text = customeBL.ID.ToString();
            customerName.Text = customeBL.Name.ToString();
            customerPhone.Text = customeBL.Phone.ToString();
            customerLongitude.Text = customeBL.Location.Longitude.ToString();
            customerLatitude.Text = customeBL.Location.Latitude.ToString();
          


        }

        private void Button_ClickResetAddCustomer(object sender, RoutedEventArgs e)
        {
            customerId.Text = null;
            customerName.Text = null;
            customerPhone.Text = null;
            customerLongitude.Text = null;
            customerLatitude.Text = null;
        }

        private void Button_ClickAddCustomer(object sender, RoutedEventArgs e)
        {
            int id;
            string name;
            string phone;
            double longitude;
            double latitude;

        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            CustomerList.Show();
            this.Close();
        }
    }
}
