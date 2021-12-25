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

namespace PL_NewDesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.Bl BLObject;
        public MainWindow()
        {
            InitializeComponent();
            BLObject = BL.FactoryBL.factory();

        }

        private void Button_ClickNewCustomer(object sender, RoutedEventArgs e)
        {
            new NewCustomer(BLObject, new CustomerWindow(BLObject, new MainWindow())).Show();
        }

        private void Button_ClickCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                new CheckTheIDWIndow(BLObject, 'c').Show();
            }
            catch
            {

            }
        }

        private void Button_ClickWorker(object sender, RoutedEventArgs e)
        {
            try
            {
                new CheckTheIDWIndow(BLObject, 'w').Show();

            }
            catch
            {

            }
        }
    }
}
