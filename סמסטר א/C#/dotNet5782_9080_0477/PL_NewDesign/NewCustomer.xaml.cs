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

namespace PL_NewDesign
{
    /// <summary>
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class NewCustomer : Window
    {
        BlApi.Bl BLObject;
        public NewCustomer(BlApi.Bl Blobject)
        {
            InitializeComponent();
            BLObject = Blobject;
        }

        private void Button_ClickReset(object sender, RoutedEventArgs e)
        {
            newID.Text = null;
            newName.Text = null;
            newPhone.Text = null;
            newLong.Text = null;
            newLat.Text = null;
        }

        private void Button_ClickJoin(object sender, RoutedEventArgs e)
        {
            try
            {
                ulong id = (ulong)Convert.ToInt32(newID.Text);
                string name = Convert.ToString(newName.Text);
                string phone = Convert.ToString(newPhone.Text);
                double longit = Convert.ToDouble(newLong.Text);
                double lat = Convert.ToDouble(newLat.Text);
                BLObject.AddCustomer(id, name, phone, new BO.LocationBL(longit, lat));


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
