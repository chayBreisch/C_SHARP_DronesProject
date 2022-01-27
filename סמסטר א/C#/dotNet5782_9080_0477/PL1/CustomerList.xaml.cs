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
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        BlApi.IBL blcustomer;
        ObservableCollection<CustomerToList> MyList = new ObservableCollection<CustomerToList>();
        CollectionView view;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public CustomerList(BlApi.IBL bl)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            blcustomer = bl;
            foreach (var item in blcustomer.GetCustomersToList())
                MyList.Add(item);
            DataContext = MyList;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);

        }

        /// <summary>
        /// show list of customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
            MyList = new ObservableCollection<CustomerToList>();
            foreach (var item in blcustomer.GetCustomersToList())
                MyList.Add(item);
            DataContext = MyList;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
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
            //new MainWindow().Show();
            Close();
        }

        /// <summary>
        /// open clicked customer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDoubleClick_customerList(object sender, RoutedEventArgs e)
        {
            sender.ToString();
            CustomerToList customerToList = (sender as ListView).SelectedValue as CustomerToList;
            BO.Customer customer = blcustomer.ConvertCustomerToListToCustomerlBL(customerToList);
            //this.Visibility = Visibility.Hidden;
            /*new Customer(blcustomer, customer, this).Show();
            Hide();*/
            var win = new Customer(blcustomer, customer);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// group by name
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
        /// group by phone
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
        /// order by phone
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
        /// order by name
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
        /// add customer to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddCustomer(object sender, RoutedEventArgs e)
        {
            /*new Customer(blcustomer, this).Show();
            Hide();*/
            var win = new Customer(blcustomer);
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
            MyList = new ObservableCollection<CustomerToList>();
            foreach (var item in blcustomer.GetDeletedCustomersToList())
                MyList.Add(item);
            DataContext = MyList;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }
    }
}
