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
    /// Interaction logic for AddDrone.xaml
    /// </summary>
    public partial class AddDrone : Window
    {
        IBL.Bl blAddDrone;
        public AddDrone(IBL.Bl bl)
        {
            InitializeComponent();
            blAddDrone = bl;
        }

        private void Button_ClickAddDrone(object sender, RoutedEventArgs e)
        {
            int id;
            bool success = Int32.TryParse(droneId.Text, out id);
            int weight;
            bool success1 = Int32.TryParse(droneWeight.Text, out weight);
            int number;
            bool success2 = Int32.TryParse(numStationtoChargeDrone.Text, out number);
            string Model = droneMdel.Text;
            if (!success)
            {
                MessageBox.Show("not valid id input");
            }
            else if (!success1)
            {
                MessageBox.Show("not valid weight input");
            }
            else if (!success2)
            {
                MessageBox.Show("not valid station input");
            }
            else
            {
                try
                {
                    blAddDrone.addDrone(id, Model, weight, number);
                    MessageBox.Show("you added succefuly");
                    //לעשות שיפתח את הקודם
                    new DroneList(blAddDrone).Show();
                    Close();
                }
                catch (Exception exce)
                {
                    MessageBox.Show(Convert.ToString(exce));
                }
            }
        }

        private void Button_ClickCancelAddDrone(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
