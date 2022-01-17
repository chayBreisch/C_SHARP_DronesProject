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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
namespace PL1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BlApi.IBL bl;

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
            try
            {
                bl = BL.FactoryBL.factory();
                InitializeComponent();
            }
            catch (ExceptionsBL.CantReturnBLObject e)
            {
                throw new ExceptionsBL.CantReturnBLObject(e);
            }
        }

        /// <summary>
        /// open the droneList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DroneList(object sender, RoutedEventArgs e)
        {
            var win = new DroneList(bl);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
           /* new DroneList(bl).Show();
            Hide();*/
        }

        /// <summary>
        /// open the StationList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickStationList(object sender, RoutedEventArgs e)
        {
            var win = new StationList(bl);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
            /* new StationList(bl).Show();
             Hide()*/
            ;
        }

        /// <summary>
        /// open the ParcelList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickParcelList(object sender, RoutedEventArgs e)
        {
           // var win = new ParcelList(bl);
            Visibility = Visibility.Hidden;
            new ParcelList(bl).ShowDialog();
            Visibility = Visibility.Visible;
            /*new ParcelList(bl).Show();
            Hide();*/
        }

        /// <summary>
        /// open the CustomerList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCustomerList(object sender, RoutedEventArgs e)
        {
            var win = new CustomerList(bl);
            Visibility = Visibility.Hidden;
            win.ShowDialog();
            Visibility = Visibility.Visible;
            /*new CustomerList(bl).Show();
            Hide();*/
        }
    }
}