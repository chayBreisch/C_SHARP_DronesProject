using System;
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
using System.Collections.ObjectModel;

namespace PL1
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    /// 

    public partial class DroneList : Window
    {
        BlApi.Bl blDroneList;
        MainWindow mainWindow;
        ObservableCollection<DroneToList> MyList = new ObservableCollection<DroneToList>();
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public DroneList(BlApi.Bl bl, MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            WindowStyle = WindowStyle.None;
            blDroneList = bl;
            foreach (var item in blDroneList.getDroneToList())
                MyList.Add(item);
            DataContext = MyList;
            //DroneListView.ItemsSource = bl.GetDronesBL();
        }

        /// <summary>
        /// show the list of the drones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowListDrones(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            List<DroneToList> drones = blDroneList.getDroneToList();
            DroneListView.ItemsSource = drones;
        }

        /// <summary>
        /// show the drones with the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSelectDronesByStatus(object sender, SelectionChangedEventArgs e)
        {
            ComboBox options = sender as ComboBox;
            List<DroneToList> drones = blDroneList.getDronesByDroneStatus(options.SelectedIndex);
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
            List<DroneToList> drones = blDroneList.getDronesByDroneWeight(options.SelectedIndex + 1);
            DroneListView.ItemsSource = drones;
        }

        /// <summary>
        /// open the addDrone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            new Drone(blDroneList, this).Show();
            Hide();

        }

        /// <summary>
        /// open the drone window with the specific drone that clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            sender.ToString();
            DroneToList droneToList = (sender as ListView).SelectedValue as DroneToList;
           BO.Drone drone = blDroneList.convertDroneToListToDroneBL(droneToList);
            //this.Visibility = Visibility.Hidden;
            new Drone(blDroneList, drone, this).Show();
            Hide();
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

    }
}