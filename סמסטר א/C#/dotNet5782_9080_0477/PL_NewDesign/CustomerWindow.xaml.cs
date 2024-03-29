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

namespace PL_NewDesign
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BlApi.IBL BLObject;
        Window ParentWindow;
        BO.Customer Customer;
        public CustomerWindow(BlApi.IBL Blobject, Window parentWindow, BO.Customer customer)
        {
            InitializeComponent();
            BLObject = Blobject;
            ParentWindow = parentWindow;
            Customer = customer;
            headTitle.Content += Customer.Name;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ClickMyParcels(object sender, RoutedEventArgs e)
        {
            theList.ItemsSource = BLObject.GetParcelsToListByCondition(P => { return Customer.Name == P.NameCustomerReciver || Customer.Name == P.NameCustomerSender; });
           // foreach(var item in theList)
        }

        private void Button_ClickSendParcel(object sender, RoutedEventArgs e)
        {
            new NewParcel(BLObject, Customer).Show();
        }

        private void Button_ClickISend(object sender, RoutedEventArgs e)
        {
            theList.ItemsSource = BLObject.GetParcelsToListByCondition(P => { return Customer.Name == P.NameCustomerSender; });
        }

        private void Button_ClickIGot(object sender, RoutedEventArgs e)
        {
            theList.ItemsSource = BLObject.GetParcelsToListByCondition(P => { return Customer.Name == P.NameCustomerReciver; });
        }
    }
}
