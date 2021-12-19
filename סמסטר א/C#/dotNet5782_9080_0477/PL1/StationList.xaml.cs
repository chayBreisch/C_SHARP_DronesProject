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
            foreach (var item in blstationList.getStationToList())
                MyList.Add(item);
            DataContext = MyList;
            //DroneListView.ItemsSource = bl.GetDronesBL();
        }

        private void chargeSlotsFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox options = sender as ComboBox;
            List<StationToList> stations = blstationList.getStationsByChargeSlots(options.SelectedIndex);
            StationListView.ItemsSource = stations;
        }

        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            List<StationToList> stations = blstationList.getStationToList();
            StationListView.ItemsSource = stations;
        }
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        private void StationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            sender.ToString();
            StationToList stationList = (sender as ListView).SelectedValue as StationToList;
            BO.Station station = blstationList.convertStationToListToStationBL(stationList);
            //this.Visibility = Visibility.Hidden;
            new Station(blstationList, station, this).Show();
            Hide();
        }
    }
}
