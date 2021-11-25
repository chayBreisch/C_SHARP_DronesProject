using IBL.BO;
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
using System.Windows.Shapes;

namespace PL1
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : Window
    {
        BL.BL bl;
        public ParcelList()
        {
            bl = new BL.BL();
            InitializeComponent();
        }

        private void Button_Click_ShowListParcels(object sender, RoutedEventArgs e)
        {
            List<ParcelBL> parcelBLs = bl.GetParcelsBL();
            foreach (var item in parcelBLs)
            {
                //DroneList.DataContext = item;
                MessageBox.Show(item.ToString());
                //DataContext = item;
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
