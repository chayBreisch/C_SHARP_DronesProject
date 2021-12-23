using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL1
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : Window
    {
        BlApi.Bl blParcelList;
        MainWindow mainWindow;
        ObservableCollection<ParcelToList> MyList = new ObservableCollection<ParcelToList>();
        public CollectionView view;
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
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            //view.SortDescriptions.Add(new SortDescription("Weight", ListSortDirection.Ascending));

            // view.SortDescriptions.Add(new SortDescription(null, ListSortDirection.Ascending));
            parcelWeight.ItemsSource = blParcelList.getweightCategoriesEnumItem();
            parcelPriority.ItemsSource = blParcelList.getPrioritiesEnumItem();
        }

        /// <summary>
        /// close this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        /* private void parcelPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
         }*/

        /// <summary>
        /// show the all list of parcels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            IEnumerable<ParcelToList> stations = blParcelList.getParcelToList();
            parcelPriority.SelectedItem = null;
            parcelWeight.SelectedItem = null;
            if (view != null)
            {
                view.GroupDescriptions.Clear();
                MyList = new ObservableCollection<ParcelToList>();
                foreach (var item in blParcelList.getParcelToList())
                    MyList.Add(item);
                DataContext = MyList;
                view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            }
        }

        /// <summary>
        /// add parcel to dataSource list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddParcel(object sender, RoutedEventArgs e)
        {
            new Parcel(blParcelList, this).Show();
            Hide();
        }

        /// <summary>
        /// show specific parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            sender.ToString();
            ParcelToList parcelToList = (sender as ListView).SelectedValue as ParcelToList;
            BO.Parcel parcel = blParcelList.convertParcelToListToParcelBL(parcelToList);
            //this.Visibility = Visibility.Hidden;
            new Parcel(blParcelList, parcel, this).Show();
            Hide();
        }

        /// <summary>
        /// return a list of parcel after filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSelectparcel(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<ParcelToList> parcels = new List<ParcelToList>();
            ComboBox options = sender as ComboBox;
            if (parcelWeight.SelectedItem == null)
                parcels = blParcelList.getParcelToListByCondition(parcel => parcel.Priority == (DO.Priorities)(parcelPriority.SelectedIndex));
            else if (parcelPriority.SelectedItem == null)
                parcels = blParcelList.getParcelToListByCondition(parcel => parcel.Weight == (DO.WeightCatagories)(parcelWeight.SelectedIndex + 1));
            else
                parcels = blParcelList.getParcelToListByCondition(parcel => parcel.Weight == (DO.WeightCatagories)(parcelWeight.SelectedIndex + 1) && parcel.Priority == (DO.Priorities)(parcelPriority.SelectedIndex));
            view.GroupDescriptions.Clear();

            MyList = new ObservableCollection<ParcelToList>();
            foreach (var item in parcels)
                MyList.Add(item);
            DataContext = MyList;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            //ParcelListView.ItemsSource = drones;
        }

        /// <summary>
        /// grouping the parcel list by priority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickGroupByReciver(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("NameCustomerReciver");
                view.GroupDescriptions.Add(property);
            }
            
        }

        /// <summary>
        /// grouping the parcel list by weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickGroupBySender(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("NameCustomerSender");
                view.GroupDescriptions.Add(property);
            }
        }
    }
}