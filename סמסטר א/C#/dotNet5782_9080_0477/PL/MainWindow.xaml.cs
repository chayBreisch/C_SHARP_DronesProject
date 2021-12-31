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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.Bl BLObject;
        public MainWindow()
        {
            try {
                InitializeComponent();
                BLObject = BL.FactoryBL.factory();
            }
            catch (ExceptionsBL.CantReturnBLObject e)
            {
                throw new ExceptionsBL.CantReturnBLObject();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new FirstPage(BLObject, this).Show();
        }
    }
}
