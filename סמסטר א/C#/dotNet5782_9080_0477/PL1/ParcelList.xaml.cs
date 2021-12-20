using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ParcelList : Window
    {
        public ParcelList()
        {
            InitializeComponent();
        }
        BlApi.Bl blParcelList;
        MainWindow mainWindow;
        ObservableCollection<ParcelToList> MyList = new ObservableCollection<ParcelToList>();


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public ParcelList(BlApi.Bl bl, MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            WindowStyle = WindowStyle.None;
            blParcelList = bl;
            foreach (var item in blParcelList.getParcelToList())
                MyList.Add(item);
            DataContext = MyList;
            //DroneListView.ItemsSource = bl.GetDronesBL();
        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        private void parcelPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox options = sender as ComboBox;
            List<ParcelToList> stations = blParcelList.getParcelsByPriority(options.SelectedIndex);
            ParcelListView.ItemsSource = stations;
        }

        private void parcelWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox options = sender as ComboBox;
            List<ParcelToList> stations = blParcelList.getParcelsByparcelWeight(options.SelectedIndex + 1);
            ParcelListView.ItemsSource = stations;
        }

        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            List<ParcelToList> stations = blParcelList.getParcelToList();
            ParcelListView.ItemsSource = stations;
        }
    }
}