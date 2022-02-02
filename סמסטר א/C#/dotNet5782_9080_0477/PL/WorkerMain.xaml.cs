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
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for WorkerMain.xaml
    /// </summary>
    public partial class WorkerMain : Window
    {
        BlApi.IBL BLObject;
        Window ParentWindow;
        PLLists PLLists;

        /// <summary>
        /// constructor
        /// </summary>
        public WorkerMain(BlApi.IBL blobject, Window parentWindow)
        {
            try
            {
                InitializeComponent();
                BLObject = blobject;
                ParentWindow = parentWindow;
                PLLists = new PLLists();
            }
            catch (ExceptionsBL.CantReturnBLObject e)
            {
                throw new ExceptionsBL.CantReturnBLObject(e);
            }
        }

        /// <summary>
        /// open the droneList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DroneList(object sender, RoutedEventArgs e)
        {
            new DroneList(BLObject, PLLists).Show();
            Hide();
        }

        /// <summary>
        /// open the StationList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickStationList(object sender, RoutedEventArgs e)
        {
            new StationList(BLObject).Show();
            Hide();
        }

        /// <summary>
        /// open the ParcelList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickParcelList(object sender, RoutedEventArgs e)
        {
            new ParcelList(BLObject).Show();
            Hide();
        }

        /// <summary>
        /// open the CustomerList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCustomerList(object sender, RoutedEventArgs e)
        {
            new CustomerList(BLObject, PLLists).Show();
            Hide();
        }
    }
}
