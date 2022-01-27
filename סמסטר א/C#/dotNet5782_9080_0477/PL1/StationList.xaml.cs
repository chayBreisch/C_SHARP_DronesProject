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

namespace PL1
{
    /// <summary>
    /// Interaction logic for StationList.xaml
    /// </summary>
    public partial class StationList : Window
    {
        BlApi.IBL blstationList;
        ObservableCollection<StationToList> MyList = new ObservableCollection<StationToList>();


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public StationList(BlApi.IBL bl)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            blstationList = bl;
            foreach (var item in blstationList.GetStationsToList())
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
            IEnumerable<StationToList> stations = blstationList.GetStationsByChargeSlots(options.SelectedIndex);
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
            IEnumerable<StationToList> stations = blstationList.GetStationsToList();
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
            //new MainWindow().Show();
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
            BO.Station station = blstationList.ConvertStationToListToStationBL(stationList);
            //this.Visibility = Visibility.Hidden;
            /*new Station(blstationList, station, this).Show();
            Hide();*/
            var win = new Station(blstationList, station);
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
            /*new Station(blstationList, this).Show();
            Hide();*/
            var win = new Station(blstationList);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /*private void txtFilter_TextChanged()
        {
            // List<BO.Station> stations = blstationList.getStationToList().ChargeSlotsFree >= 0

        }*/

        /*public void Refresh()
        {
            IEnumerable<StationToList> stations;
            if (chargeSlotsFilter.SelectedItem != null)
                stations = blstationList.GetStationsByChargeSlots(chargeSlotsFilter.SelectedIndex);
            else
                stations = blstationList.GetStationsToList();
            StationListView.ItemsSource = stations;
        }*/
        /// <summary>
        /// show deleted stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowDeletedStations(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            IEnumerable<StationToList> stations = blstationList.GetDeletedStationsToList();
            chargeSlotsFilter.SelectedItem = null;
            StationListView.ItemsSource = stations;
        }
    }
}
