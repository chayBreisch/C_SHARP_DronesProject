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
        CollectionView view;
        internal static PLLists PLLists;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public ParcelList(BlApi.IBL bl, PLLists pllists)
        {
            InitializeComponent();
            PLLists = pllists;
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            ParcelListView.DataContext = PLLists.Parcels;
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.DataContext);
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
            Close();
        }

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
                ParcelListView.DataContext = PLLists.Parcels;
                view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.DataContext);
            }
        }

        /// <summary>
        /// add parcel to dataSource list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddParcel(object sender, RoutedEventArgs e)
        {
            var win = new Parcel(BLObject, PLLists);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// show specific parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            sender.ToString();
            Parcel_ parcelToList = (sender as ListView).SelectedValue as Parcel_;
            BO.Parcel parcel = BLObject.GetSpecificParcelBL(parcelToList.ID);
            var win = new Parcel(BLObject, parcel, PLLists);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
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
                ParcelListView.DataContext = PLLists.Parcels.Where(parcel => parcel.Priority == (DO.Priorities)(parcelPriority.SelectedIndex));
            else if (parcelPriority.SelectedItem == null)
                ParcelListView.DataContext = PLLists.Parcels.Where(parcel => parcel.Weight == (DO.WeightCatagories)(parcelWeight.SelectedIndex + 1));
            else
                ParcelListView.DataContext = PLLists.Parcels.Where(parcel => parcel.Weight == (DO.WeightCatagories)(parcelWeight.SelectedIndex + 1) && parcel.Priority == (DO.Priorities)(parcelPriority.SelectedIndex));
            view.GroupDescriptions.Clear();
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
                ParcelListView.DataContext = PLLists.Parcels.Where(P => P.IsActive == false);
                view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.DataContext);
            }
        }
    }
}