using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for StationList.xaml
    /// </summary>
    public partial class StationList : Window
    {
        BlApi.IBL BLObject;
        internal static PLLists PLLists;
        CollectionView view;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public StationList(BlApi.IBL bl)
        {
            InitializeComponent();
            PLLists = new PLLists();
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            DataContext = PLLists.Stations;
        }

        /// <summary>
        /// show station kist with a filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chargeSlotsFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox options = sender as ComboBox;
            if (options.SelectedIndex == 0)
            {
                StationListView.DataContext = PLLists.Stations.Where(S => S.ChargeSlotsFree == 0);
            }
            else
            {
                StationListView.DataContext = PLLists.Stations.Where(S => S.ChargeSlotsFree > 0);
            }
        }

        /// <summary>
        /// show all list from station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            chargeSlotsFilter.SelectedItem = null;
            StationListView.DataContext = PLLists.Stations;
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
        /// pass to a specific station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            sender.ToString();
            Station_ stationList = (sender as ListView).SelectedValue as Station_;
            BO.Station station = BLObject.GetSpecificStationBL(stationList.ID);
            var win = new Station(BLObject, station, PLLists);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// pass to add drone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddStation(object sender, RoutedEventArgs e)
        {
            var win = new Station(BLObject, PLLists);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// show deleted stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowDeletedStations(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            chargeSlotsFilter.SelectedItem = null;
            StationListView.DataContext = PLLists.Stations.Where(S => S.IsActive == false);
        }
    }
}
