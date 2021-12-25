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
        BlApi.Bl blstationList;
        MainWindow mainWindow;
        ObservableCollection<StationToList> MyList = new ObservableCollection<StationToList>();


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public StationList(BlApi.Bl bl, MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            WindowStyle = WindowStyle.None;
            blstationList = bl;
            foreach (var item in blstationList.GetStationToList())
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
            IEnumerable<StationToList> stations = blstationList.GetStationToList();
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
            mainWindow.Show();
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
            new Station(blstationList, station, this).Show();
            Hide();
        }

        /// <summary>
        /// pass to add drone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddStation(object sender, RoutedEventArgs e)
        {
            new Station(blstationList, this).Show();
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
                stations = blstationList.GetStationsByChargeSlots(chargeSlotsFilter.SelectedIndex);
            else
                stations = blstationList.GetStationToList();
            StationListView.ItemsSource = stations;
        }
    }
}
