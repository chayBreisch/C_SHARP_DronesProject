using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Drone_ DronePL;
        PLLists PLLists;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneList"></param>
        public Drone(BlApi.IBL bl, PLLists plLists)
        {
            BLObject = bl;
            InitializeComponent();
            PLLists = plLists;
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
        public Drone(BlApi.IBL bl, BO.Drone drone, PLLists plLists)
        {
            BLObject = bl;
            droneBL = drone;
            InitializeComponent();
            PLLists = plLists;
            DronePL = new Drone_(droneBL);
            maimGrid.DataContext = DronePL;
            WindowStyle = WindowStyle.None;
            addDrone.Visibility = Visibility.Hidden;
            actions.Visibility = Visibility.Visible;
            createButtons(droneBL);
            if (droneBL.parcelInDelivery != null)
                parcelInDeliveryDrone.Text = droneBL.parcelInDelivery.ToString();
            else
                parcelInDeliveryDrone.Text = "no parcel";
        }

        
        //###############################################################################
        //add Drone
        //###############################################################################
        /// <summary>
        /// get id of drone
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// get station to charge drone
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// get model
        /// </summary>
        /// <returns></returns>
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
                PLLists.AddDrone(BLObject.GetSpecificDroneBL(getID()));
                MessageBox.Show("you added succefuly");
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
        /// </summary
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneBL = BLObject.UpdateDataDroneModel(droneBL.ID, modelDrone.Text);
                MessageBox.Show("you updated sucssesfully");
                DronePL.updateDrone(droneBL);

                PLLists.UpdateDrone(droneBL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("can not update drone");
            }
        }

        /// <summary>
        /// send drone to charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneBL = BLObject.UpdateSendDroneToCharge(droneBL.ID);
                TimeChargerBlock.Visibility = Visibility.Visible;
                MessageBox.Show("the drone is sended to charge succesfully");
                DronePL.updateDrone(droneBL);
                PLLists.UpdateDrone(droneBL);
                createButtons(droneBL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("can not charge drone");
            }
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
                TimeChargerBlock.Visibility = Visibility.Hidden;
                try
                {
                    droneBL = BLObject.UpdateUnchargeDrone(droneBL.ID, time);
                    TimeCharger.Text = "";
                    MessageBox.Show("the drone is uncharged sucssesfully");
                    DronePL.updateDrone(droneBL);
                    PLLists.UpdateDrone(droneBL);
                    createButtons(droneBL);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("can not uncharge drone");
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
                DronePL.updateDrone(droneBL);
                PLLists.UpdateDrone(droneBL);
                maimGrid.DataContext = DronePL;

                createButtons(droneBL);
                //PLLists.UpdateDrone(droneBL);
            }
            catch (Exception ex)
            {
                check = false;
                MessageBox.Show("can not connect drone");
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
                DronePL.updateDrone(droneBL);
                PLLists.UpdateDrone(droneBL);
                createButtons(droneBL);
                //PLLists.UpdateDrone(droneBL);
            }
            catch (Exception ex)
            {
                check = false;
                MessageBox.Show("can not collect drone");
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
                DronePL.updateDrone(droneBL);
                PLLists.UpdateDrone(droneBL);
                createButtons(droneBL);
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
            if (worker != null)
                worker.CancelAsync();
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
                new Parcel(BLObject, BLObject.GetSpecificParcelBL(droneBL.parcelInDelivery.ID), PLLists).Show();
        }

        /// <summary>
        /// call the right function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = $"{sender.ToString().Substring(sender.ToString().LastIndexOf(':') + 2)}";
                switch (s)
                {
                    case "update":
                        Update_Click(sender, e);
                        break;
                    case "send to charge":
                        Charge_Click(sender, e);
                        break;
                    case "uncharge drone":
                        UnCharge_Click(sender, e);
                        break;
                    case "send to parcel":
                        Connect_Click(sender, e);
                        break;
                    case "pick up parcel":
                        Collect_Click(sender, e);
                        break;
                    case "parcel delivery":
                        Supply_Click(sender, e);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// create the buttons by the status
        /// </summary>
        /// <param name="drone"></param>
        private void createButtons(BO.Drone drone)
        {
            if (droneBL.BatteryStatus < 10) battery.Foreground = new SolidColorBrush(Colors.Red);
            else if (droneBL.BatteryStatus < 50) battery.Foreground = new SolidColorBrush(Colors.Yellow);
            else if (droneBL.BatteryStatus < 80) battery.Foreground = new SolidColorBrush(Colors.BlueViolet);
            else battery.Foreground = new SolidColorBrush(Colors.Green);
            if (drone.DroneStatus == DroneStatus.Available)
            {
                button1.Content = "send to charge";
                button2.Content = "send to parcel";
                placewTheButtons(2);
            }
            else if (drone.DroneStatus == DroneStatus.Maintenance)
            {
                button1.Content = "uncharge drone";
                placewTheButtons(1);
            }
            else if (BLObject.GetSpecificParcelBL(drone.parcelInDelivery.ID).PickedUp == null)
            {
                button1.Content = "pick up parcel";
                placewTheButtons(1);
            }
            else
            {
                button1.Content = "parcel delivery";
                placewTheButtons(1);
            }

        }

        /// <summary>
        /// place the buttons
        /// </summary>
        /// <param name="count"></param>
        private void placewTheButtons(int count)
        {
            if (count == 1)
            {
                button2.Visibility = Visibility.Hidden;
            }
            if (count == 2)
            {
                button2.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// delete drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DeleteDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("are you sure you want to remove sdrone?", "remove parcel", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    BLObject.RemoveDrone(droneBL.ID);
                    Button_ClickClose(sender, e);
                    MessageBox.Show("drone removed sucssesfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("can't remove parcel because it's connected to a drone");
            }
        }
        BackgroundWorker worker;

        /// <summary>
        /// start simulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimulationBtn_Click(object sender, RoutedEventArgs e)
        {
            worker = new BackgroundWorker();
            if (SimulationBtn.Content == "start manual")
            {
                SimulationBtn.Content = "start simulation";
                worker.CancelAsync();
                createButtons(droneBL);
                return;
            }


            SimulationBtn.Content = "start manual";
            createButtons(droneBL);

            worker.DoWork += (object? sender, DoWorkEventArgs e) =>
            {
                BLObject.StartSimulation(
                   droneBL,
                   worker,
                   (drone, i) => { BLObject.UpdateDataDrone(droneBL); worker.ReportProgress(i); },
                   () => worker.CancellationPending);

            };
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (object? sender, ProgressChangedEventArgs e) =>
            {
                DronePL.updateDrone(droneBL);
                PLLists.UpdateDrone(droneBL);
                createButtons(droneBL);
            };

            worker.RunWorkerCompleted += (object? sender, RunWorkerCompletedEventArgs e) =>
            {
                SimulationBtn.Content = "start simulation";
                worker.CancelAsync();
                createButtons(droneBL);
            };
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }
    }
}

