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
namespace PL1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IBL.Bl bl;

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
            bl = BL.FactoryBL.factory("BL");
            InitializeComponent();
        }

        /// <summary>
        /// open the droneList window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DroneList(object sender, RoutedEventArgs e)
        {
            new DroneList(bl).Show();
            Close();
        }
    }
}