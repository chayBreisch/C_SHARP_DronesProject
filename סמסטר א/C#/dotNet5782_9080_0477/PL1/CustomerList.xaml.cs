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
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        BlApi.Bl blcustomer;
        MainWindow mainWindow;
        ObservableCollection<CustomerToList> MyList = new ObservableCollection<CustomerToList>();


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
        }

        private void Button_ClickShowList(object sender, RoutedEventArgs e)
        {
            ListBox listBox1 = new ListBox();
            List<CustomerToList> customers = blcustomer.getCustomerToList();
            CustomerListView.ItemsSource = customers;
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
    }
}
