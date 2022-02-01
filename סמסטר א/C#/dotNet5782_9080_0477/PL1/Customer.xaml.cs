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

        BlApi.IBL blCustomer;
        BO.Customer customeBL;
        BO.Customer customer = new BO.Customer();
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customerList"></param>
        public Customer(BlApi.IBL bl)
        {
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
        public Customer(BlApi.IBL bl, BO.Customer customer)
        {
            blCustomer = bl;
            customeBL = customer;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            addCustomer.Visibility = Visibility.Hidden;
            actions.Visibility = Visibility.Visible;
            idcustomer.Text = customeBL.ID.ToString();
            nameCustomer.Text = customeBL.Name.ToString();
            phoneCustomr.Text = customeBL.Phone.ToString();
            longCustomer.Text = customeBL.Location.Longitude.ToString();
            latCustomere.Text = customeBL.Location.Latitude.ToString();
            sendedBy.ItemsSource = customeBL.parcelSendedByCustomer;
            sendedTo.ItemsSource = customeBL.parcelSendedToCustomer;
        }

        /*  public Customer(BlApi.Bl bl, BO.Customer customer, Window ParentWindow)
          {
              parentWindow = ParentWindow;
              blCustomer = bl;
              customeBL = customer;
              InitializeComponent();
              WindowStyle = WindowStyle.None;
              addCustomer.Visibility = Visibility.Visible;
              actions.Visibility = Visibility.Hidden;
              idcustomer.Text = customeBL.ID.ToString();
              nameCustomer.Text = customeBL.Name.ToString();
              phoneCustomr.Text = customeBL.Phone.ToString();
              longCustomer.Text = customeBL.Location.Longitude.ToString();
              latCustomere.Text = customeBL.Location.Latitude.ToString();
              sendedBy.ItemsSource = customeBL.parcelSendedByCustomer;
              sendedTo.ItemsSource = customeBL.parcelSendedToCustomer;

  *//*            SumOfparcelSendedByCustomer.Text = customeBL.parcelSendedByCustomer.Count.ToString();
              SumOfparcelSendedToCustomer.Text = customeBL.parcelSendedToCustomer.Count.ToString();*//*
          }*/

        /// <summary>
        /// reset details of customer to add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickResetAddCustomer(object sender, RoutedEventArgs e)
        {
            customerId.Text = null;
            customerName.Text = null;
            customerPhone.Text = null;
            customerLongitude.Text = null;
            customerLatitude.Text = null;
        }

        /// <summary>
        /// get id of customer
        /// </summary>
        /// <returns></returns>
        private ulong getID()
        {
            try
            {
                return ulong.Parse(customerId.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("id");
            }
        }

        /// <summary>
        /// get name of customer
        /// </summary>
        /// <returns></returns>
        private string getName()
        {
            if (customerName.Text == "")
                throw new InValidInput("name");
            return customerName.Text;
        }

        /// <summary>
        /// get phone of customer
        /// </summary>
        /// <returns></returns>
        private string getPhone()
        {
            if (customerPhone.Text == "")
                throw new InValidInput("phone");
            return customerPhone.Text;
        }

        /// <summary>
        /// get longitude of customer
        /// </summary>
        /// <returns></returns>
        private double getLongitude()
        {
            try
            {
                return Convert.ToDouble(customerLongitude.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("longitude");
            }
        }

        /// <summary>
        /// get latitude of customer
        /// </summary>
        /// <returns></returns>
        private double getLatitude()
        {
            try
            {
                return Convert.ToDouble(customerLatitude.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("longitude");
            }
        }

        /// <summary>
        /// add customer to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                blCustomer.AddCustomer(getID(), getName(), getPhone(), new BO.LocationBL(getLongitude(), getLatitude()));
                MessageBox.Show("succesfull add");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDoubleClick_Sended(object sender, RoutedEventArgs e)
        {
            sender.ToString();
            BO.ParcelAtCustomer parcelAtCustomer = (sender as ListView).SelectedValue as BO.ParcelAtCustomer;
            BO.Parcel parcelBL = blCustomer.GetSpecificParcelBL(parcelAtCustomer.ID);
            BO.Drone drone = blCustomer.GetSpecificDroneBLWithDeleted(parcelBL.Drone.ID);
            /*new Parcel(blCustomer, parcelBL).Show();
            Hide();*/
            var win = new Parcel(blCustomer, parcelBL);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// delete customer from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DeleteCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("are you sure you want to remove customer?", "remove customer", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    blCustomer.RemoveCustomer(customeBL.ID);
                    Button_ClickClose(sender, e);
                    /* ParentWindow.Show();
                     Close();*/
                    MessageBox.Show("customer removed sucssesfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("can't remove parcel because it's connected to a drone");
            }
        }
    }
}
