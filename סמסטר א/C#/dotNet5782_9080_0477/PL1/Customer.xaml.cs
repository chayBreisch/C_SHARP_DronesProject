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
        Parcel parcelWindow;
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
            idcustomer.Text = customeBL.ID.ToString();
            nameCustomer.Text = customeBL.Name.ToString();
            phoneCustomr.Text = customeBL.Phone.ToString();
            SumOfparcelSendedByCustomer.Text = customeBL.parcelSendedByCustomer.Count.ToString();
            SumOfparcelSendedToCustomer.Text = customeBL.parcelSendedToCustomer.Count.ToString();
        }

        public Customer(BlApi.Bl bl, BO.Customer customer, Parcel parcel)
        {
            parcelWindow = parcel;
            blCustomer = bl;
            customeBL = customer;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            addCustomer.Visibility = Visibility.Hidden;
            actions.Visibility = Visibility.Visible;
            idcustomer.Text = customeBL.ID.ToString();
            nameCustomer.Text = customeBL.Name.ToString();
            phoneCustomr.Text = customeBL.Phone.ToString();
            SumOfparcelSendedByCustomer.Text = customeBL.parcelSendedByCustomer.Count.ToString();
            SumOfparcelSendedToCustomer.Text = customeBL.parcelSendedToCustomer.Count.ToString();
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
            /*int id;
            string name;
            string phone;
            double longitude;
            double latitude;*/

        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            if (CustomerList != null)
                CustomerList.Show();
            else if (parcelWindow != null)
                parcelWindow.Show();
            this.Close();
        }
    }
}
