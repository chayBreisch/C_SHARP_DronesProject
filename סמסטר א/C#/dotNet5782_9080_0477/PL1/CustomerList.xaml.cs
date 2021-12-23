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
        BlApi.Bl blcustomer;
        MainWindow mainWindow;
        ObservableCollection<CustomerToList> MyList = new ObservableCollection<CustomerToList>();
        CollectionView view;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public CustomerList(BlApi.Bl bl, MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            WindowStyle = WindowStyle.None;
            blcustomer = bl;
            foreach (var item in blcustomer.getCustomerToList())
                MyList.Add(item);
            DataContext = MyList;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);

        }

        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
           /* ListBox listBox1 = new ListBox();
            List<CustomerToList> customers = blcustomer.getCustomerToList();*/
            //CustomerListView.ItemsSource = customers;
            if (view != null)
            {
                view.GroupDescriptions.Clear();
            }
        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }

        private void MouseDoubleClick_customerList(object sender, RoutedEventArgs e)
        {
            sender.ToString();
            CustomerToList customerToList = (sender as ListView).SelectedValue as CustomerToList;
            BO.Customer customer = blcustomer.convertCustomerToListToCustomerlBL(customerToList);
            //this.Visibility = Visibility.Hidden;
            new Customer(blcustomer, customer, this).Show();
            Hide();
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
            new Customer(blcustomer, this).Show();
            Hide();

        }
    }
}
