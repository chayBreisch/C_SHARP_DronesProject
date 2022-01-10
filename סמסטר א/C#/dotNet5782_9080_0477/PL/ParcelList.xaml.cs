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

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : Window
    {
        BlApi.IBL BLObject;
        Window ParentWindow;
        public ObservableCollection<ParcelToList> MyList = new ObservableCollection<ParcelToList>();
        public CollectionView view;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public ParcelList(BlApi.IBL bl, Window main)
        {
            InitializeComponent();
            ParentWindow = main;
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            foreach (var item in BLObject.GetParcelsToList())
                MyList.Add(item);
            DataContext = MyList;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            //view.SortDescriptions.Add(new SortDescription("Weight", ListSortDirection.Ascending));

            // view.SortDescriptions.Add(new SortDescription(null, ListSortDirection.Ascending));
            parcelWeight.ItemsSource = BLObject.GetweightCategoriesEnumItem();
            parcelPriority.ItemsSource = BLObject.GetPrioritiesEnumItem();
        }

        /// <summary>
        /// close this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            ParentWindow.Show();
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
            parcelPriority.SelectedItem = null;
            parcelWeight.SelectedItem = null;
            if (view != null)
            {
                view.GroupDescriptions.Clear();
                MyList = new ObservableCollection<ParcelToList>();
                foreach (var item in BLObject.GetParcelsToList())
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
            new Parcel(BLObject, this).Show();
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
            BO.Parcel parcel = BLObject.ConvertParcelToListToParcelBL(parcelToList);
            //this.Visibility = Visibility.Hidden;
            new Parcel(BLObject, parcel, this).Show();
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
                parcels = BLObject.GetParcelsToListByCondition(parcel => parcel.Priority == (DO.Priorities)(parcelPriority.SelectedIndex));
            else if (parcelPriority.SelectedItem == null)
                parcels = BLObject.GetParcelsToListByCondition(parcel => parcel.Weight == (DO.WeightCatagories)(parcelWeight.SelectedIndex + 1));
            else
                parcels = BLObject.GetParcelsToListByCondition(parcel => parcel.Weight == (DO.WeightCatagories)(parcelWeight.SelectedIndex + 1) && parcel.Priority == (DO.Priorities)(parcelPriority.SelectedIndex));
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

        /// <summary>
        /// refresh the list
        /// </summary>
        public void Refresh()
        {
            IEnumerable<ParcelToList> parcels = new List<ParcelToList>();
            if (parcelWeight.SelectedItem != null && parcelPriority.SelectedItem != null)
                parcels = BLObject.GetParcelsToListByCondition(parcel => parcel.Weight == (DO.WeightCatagories)(parcelWeight.SelectedIndex + 1) && parcel.Priority == (DO.Priorities)(parcelPriority.SelectedIndex));
            else if (parcelPriority.SelectedItem != null)
                parcels = BLObject.GetParcelsToListByCondition(parcel => parcel.Priority == (DO.Priorities)(parcelPriority.SelectedIndex));
            else if (parcelWeight.SelectedItem != null)
                parcels = BLObject.GetParcelsToListByCondition(parcel => parcel.Weight == (DO.WeightCatagories)(parcelWeight.SelectedIndex + 1));
            else
                parcels = BLObject.GetParcelsToList();
            view.GroupDescriptions.Clear();

            MyList = new ObservableCollection<ParcelToList>();
            foreach (var item in parcels)
                MyList.Add(item);
            DataContext = MyList;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }

        /// <summary>
        /// show deleted parcels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowDeletedParcels(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            parcelPriority.SelectedItem = null;
            parcelWeight.SelectedItem = null;
            if (view != null)
            {
                view.GroupDescriptions.Clear();
                MyList = new ObservableCollection<ParcelToList>();
                foreach (var item in BLObject.GetDeletedParcelsToList())
                    MyList.Add(item);
                DataContext = MyList;
                view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            }
        }
    }
}