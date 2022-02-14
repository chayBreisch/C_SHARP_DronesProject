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
        CollectionView view;
        internal static PLLists PLLists;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public CustomerList(BlApi.IBL bl, PLLists plLists)
        {
            InitializeComponent();
            PLLists = plLists;
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            MainGrid.DataContext = PLLists.Customers;
            view = (CollectionView)CollectionViewSource.GetDefaultView(MainGrid.DataContext);

        }
        
        /// <summary>
        /// show list customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
            CustomerListView.DataContext = PLLists.Customers;
            view = (CollectionView)CollectionViewSource.GetDefaultView(CustomerListView.DataContext);
            if (view != null)
            {
                view.GroupDescriptions.Clear();
            }
        }

        /// <summary>
        /// close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// open clicked customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDoubleClick_customerList(object sender, RoutedEventArgs e)
        {
            sender.ToString();
            Customer_ customerToList = (sender as ListView).SelectedValue as Customer_;
            BO.Customer customer = BLObject.GetSpecificCustomerBL(customer => customer.ID == customerToList.ID );
            var win = new Customer(BLObject, customer, 'w', PLLists);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// group customers by name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickGroupByName(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("Name");
                view.GroupDescriptions.Add(property);
            }

        }

        /// <summary>
        /// group customers by phone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickGroupByPhone(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription property = new PropertyGroupDescription("Phone");
                view.GroupDescriptions.Add(property);
            }
        }

        /// <summary>
        /// order customers by phone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickOrderByPhone(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanSort == true)
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Phone", ListSortDirection.Ascending));
            }
        }

        /// <summary>
        /// order customers by name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickOrderByName(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanSort == true)
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }
        }

        /// <summary>
        /// add customers to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddCustomer(object sender, RoutedEventArgs e)
        {
            /*new Customer(BLObject, this).Show();
            Hide();*/
            var win = new Customer(BLObject, PLLists);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// show removed customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowRemovedCustomers(object sender, RoutedEventArgs e)
        {
            CustomerListView.DataContext = PLLists.Customers.Where(C => C.IsActive == false);
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }
    }
}
