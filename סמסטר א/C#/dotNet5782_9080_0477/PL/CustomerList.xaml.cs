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
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        BlApi.IBL BLObject;
        //ObservableCollection<CustomerToList> MyList = new ObservableCollection<CustomerToList>();
        CollectionView view;
        internal static PLLists PLLists;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public CustomerList(BlApi.IBL bl)
        {
            InitializeComponent();
            PLLists = new PLLists();
            CustomerListView.ItemsSource = PLLists.Customers;
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            /*foreach (var item in BLObject.GetCustomerToList())
                MyList.Add(item);*/
            DataContext = PLLists.Customers;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);

        }

        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
            //MyList = new ObservableCollection<CustomerToList>();
            /*foreach (var item in BLObject.GetCustomerToList())
                MyList.Add(item);*/
            DataContext = PLLists.Customers;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            if (view != null)
            {
                view.GroupDescriptions.Clear();
            }
        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MouseDoubleClick_customerList(object sender, RoutedEventArgs e)
        {
            sender.ToString();
            Customer_ customerToList = (sender as ListView).SelectedValue as Customer_;
            BO.Customer customer = BLObject.GetSpecificCustomerBL(customer => customer.ID == customerToList.ID );
            //this.Visibility = Visibility.Hidden;
            /*new Customer(BLObject, customer, this, 'w').Show();
            Hide();*/
            var win = new Customer(BLObject, customer, 'w');
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void Button_ClickGroupByName(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("Name");
                view.GroupDescriptions.Add(property);
            }

        }

        private void Button_ClickGroupByPhone(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("Phone");
                view.GroupDescriptions.Add(property);
            }
        }

        private void Button_ClickOrderByPhone(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanSort == true)
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Phone", ListSortDirection.Ascending));
            }
        }

        private void Button_ClickOrderByName(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanSort == true)
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }
        }

        private void Button_ClickAddCustomer(object sender, RoutedEventArgs e)
        {
            /*new Customer(BLObject, this).Show();
            Hide();*/
            var win = new Customer(BLObject);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void Button_Click_ShowRemovedCustomers(object sender, RoutedEventArgs e)
        {
            //MyList = new ObservableCollection<CustomerToList>();
            /*foreach (var item in BLObject.GetDeletedCustomerToList())
                MyList.Add(item);*/
            DataContext = PLLists.Customers;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }
    }
}
