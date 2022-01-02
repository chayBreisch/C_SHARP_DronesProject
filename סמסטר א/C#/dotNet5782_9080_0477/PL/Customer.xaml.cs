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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {

        BlApi.Bl BLObject;
        BO.Customer customeBL;
        Window ParentWindow;
        BO.Customer customer = new BO.Customer();
        char CheckIdentity;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customerList"></param>
        public Customer(BlApi.Bl bl, Window parentWindow)
        {
            this.ParentWindow = parentWindow;
            BLObject = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            CustomerPage.Visibility = Visibility.Hidden;
            addCustomer.Visibility = Visibility.Visible;
        }


        public Customer(BlApi.Bl blobject, Window parentWindow, char c)
        {
            ParentWindow = parentWindow;
            BLObject = blobject;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            CustomerPage.Visibility = Visibility.Hidden;
            addCustomer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customer"></param>
        /// <param name="customerList"></param>
        public Customer(BlApi.Bl bl, BO.Customer customer, Window parentWindow, char c)
        {
            CheckIdentity = c;
            this.ParentWindow = parentWindow;
            BLObject = bl;
            customeBL = customer;
            InitializeComponent();
            if (CheckIdentity == 'c')
            {
                actions.Visibility = Visibility.Hidden;
                addCustomer.Visibility = Visibility.Hidden;
                CustomerPage.Visibility = Visibility.Visible;
                headTitle.Content += customeBL.Name;
            }
            else
            {
                WindowStyle = WindowStyle.None;
                addCustomer.Visibility = Visibility.Hidden;
                CustomerPage.Visibility = Visibility.Hidden;
                actions.Visibility = Visibility.Visible;
                idcustomer.Text = customeBL.ID.ToString();
                nameCustomer.Text = customeBL.Name.ToString();
                phoneCustomr.Text = customeBL.Phone.ToString();
                longCustomer.Text = customeBL.Location.Longitude.ToString();
                latCustomere.Text = customeBL.Location.Latitude.ToString();
                sendedBy.ItemsSource = customeBL.parcelSendedByCustomer;
                sendedTo.ItemsSource = customeBL.parcelSendedToCustomer;
            }
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
            try
            {
                ulong id = ulong.Parse(customerId.Text);
                string name = Convert.ToString(customerName.Text);
                string phone = Convert.ToString(phoneCustomr.Text);
                double longitude = Convert.ToDouble(customerLongitude.Text);
                double latitude = Convert.ToDouble(customerLatitude.Text);
                BLObject.AddCustomer(id, name, phone, new BO.LocationBL(longitude, latitude));
                MessageBox.Show("succesfull add");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            if (ParentWindow != null)
                ParentWindow.Show();
            else if (ParentWindow != null)
                ParentWindow.Show();
            this.Close();
        }


        private void MouseDoubleClick_Sended(object sender, RoutedEventArgs e)
        {
            sender.ToString();
            BO.ParcelAtCustomer parcelAtCustomer = (sender as ListView).SelectedValue as BO.ParcelAtCustomer;
            BO.Parcel parcelBL = BLObject.GetSpecificParcelBL(parcelAtCustomer.ID);
            BO.Drone drone = BLObject.GetSpecificDroneBLWithDeleted(parcelBL.Drone.ID);
            new Parcel(BLObject, parcelBL, new Drone(BLObject, drone, new DroneList(BLObject, new MainWindow()))).Show();
            Hide();
        }

        private void Button_Click_DeleteCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("are you sure you want to remove customer?", "remove customer", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    BLObject.RemoveCustomer(customeBL.ID);
                    Button_ClickClose(sender, e);
                    /* ParentWindow.Show();
                     Close();*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("can't remove parcel because it's connected to a drone");
            }
        }

        private void Button_ClickMyParcels(object sender, RoutedEventArgs e)
        {
            List<BO.Parcel> parcels = new List<BO.Parcel>(BLObject.GetParcelsByCondition(P => { return customeBL.ID == P.Reciever.ID || customeBL.ID == P.Sender.ID; }));
            theList.ItemsSource = BLObject.GetParcelToListByCondition(P => { return customeBL.Name == P.NameCustomerReciver || customeBL.Name == P.NameCustomerSender; });
            foreach (var item in theList.Items)
            {
                getContent((BO.ParcelToList)item);
            }
        }

        private void Button_ClickSendParcel(object sender, RoutedEventArgs e)
        {
            new Parcel(BLObject, customeBL).Show();

        }

        private void Button_ClickISend(object sender, RoutedEventArgs e)
        {
            theList.ItemsSource = BLObject.GetParcelToListByCondition(P => { return customeBL.Name == P.NameCustomerSender; });
        }

        private void Button_ClickIGot(object sender, RoutedEventArgs e)
        {
            theList.ItemsSource = BLObject.GetParcelToListByCondition(P => { return customeBL.Name == P.NameCustomerReciver; });
        }

        private void getContent(BO.ParcelToList parcel)
        {
            if (parcel.NameCustomerReciver == customeBL.Name)
            {
                parcel.content = "Goted";
                parcel.pathTo = "g";
            }
            else
            {
                parcel.content = "Collected";
                parcel.pathTo = "c";
            }
            BLObject.updateParecl(parcel);
        }

        private void g(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("g");
        }

        private void c(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("c");
        }
    }
}
