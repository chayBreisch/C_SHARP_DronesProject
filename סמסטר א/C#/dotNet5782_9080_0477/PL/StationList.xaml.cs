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
        Window ParentWindow;
        ObservableCollection<StationToList> MyList = new ObservableCollection<StationToList>();


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public StationList(BlApi.IBL bl, Window main)
        {
            InitializeComponent();
            ParentWindow = main;
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            foreach (var item in BLObject.GetStationsToList())
                MyList.Add(item);
            DataContext = MyList;
            //DroneListView.ItemsSource = bl.GetDronesBL();
        }

        /// <summary>
        /// show station kist with a filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chargeSlotsFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox options = sender as ComboBox;
            IEnumerable<StationToList> stations = BLObject.GetStationsByChargeSlots(options.SelectedIndex);
            StationListView.ItemsSource = stations;
        }

        /// <summary>
        /// show all list from station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            IEnumerable<StationToList> stations = BLObject.GetStationsToList();
            chargeSlotsFilter.SelectedItem = null;
            //weightFilter.SelectedItem = null;
            StationListView.ItemsSource = stations;
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            ParentWindow.Show();
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
            StationToList stationList = (sender as ListView).SelectedValue as StationToList;
            BO.Station station = BLObject.ConvertStationToListToStationBL(stationList);
            //this.Visibility = Visibility.Hidden;
            new Station(BLObject, station, this).Show();
            Hide();
        }

        /// <summary>
        /// pass to add drone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddStation(object sender, RoutedEventArgs e)
        {
            new Station(BLObject, this).Show();
            Hide();
        }



        private void txtFilter_TextChanged()
        {
            // List<BO.Station> stations = blstationList.getStationToList().ChargeSlotsFree >= 0

        }

        public void Refresh()
        {
            IEnumerable<StationToList> stations;
            if (chargeSlotsFilter.SelectedItem != null)
                stations = BLObject.GetStationsByChargeSlots(chargeSlotsFilter.SelectedIndex);
            else
                stations = BLObject.GetStationsToList();
            StationListView.ItemsSource = stations;
        }

        private void Button_Click_ShowDeletedStations(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            IEnumerable<StationToList> stations = BLObject.GetDeletedStationsToList();
            chargeSlotsFilter.SelectedItem = null;
            StationListView.ItemsSource = stations;
        }
    }
}
