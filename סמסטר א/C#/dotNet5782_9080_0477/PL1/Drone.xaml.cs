using IBL.BO;
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
        IBL.Bl blDrone;
        DroneBL droneBL;
        public Drone(IBL.Bl bl)
        {
            blDrone = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            addDrone.Visibility = Visibility.Visible;
        }

        DroneList DroneList;
        public Drone(IBL.Bl bl, DroneBL drone, DroneList droneList)
        {
            DroneList = droneList;
            blDrone = bl;
            droneBL = drone;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            addDrone.Visibility = Visibility.Hidden;
            actions.Visibility = Visibility.Visible;
            idDrone.Text = drone.ID.ToString();
            modelDrone.Text = drone.Model;
            batteryDrone.Text = $"{Math.Round(drone.BatteryStatus).ToString()}%";
            weightDrone.Text = drone.Weight.ToString();
            statusDrone.Text = drone.DroneStatus.ToString();
            if (drone.parcelInDelivery != null)
                parcelInDeliveryDrone.Text = drone.parcelInDelivery.ToString();
            else
                parcelInDeliveryDrone.Text = "no parcel";
            locationDrone.Text = $"{drone.Location.Latitude}, {drone.Location.Longitude}";
            if (droneBL.DroneStatus != DroneStatus.Available)
            {
                Charge.IsEnabled = false;
                Connect.IsEnabled = false;
                Collect.IsEnabled = false;
                Supply.IsEnabled = false;
            }
            if (droneBL.DroneStatus != DroneStatus.Maintenance)
            {
                UnCharge.IsEnabled = false;
                TimeCharger.Visibility = Visibility.Hidden;
                TimeChargerBlock.Visibility = Visibility.Hidden;
                
            }

        }




        //###############################################################################
        //add Drone
        //###############################################################################
        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            int id;
            bool success = Int32.TryParse(droneId.Text, out id);
            int weight;
            bool success1 = Int32.TryParse(droneWeight.Text, out weight);
            int number;
            bool success2 = Int32.TryParse(numStationtoChargeDrone.Text, out number);
            string Model = droneMdel.Text;
            if (!success)
            {
                MessageBox.Show("not valid id input");
            }
            else if (!success1)
            {
                MessageBox.Show("not valid weight input");
            }
            else if (!success2)
            {
                MessageBox.Show("not valid station input");
            }
            else
            {
                try
                {
                    blDrone.addDrone(id, Model, weight, number);
                    MessageBox.Show("you added succefuly");
                    //לעשות שיפתח את הקודם
                    DroneList.Visibility = Visibility.Visible;
                    //new DroneList(blDrone).Show();
                    this.Close();
                }
                catch (Exception exce)
                {
                    MessageBox.Show(Convert.ToString(exce));
                }
            }
        }

        private void Button_ClickCancelAddDrone(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //###########################################################
        //actions
        //###########################################################
      private void Update_Click(object sender, RoutedEventArgs e)
        {
           string model = modelDrone.Text;
            blDrone.updateDataDroneModel(droneBL.ID, model);
            modelDrone.Text = droneBL.Model;
         }

       private void Charge_Click(object sender, RoutedEventArgs e)
        {
            blDrone.updateSendDroneToCharge(droneBL.ID);
            statusDrone.Text = droneBL.DroneStatus.ToString();
            Charge.IsEnabled = false;
            Connect.IsEnabled = false;
            Collect.IsEnabled = false;
            Supply.IsEnabled = false;
            UnCharge.IsEnabled = true;
            TimeCharger.Visibility = Visibility.Visible;
            TimeChargerBlock.Visibility = Visibility.Visible;
       }

        private void UnCharge_Click(object sender, RoutedEventArgs e)
        {
            double time;
            bool success = double.TryParse(TimeCharger.Text, out time);
            if (!success)
                MessageBox.Show("invalid time");
            else
            {
                Charge.IsEnabled = true;
                Connect.IsEnabled = true;
                Collect.IsEnabled = true;
                Supply.IsEnabled = true;
                UnCharge.IsEnabled = false;
                TimeCharger.Visibility = Visibility.Hidden;
                TimeChargerBlock.Visibility = Visibility.Hidden;
                blDrone.updateUnchargeDrone(droneBL.ID, time);
                statusDrone.Text = droneBL.DroneStatus.ToString();
                batteryDrone.Text = $"{Math.Round(droneBL.BatteryStatus).ToString()}%";
                TimeCharger.Text = "";
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            try
            {
                blDrone.updateConnectParcelToDrone(droneBL.ID);
            }
            catch(Exception ex)
            {
                check = false;
                MessageBox.Show(ex.ToString());
            }
            if (check)
                MessageBox.Show("connected succesfully");       
        }

        private void Collect_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            try
            {
                blDrone.updateCollectParcelByDrone(droneBL.ID);
            }
            catch(Exception ex)
            {
                check = false;
                MessageBox.Show(ex.ToString());
            }
            if (check)
                MessageBox.Show("collected succesfully");
        }

        private void Supply_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            try
            {
                blDrone.updateSupplyParcelByDrone(droneBL.ID);
            }
            catch(Exception ex)
            {
                check = false;
                MessageBox.Show(ex.ToString());
            }
            if (check)
                MessageBox.Show("supplied succesfully");
        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            DroneList.Visibility = Visibility.Visible;
            //new DroneList(blDrone).Show();
            this.Close();
        }
    }
}

