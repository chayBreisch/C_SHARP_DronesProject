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
        /// <summary>
        /// constructor add statoin
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneList"></param>
        public Station(BlApi.Bl bl, StationList stationList)
        {
            StationList = stationList;
            blStation = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            addStation.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// constructor actions
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="station"></param>
        /// <param name="stationList"></param>
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

        //######################################################
        //add station
        //######################################################

        /// <summary>
        /// add station to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddStation(object sender, RoutedEventArgs e)
        {

            int id;
            bool success = Int32.TryParse(idStation.Text, out id);
            int chargeSlots;
            bool success1 = Int32.TryParse(chargeSlotsStation.Text, out chargeSlots);
            int longitude;
            bool success2 = Int32.TryParse(longitudeStation.Text, out longitude);
            int latitude;
            bool success3 = Int32.TryParse(latitudeStation.Text, out latitude);
            int Model;
            bool success4 = Int32.TryParse(latitudeStation.Text, out Model);
            if (!success)
            {
                MessageBox.Show("not valid id input");
            }
            else if (!success1)
            {
                MessageBox.Show("not valid charge slots input");
            }
            else if (!success2)
            {
                MessageBox.Show("not valid longitude input");
            }
            else if (!success3)
            {
                MessageBox.Show("not valid latitude input");
            }
            else if (!success4)
            {
                MessageBox.Show("not valid model input");
            }
            else
            {
                try
                {
                    blStation.addStation(id, Model, new BO.LocationBL(longitude, latitude), chargeSlots);
                    MessageBox.Show("you added succefuly");
                    StationList.Show();
                    Close();
                }
                catch (Exception exce)
                {
                    MessageBox.Show(exce.Message);
                }
            }

        }


        //######################################################
        //actions
        //######################################################

        /// <summary>
        /// update the drone model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_ClickStation(object sender, RoutedEventArgs e)
        {
            stationBL = blStation.updateDataStation(stationBL.ID, int.Parse(nameStation.Text), int.Parse(ChargeSlotsStation.Text));
        }

        /// <summary>
        /// close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            StationList.Show();
            Close();
        }

       
    }
}
