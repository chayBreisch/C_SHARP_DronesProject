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

namespace PL1
{
    /// <summary>
    /// Interaction logic for Station.xaml
    /// </summary>
    public partial class Station : Window
    {
        StationList StationList;
        BlApi.Bl blStation;
        BO.Station stationBL;
        public Station(BlApi.Bl bl, BO.Station station, StationList stationList)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            StationList = stationList;
            blStation = bl;
            stationBL = station;
            idstation.Text = stationBL.ID.ToString();
            nameStation.DataContext = stationBL;
            ChargeSlotsStation.DataContext = stationBL;
            LocationStation.Text = $"{stationBL.Location.Latitude}, {stationBL.Location.Longitude}";
        }



        /// <summary>
        /// update the drone model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_ClickStation(object sender, RoutedEventArgs e)
        {
            stationBL = blStation.updateDataStation(stationBL.ID, int.Parse(nameStation.Text), int.Parse(ChargeSlotsStation.Text));
        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            StationList.Show();
            Close();
        }
    }
}
