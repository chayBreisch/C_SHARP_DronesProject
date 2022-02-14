﻿using System;
using BO;
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
using System.Collections.ObjectModel;
using DO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    /// 

    public partial class DroneList : Window
    {
        BlApi.IBL BLObject;
        internal static PLLists PLLists;
        CollectionView view;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public DroneList(BlApi.IBL bl, PLLists plLists)
        {
            InitializeComponent();
            PLLists = plLists;
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            DataContext = PLLists.Drones;
            statusFilter.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            weightFilter.ItemsSource = BLObject.GetweightCategoriesEnumItem();
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }

        /// <summary>
        /// show the list of the drones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowListDrones(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            statusFilter.SelectedItem = null;
            weightFilter.SelectedItem = null;
            DroneListView.DataContext = PLLists.Drones;
            if (view != null)
            {
                view.GroupDescriptions.Clear();
                DataContext = PLLists.Drones;
                view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            }
        }

        /// <summary>
        /// show the drones with the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSelectDrone(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<DroneToList> drones = new List<DroneToList>();
            ComboBox options = sender as ComboBox;
            if (weightFilter.SelectedItem == null)
                DroneListView.DataContext = PLLists.Drones.Where(drone => drone.DroneStatus == (DroneStatus)(statusFilter.SelectedIndex));
            else if(statusFilter.SelectedItem == null)
                DroneListView.DataContext = PLLists.Drones.Where(drone => drone.Weight == (WeightCatagories)(weightFilter.SelectedIndex + 1));
            else
                DroneListView.DataContext = PLLists.Drones.Where(drone => drone.Weight == (WeightCatagories)(weightFilter.SelectedIndex + 1) && drone.DroneStatus == (DroneStatus)(statusFilter.SelectedIndex));
            DataContext = PLLists.Drones;//fix
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }

        /// <summary>
        /// open the addDrone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            var win = new Drone(BLObject, PLLists);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;

        }

        /// <summary>
        /// open the drone window with the specific drone that clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            sender.ToString();
            Drone_ droneToList = (sender as ListView).SelectedValue as Drone_;
            BO.Drone drone = BLObject.GetSpecificDroneBL(droneToList.ID);
            new Drone(BLObject, drone, PLLists).Show();
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// grouping list by status
        /// </summary>
        private void Button_Click_GroupingStatus(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("DroneStatus");
                view.GroupDescriptions.Add(property);
            }
        }

        /// <summary>
        /// grouping list by weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickGroupingWeight(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("Weight");
                view.GroupDescriptions.Add(property);
            }
        }

        /// <summary>
        /// show deleted customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowDeletedCustomers(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            statusFilter.SelectedItem = null;
            weightFilter.SelectedItem = null;
            DroneListView.DataContext = PLLists.Drones.Where(D => D.IsActiv == false);
            if (view != null)
            {
                view.GroupDescriptions.Clear();
                DataContext = PLLists.Drones;
                view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            }
        }
    }
}