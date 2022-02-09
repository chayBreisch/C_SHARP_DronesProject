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

namespace PL
{
    /// <summary>
    /// Interaction logic for Station.xaml
    /// </summary>
    public partial class Station : Window
    {
        BlApi.IBL BLObject;
        BO.Station stationBL;
        BO.Drone droneBL;
        /// <summary>
        /// constructor add statoin
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneList"></param>
        public Station(BlApi.IBL bl)
        {
            BLObject = bl;
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
        /// <param name="parentWindow"></param>
        public Station(BlApi.IBL bl, BO.Station station)
        {
            InitializeComponent();
            actions.Visibility = Visibility.Visible;
            addStation.Visibility = Visibility.Hidden;
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            stationBL = station;
            idstation.Text = stationBL.ID.ToString();
            nameStation.DataContext = stationBL;
            ChargeSlotsStation.DataContext = stationBL;
            LocationStation.Text = $"{stationBL.Location.Latitude}, {stationBL.Location.Longitude}";
            DroneInStation.ItemsSource = stationBL.DronesInCharge;
        }

        //######################################################
        //add station
        //######################################################

        /// <summary>
        /// add station to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private int getID()
        {
            try
            {
                return int.Parse(idStation.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("id");
            }
        }

        /// <summary>
        /// get location of the station
        /// </summary>
        /// <returns></returns>
        private BO.Location getLocation()
        {
            try
            {
                return new BO.Location(int.Parse(longitudeStation.Text), int.Parse(latitudeStation.Text));
            }
            catch (Exception e)
            {
                throw new InValidInput("location");
            }
        }

        /// <summary>
        /// get model of the station
        /// </summary>
        /// <returns></returns>
        private int getModel()
        {

            if (latitudeStation.Text == "")
               throw new InValidInput("model");
            return int.Parse(latitudeStation.Text);
        }

        /// <summary>
        /// get sum charge slots of the station
        /// </summary>
        /// <returns></returns>
        private int getchargeSlots()
        {
            try
            {
                return int.Parse(chargeSlotsStation.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("chargeSlots");
            }
        }

        /// <summary>
        /// add ststion to dataSource list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddStation(object sender, RoutedEventArgs e)
        {
            try
            {
                BLObject.AddStation(getID(), getModel(), getLocation(), getchargeSlots());
                MessageBox.Show("you added succefuly");
                Close();
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
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
            try
            {
                stationBL.Name = int.Parse(nameStation.Text);
                stationBL.ChargeSlots = int.Parse(ChargeSlotsStation.Text);
                stationBL = BLObject.UpdateStation(stationBL);
                MessageBox.Show("station updated sucssesfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("can not update station");
            }
        }

        /// <summary>
        /// close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickReset(object sender, RoutedEventArgs e)
        {
            idStation.Text = null;
            chargeSlotsStation.Text = null;
            nameStationToAdd.Text = null;
            longitudeStation.Text = null;
            latitudeStation.Text = null;
        }

        /// <summary>
        /// open the drone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDoubleClick_OpenDrone(object sender, RoutedEventArgs e)
        {
            sender.ToString();
            BO.DroneInCharger droneInCharger = (sender as ListView).SelectedValue as BO.DroneInCharger;
            BO.Drone droneBL = BLObject.GetSpecificDroneBL(droneInCharger.ID);
            //this.Visibility = Visibility.Hidden;
            /*new Drone(BLObject, droneBL, this).Show();
            Hide();*/
            //var win = new Drone(BLObject, droneBL);
            Visibility = Visibility.Hidden;
            //win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// remove station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("are you sure you want to remove staton?", "remove station", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    BLObject.RemoveStation(stationBL.ID);
                    //ParentWindow.Show();
                    MessageBox.Show("station deleted sucssesully");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nyou have to release them before");
            }
        }
    }
}