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

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        BlApi.IBL BLObject;
        BO.Drone droneBL;
        Window ParentWindow;
        BO.Drone drone = new BO.Drone();
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneList"></param>
        public Drone(BlApi.IBL bl, Window droneList)
        {
            ParentWindow = droneList;
            BLObject = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            addDrone.Visibility = Visibility.Visible;
            droneWeight.ItemsSource = BLObject.GetweightCategoriesEnumItem();
            droneWeight.SelectedItem = BLObject.GetweightCategoriesEnumItem().GetValue(1);
        }

        /// <summary>
        /// constructor actions
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="drone"></param>
        /// <param name="droneList"></param>
        public Drone(BlApi.IBL bl, BO.Drone drone, Window droneList)
        {
            ParentWindow = droneList;
            BLObject = bl;
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
                Supply.IsEnabled = false;
                UnCharge.IsEnabled = true;
            }
            if (droneBL.DroneStatus != DroneStatus.Maintenance)
            {
                Supply.IsEnabled = true;
                UnCharge.IsEnabled = false;
                TimeChargerBlock.Visibility = Visibility.Hidden;

            }
            if (droneBL.IsActive == false)
            {
                Connect.IsEnabled = false;
                Charge.IsEnabled = false;
                Update.IsEnabled = false;
            }
            //get the time inn charge
            /* if(droneBL.DroneStatus == DroneStatus.Maintenance)
             {
                 //TimeChargerBlock.Text+= 
             }*/

        }

        /// <summary>
        /// constructor actions
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="drone"></param>
        /// <param name="parcel"></param>
       /* public Drone(BlApi.Bl bl, BO.Drone drone, Window parcel)
        {
            ParentWindow = parcel;
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
                Charge.IsEnabled = false;
                UnCharge.IsEnabled = true;
            }
            if (droneBL.DroneStatus != DroneStatus.Maintenance)
            {
                Charge.IsEnabled = true;
                UnCharge.IsEnabled = false;
                TimeChargerBlock.Visibility = Visibility.Hidden;

            }

        }*/

        /// <summary>
        /// constructor actions
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="drone"></param>
        /// <param name="station"></param>
       /* public Drone(BlApi.Bl bl, BO.Drone drone, Window station)
        {
            stationWindow = station;
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
                Charge.IsEnabled = false;
                UnCharge.IsEnabled = true;
            }
            if (droneBL.DroneStatus != DroneStatus.Maintenance)
            {
                Charge.IsEnabled = true;
                UnCharge.IsEnabled = false;
                TimeChargerBlock.Visibility = Visibility.Hidden;

            }

        }*/
        //###############################################################################
        //add Drone
        //###############################################################################
        private int getID()
        {
            try
            {
                return Int32.Parse(droneId.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("id");
            }

        }

        private int getStation()
        {
            try
            {
                return Int32.Parse(numStationtoChargeDrone.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("station");
            }
        }

        private string getModel()
        {
            if (droneMdel.Text == "")
                throw new InValidInput("model");
            return droneMdel.Text;
        }
        /// <summary>
        /// add drone to the list of drones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {

            try
            {
                BLObject.AddDrone(getID(), getModel(), droneWeight.SelectedIndex, getStation());
                MessageBox.Show("you added succefuly");
                ParentWindow.Show();
                Close();
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
            }

        }

        /// <summary>
        /// close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickResetAddDrone(object sender, RoutedEventArgs e)
        {
            droneId.Text = null;
            droneMdel.Text = null;
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
            droneBL = BLObject.UpdateDataDroneModel(droneBL.ID, modelDrone.Text);
        }

        /// <summary>
        /// send drone to charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            droneBL = BLObject.UpdateSendDroneToCharge(droneBL.ID);
            statusDrone.Text = droneBL.DroneStatus.ToString();
            /*visible.Visibility = Visibility.Hidden;
            hidden.Visibility = Visibility.Visible;*/
            Supply.IsEnabled = false;
            UnCharge.IsEnabled = true;

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
                Supply.IsEnabled = true;
                UnCharge.IsEnabled = false;

                /*Charge.IsEnabled = true;
                Connect.IsEnabled = true;
                Collect.IsEnabled = true;
                Supply.IsEnabled = true;
                UnCharge.IsEnabled = false;*/
                //TimeCharger.Visibility = Visibility.Hidden;
                TimeChargerBlock.Visibility = Visibility.Hidden;
                try
                {
                    droneBL = BLObject.UpdateUnchargeDrone(droneBL.ID, time);
                    statusDrone.Text = droneBL.DroneStatus.ToString();
                    batteryDrone.Text = $"{Math.Round(droneBL.BatteryStatus).ToString()}%";
                    TimeCharger.Text = "";
                }
                catch (Exception ex)
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
                BLObject.UpdateConnectParcelToDrone(droneBL.ID);
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
                BLObject.UpdateCollectParcelByDrone(droneBL.ID);
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
                BLObject.UpdateSupplyParcelByDrone(droneBL.ID);
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
            if (ParentWindow != null)
                ParentWindow.Show();
            else if (ParentWindow != null)
                ParentWindow.Show();
            else if (ParentWindow != null)
                ParentWindow.Show();
            Close();
        }

        /// <summary>
        /// open the parcel window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_openParcel(object sender, RoutedEventArgs e)
        {
            if (droneBL.parcelInDelivery != null)
                new Parcel(BLObject, BLObject.GetSpecificParcelBL(droneBL.parcelInDelivery.ID), this).Show();
        }

        private void Button_Click_DeleteDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("are you sure you want to remove sdrone?", "remove parcel", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    BLObject.RemoveDrone(droneBL.ID);
                    Button_ClickClose(sender, e);
                    /* ParentWindow.Show();
                     Close();*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("can't remove parcel because it's connected to a drone");
            }
        }
    }
}

