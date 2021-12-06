using System;
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
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    /// 

    public partial class DroneList : Window
    {
        IBL.Bl blDroneList;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public DroneList(IBL.Bl bl)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            blDroneList = bl;
            DroneListView.ItemsSource = bl.GetDronesBL();
        }

        /// <summary>
        /// show the list of the drones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowListDrones(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            List<DroneBL> drones = blDroneList.GetDronesBL();
            DroneListView.ItemsSource = drones;
        }

        /// <summary>
        /// close thus window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// open the mainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        /// <summary>
        /// show the drones with the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSelectDronesByStatus(object sender, SelectionChangedEventArgs e)
        {
            ComboBox options = sender as ComboBox;
            List<DroneBL> drones = blDroneList.getDronesByDroneStatus(options.SelectedIndex);
            DroneListView.ItemsSource = drones;
        }

        /// <summary>
        /// show drones with selected weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionDroneByWeight(object sender, SelectionChangedEventArgs e)
        {
            ComboBox options = sender as ComboBox;
            List<DroneBL> drones = blDroneList.getDronesByDroneWeight(options.SelectedIndex + 1);
            DroneListView.ItemsSource = drones;
        }

        /// <summary>
        /// open the addDrone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            Hide();
            new Drone(blDroneList).Show();
            
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            sender.ToString();
            ListView options = sender as ListView;
            DroneBL drone = blDroneList.getDroneByIndex(options.SelectedIndex);
            new Drone(blDroneList, drone).Show();
            Close();
            //MessageBox.Show(drone.ToString());
        }

        private void DroneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}