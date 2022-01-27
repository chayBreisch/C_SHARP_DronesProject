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
using DO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    /// 

    public partial class DroneList : Window
    {
        BlApi.IBL BLObject;
        //ObservableCollection<DroneToList> MyList = new ObservableCollection<DroneToList>();
        internal static PLLists PLLists;
        CollectionView view;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public DroneList(BlApi.IBL bl, PLLists plLists)
        {
            InitializeComponent();
            PLLists = plLists;
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            /*foreach (var item in BLObject.GetDronesToList())
                MyList.Add(item);*/
            DataContext = PLLists.Drones;
            statusFilter.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            weightFilter.ItemsSource = BLObject.GetweightCategoriesEnumItem();
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }

        /// <summary>
        /// show the list of the drones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowListDrones(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            statusFilter.SelectedItem = null;
            weightFilter.SelectedItem = null;
            //DroneListView.ItemsSource = drones;
            if (view != null)
            {
                view.GroupDescriptions.Clear();
                /*MyList = new ObservableCollection<DroneToList>();
                foreach (var item in BLObject.GetDronesToList())
                    MyList.Add(item);*/
                DataContext = PLLists.Drones;
                view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            }
        }

        /// <summary>
        /// show the drones with the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSelectDrone(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<DroneToList> drones = new List<DroneToList>();
            ComboBox options = sender as ComboBox;
            if(weightFilter.SelectedItem == null)
                drones = BLObject.GetDronesToListByCondition(drone => drone.DroneStatus == (DroneStatus)(statusFilter.SelectedIndex));
            else if(statusFilter.SelectedItem == null)
                drones = BLObject.GetDronesToListByCondition(drone => drone.Weight == (WeightCatagories)(weightFilter.SelectedIndex + 1));
            else
                drones = BLObject.GetDronesToListByCondition(drone => drone.Weight == (WeightCatagories)(weightFilter.SelectedIndex + 1) && drone.DroneStatus == (DroneStatus)(statusFilter.SelectedIndex));
            //DroneListView.ItemsSource = drones;
            /*MyList = new ObservableCollection<DroneToList>();
            foreach (var item in drones)
                MyList.Add(item);*/
            DataContext = PLLists.Drones;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }

    /*    /// <summary>
        /// show drones with selected weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionDroneByWeight(object sender, SelectionChangedEventArgs e)
        {
            ComboBox options = sender as ComboBox;
            List<DroneToList> drones = new List<DroneToList>();
            if (statusFilter == null)
                drones = blDroneList.getDroneToListByCondition(drone => drone.Weight == (WeightCatagories)(options.SelectedIndex + 1)).ToList();
            else
                drones = blDroneList.getDroneToListByCondition(drone => drone.Weight == (WeightCatagories)(options.SelectedIndex + 1) && drone.DroneStatus == (DroneStatus)(options.SelectedIndex)).ToList();
            DroneListView.ItemsSource = drones;
        }*/

        /// <summary>
        /// open the addDrone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            /*new Drone(BLObject, this).Show();
            Hide();*/
            var win = new Drone(BLObject, PLLists);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;

        }

        /// <summary>
        /// open the drone window with the specific drone that clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            sender.ToString();
            Drone_ droneToList = (sender as ListView).SelectedValue as Drone_;
            BO.Drone drone = BLObject.GetSpecificDroneBL(droneToList.ID);
            //this.Visibility = Visibility.Hidden;
            /*new Drone(BLObject, drone, this).Show();
            Hide();*/
            var win = new Drone(BLObject, drone, PLLists);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// grouping list by status
        /// </summary>
        private void Button_Click_GroupingStatus(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("DroneStatus");
                view.GroupDescriptions.Add(property);
            }
        }

        /// <summary>
        /// grouping list by weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickGroupingWeight(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("Weight");
                view.GroupDescriptions.Add(property);
            }
        }

        /*public void Refresh()
        {
            IEnumerable<DroneToList> drones = new List<DroneToList>();
            if (weightFilter.SelectedItem != null && statusFilter.SelectedItem != null)
                drones = BLObject.GetDronesToListByCondition(drone => drone.Weight == (WeightCatagories)(weightFilter.SelectedIndex + 1) && drone.DroneStatus == (DroneStatus)(statusFilter.SelectedIndex));
            else if (statusFilter.SelectedItem != null)
                drones = BLObject.GetDronesToListByCondition(drone => drone.DroneStatus == (DroneStatus)(statusFilter.SelectedIndex));
            else if (weightFilter.SelectedItem != null)
                drones = BLObject.GetDronesToListByCondition(drone => drone.Weight == (WeightCatagories)(weightFilter.SelectedIndex + 1));
            else
                drones = BLObject.GetDronesToList();
            //DroneListView.ItemsSource = drones;
           *//* MyList = new ObservableCollection<DroneToList>();
            foreach (var item in drones)
                MyList.Add(item);*//*
            DataContext = PLLists.Drones;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }*/

        /// <summary>
        /// show deleted customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowDeletedCustomers(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            statusFilter.SelectedItem = null;
            weightFilter.SelectedItem = null;
            //DroneListView.ItemsSource = drones;
            if (view != null)
            {
                view.GroupDescriptions.Clear();
               /* MyList = new ObservableCollection<DroneToList>();
                foreach (var item in BLObject.GetDeletedDronesToList())
                    MyList.Add(item);*/
                DataContext = PLLists.Drones;
                view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            }
        }
    }
}