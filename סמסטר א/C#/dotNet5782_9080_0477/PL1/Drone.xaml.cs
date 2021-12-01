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
            actions.Visibility = Visibility.Hidden;
            addDrone.Visibility = Visibility.Visible;
        }

        public Drone(IBL.Bl bl, DroneBL drone)
        {
            blDrone = bl;
            droneBL = drone;
            InitializeComponent();
            addDrone.Visibility = Visibility.Hidden;
            actions.Visibility = Visibility.Visible;
            DroneContainIDModel.Text = $"id: {drone.ID }, model: ";
            ModelDrone.Text = drone.Model;
            DroneContain.Text = $", battery: {drone.BatteryStatus}, weight: {drone.Weight},  status: {drone.DroneStatus}, parcelInDelivery: { drone.parcelInDelivery}, location: { drone.Location.ToString() }";
            if (droneBL.DroneStatus != DroneStatus.Available)
                Charge.IsEnabled = false;
            if(droneBL.DroneStatus != DroneStatus.Maintenance)
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
                    new DroneList(blDrone).Show();
                    Close();
                }
                catch (Exception exce)
                {
                    MessageBox.Show(Convert.ToString(exce));
                }
            }
        }

        private void Button_ClickCancelAddDrone(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string model = ModelDrone.Text;
            blDrone.updateDataDroneModel(droneBL.ID, model);
            ModelDrone.Text = droneBL.Model;
            DroneContain.Text = $", battery: {droneBL.BatteryStatus}, weight: {droneBL.Weight},  status: {droneBL.DroneStatus}, parcelInDelivery: { droneBL.parcelInDelivery}, location: { droneBL.Location.ToString() }";

            /*new Drone(blDrone, droneBL).Show();
            Close();*/
        }

        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            blDrone.updateSendDroneToCharge(droneBL.ID);
            DroneContain.Text = $", battery: {droneBL.BatteryStatus}, weight: {droneBL.Weight},  status: {droneBL.DroneStatus}, parcelInDelivery: { droneBL.parcelInDelivery}, location: { droneBL.Location.ToString() }";
            Charge.IsEnabled = false;
            UnCharge.IsEnabled = true;
            TimeCharger.Visibility = Visibility.Visible;
            TimeChargerBlock.Visibility = Visibility.Visible;
            /*  new Drone(blDrone, droneBL).Show();
              Close();*/
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
                UnCharge.IsEnabled = false;
                TimeCharger.Visibility = Visibility.Hidden;
                TimeChargerBlock.Visibility = Visibility.Hidden;
                blDrone.updateUnchargeDrone(droneBL.ID, time);
                DroneContain.Text = $", battery: {droneBL.BatteryStatus}, weight: {droneBL.Weight},  status: {droneBL.DroneStatus}, parcelInDelivery: { droneBL.parcelInDelivery}, location: { droneBL.Location.ToString() }";
                TimeCharger.Text = "";
                /*new Drone(blDrone, droneBL).Show();
                Close();*/
            }

        }
    }
}

