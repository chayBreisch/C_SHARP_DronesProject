﻿using System;
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

        BlApi.IBL BLObject;
        BO.Customer customeBL;
        BO.Customer customer = new BO.Customer();
        char CheckIdentity;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customerList"></param>
        public Customer(BlApi.IBL bl)
        {
            BLObject = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            CustomerPage.Visibility = Visibility.Hidden;
            addCustomer.Visibility = Visibility.Visible;
        }


        /*public Customer(BlApi.IBL blobject, char c)
        {
            BLObject = blobject;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            CustomerPage.Visibility = Visibility.Hidden;
            addCustomer.Visibility = Visibility.Visible;
        }*/

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customer"></param>
        /// <param name="customerList"></param>
        public Customer(BlApi.IBL bl, BO.Customer customer, char c)
        {
            CheckIdentity = c;
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

        private string getName()
        {
            if (customerName.Text == "")
                throw new InValidInput("name");
            return customerName.Text;
        }

        private string getPhone()
        {
            if (customerPhone.Text == "")
                throw new InValidInput("phone");
            return customerPhone.Text;
        }

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
        private void Button_ClickAddCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                BLObject.AddCustomer(getID(), getName(), getPhone(), new BO.LocationBL(getLongitude(), getLatitude()));
                MessageBox.Show("succesfull add");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void MouseDoubleClick_Sended(object sender, RoutedEventArgs e)
        {
            sender.ToString();
            BO.ParcelAtCustomer parcelAtCustomer = (sender as ListView).SelectedValue as BO.ParcelAtCustomer;
            BO.Parcel parcelBL = BLObject.GetSpecificParcelBL(parcelAtCustomer.ID);
            BO.Drone drone = BLObject.GetSpecificDroneBLWithDeleted(parcelBL.Drone.ID);
            /*new Parcel(BLObject, parcelBL, new Drone(BLObject, drone, new DroneList(BLObject, new MainWindow()))).Show();
            Hide();*/
            var win = new Parcel(BLObject, parcelBL);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
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
                    MessageBox.Show("customer removed sucssesfully");
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
            theList.ItemsSource = BLObject.GetParcelsToListByCondition(P => { return customeBL.Name == P.NameCustomerReciver || customeBL.Name == P.NameCustomerSender; });
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
            theList.ItemsSource = BLObject.GetParcelsToListByCondition(P => { return customeBL.Name == P.NameCustomerSender; });
        }

        private void Button_ClickIGot(object sender, RoutedEventArgs e)
        {
            theList.ItemsSource = BLObject.GetParcelsToListByCondition(P => { return customeBL.Name == P.NameCustomerReciver; });
        }

        private void getContent(BO.ParcelToList parcel)
        {
            if (parcel.NameCustomerReciver == customeBL.Name)
            {
                parcel.content = "Goted";
            }
            else
            {
                parcel.content = "Collected";
            }
            BLObject.updateParecl(parcel);
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{sender.ToString().Substring(sender.ToString().IndexOf(':') + 1, "Collected".Length)}");
            cl((CheckBox)sender);
        }

        private void cl(CheckBox c)
        {
            c.IsEnabled = false;
        }
    }
}
