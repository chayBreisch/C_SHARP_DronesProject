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

namespace PL1
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        BlApi.Bl blDrone;
        BO.Drone droneBL;
        DroneList DroneList;
        BO.Drone drone = new BO.Drone();
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneList"></param>
        public Drone(BlApi.Bl bl, DroneList droneList)
        {
            DroneList = droneList;
            blDrone = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            addDrone.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="drone"></param>
        /// <param name="droneList"></param>
        public Drone(BlApi.Bl bl, BO.Drone drone, DroneList droneList)
        {
            DroneList = droneList;
            blDrone = bl;
            droneBL = drone;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            addDrone.Visibility = Visibility.Hidden;
            actions.Visibility = Visibility.Visible;
            idDrone.Text = droneBL.ID.ToString();
            modelDrone.DataContext = droneBL;
            batteryDrone.Text = $"{Math.Round(droneBL.BatteryStatus).ToString()}%";
            weightDrone.Text = droneBL.Weight.ToString();
            statusDrone.Text = droneBL.DroneStatus.ToString();
            if (droneBL.parcelInDelivery != null)
                parcelInDeliveryDrone.Text = droneBL.parcelInDelivery.ToString();
            else
                parcelInDeliveryDrone.Text = "no parcel";
            locationDrone.Text = $"{droneBL.Location.Latitude}, {droneBL.Location.Longitude}";
            if (droneBL.DroneStatus != DroneStatus.Available)
            {
                visible.IsChecked = false;
                hidden.IsChecked = true;
            }
            if (droneBL.DroneStatus != DroneStatus.Maintenance)
            {
                visible.IsChecked = true;
                hidden.IsChecked = false;
                TimeChargerBlock.Visibility = Visibility.Hidden;

            }

        }


        //###############################################################################
        //add Drone
        //###############################################################################

        /// <summary>
        /// add drone to the list of drones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            int id;
            bool success = Int32.TryParse(droneId.Text, out id);
           // int weight = options.SelectedIndex;
/*            bool success1 = Int32.TryParse(options.SelectedIndex, out weight);*/
            int number;
            bool success2 = Int32.TryParse(numStationtoChargeDrone.Text, out number);
            string Model = droneMdel.Text;
            if (!success)
            {
                MessageBox.Show("not valid id input");
            }
            /*else if (!success1)
            {
                MessageBox.Show("not valid weight input");
            }*/
            else if (!success2)
            {
                MessageBox.Show("not valid station input");
            }
            else
            {
                try
                {
                    blDrone.addDrone(id, Model, 1, number);
                    MessageBox.Show("you added succefuly");
                    DroneList.Show();
                    Close();
                }
                catch (Exception exce)
                {
                    MessageBox.Show(exce.Message);
                }
            }
        }

        /// <summary>
        /// close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickResetAddDrone(object sender, RoutedEventArgs e)
        {
            droneId.Text = "";
            droneMdel.Text = "";
            droneWeight.Text = "";
            numStationtoChargeDrone.Text = "";
        }


        //###########################################################
        //actions
        //###########################################################

        /// <summary>
        /// update the drone model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            droneBL = blDrone.updateDataDroneModel(droneBL.ID, modelDrone.Text);
        }

        /// <summary>
        /// send drone to charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            droneBL = blDrone.updateSendDroneToCharge(droneBL.ID);
            statusDrone.Text = droneBL.DroneStatus.ToString();
            /*visible.Visibility = Visibility.Hidden;
            hidden.Visibility = Visibility.Visible;*/
            visible.IsChecked = false;
            hidden.IsChecked = true;

            /*Charge.IsEnabled = false;
            Connect.IsEnabled = false;
            Collect.IsEnabled = false;
            Supply.IsEnabled = false;
            UnCharge.IsEnabled = true;*/
            //TimeCharger.Visibility = Visibility.Visible;
            TimeChargerBlock.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// uncharge drone 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnCharge_Click(object sender, RoutedEventArgs e)
        {
            double time;
            bool success = double.TryParse(TimeCharger.Text, out time);
            if (!success)
                MessageBox.Show("invalid time");
            else
            {
                /*visible.Visibility = Visibility.Visible;
                hidden.Visibility = Visibility.Hidden;*/
                visible.IsChecked = true;
                hidden.IsChecked = false;

                /*Charge.IsEnabled = true;
                Connect.IsEnabled = true;
                Collect.IsEnabled = true;
                Supply.IsEnabled = true;
                UnCharge.IsEnabled = false;*/
                //TimeCharger.Visibility = Visibility.Hidden;
                TimeChargerBlock.Visibility = Visibility.Hidden;
                try
                {
                    droneBL = blDrone.updateUnchargeDrone(droneBL.ID, time);
                    statusDrone.Text = droneBL.DroneStatus.ToString();
                    batteryDrone.Text = $"{Math.Round(droneBL.BatteryStatus).ToString()}%";
                    TimeCharger.Text = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// connect the drone to a parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            try
            {
                blDrone.updateConnectParcelToDrone(droneBL.ID);
            }
            catch (Exception ex)
            {
                check = false;
                MessageBox.Show(ex.Message.ToString());
            }
            if (check)
                MessageBox.Show("connected succesfully");
        }

        /// <summary>
        /// collect a parcel with the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Collect_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            try
            {
                blDrone.updateCollectParcelByDrone(droneBL.ID);
            }
            catch (Exception ex)
            {
                check = false;
                MessageBox.Show(ex.Message.ToString());
            }
            if (check)
                MessageBox.Show("collected succesfully");
        }

        /// <summary>
        /// suplly a parcel with the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supply_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            try
            {
                blDrone.updateSupplyParcelByDrone(droneBL.ID);
            }
            catch (Exception ex)
            {
                check = false;
                MessageBox.Show(ex.Message.ToString());
            }
            if (check)
                MessageBox.Show("supplied succesfully");
        }

        /// <summary>
        /// close the drone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            DroneList.Show();
            Close();
        }
    }
}

