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

        BlApi.Bl bl;

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
                throw new ExceptionsBL.CantReturnBLObject();
            }
        }

        /// <summary>
        /// open the droneList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DroneList(object sender, RoutedEventArgs e)
        {
            new DroneList(bl, this).Show();
            Hide();
        }

        private void Button_ClickStationList(object sender, RoutedEventArgs e)
        {
            new StationList(bl, this).Show();
            Hide();
        }
    }
}