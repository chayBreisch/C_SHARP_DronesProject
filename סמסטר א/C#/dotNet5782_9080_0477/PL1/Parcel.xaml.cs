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
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class Parcel : Window
    {
        ParcelList parcelList;
        BlApi.Bl blparcel;
        BO.Parcel parcelBL;
        /// <summary>
        /// constructor add parcel
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneList"></param>
        public Parcel(BlApi.Bl bl, ParcelList ParcelList)
        {
            parcelList = ParcelList;
            blparcel = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            addStation.Visibility = Visibility.Visible;
            weightParcel.ItemsSource = Enum.GetValues(typeof(DO.WeightCatagories));
            weightParcel.SelectedItem = DO.WeightCatagories.Medium;
            priorityParcel.ItemsSource = Enum.GetValues(typeof(DO.Priorities));
            priorityParcel.SelectedItem = DO.Priorities.Regular;
        }

        /// <summary>
        /// constructor actions parcel
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="parcel"></param>
        /// <param name="ParcelList"></param>
        public Parcel(BlApi.Bl bl, BO.Parcel parcel, ParcelList ParcelList)
        {
            InitializeComponent();
            actions.Visibility = Visibility.Visible;
            addStation.Visibility = Visibility.Hidden;
            WindowStyle = WindowStyle.None;
            parcelList = ParcelList;
            blparcel = bl;
            parcelBL = parcel;
            idparcel.Text = parcelBL.ID.ToString();
            senderidparcel.Text = parcelBL.Sender.ID.ToString();
            recieveridparcel.Text = parcelBL.Reciever.ID.ToString();
            weightparcel.Text = parcelBL.Weight.ToString();
            priorityparcel.Text = parcelBL.Priorities.ToString();
            droneparcel.Text = parcelBL.Drone.ToString();
            //statusparcel.Text = parcelBL.
        }

        private void Button_ClickAddParcel(object sender, RoutedEventArgs e)
        {
            ulong senderId;
            bool success = ulong.TryParse(senderIdParcel.Text, out senderId);
            ulong recieverId;
            bool success1 = ulong.TryParse(recieverIdParcel.Text, out recieverId);
            if (!success)
            {
                MessageBox.Show("not valid sender id input");
            }
            else if (!success1)
            {
                MessageBox.Show("not valid reciever id input");
            }
            else
            {
                try
                {
                    blparcel.AddParcel(senderId, recieverId, weightParcel.SelectedIndex, priorityParcel.SelectedIndex);
                    MessageBox.Show("you added succefuly");
                    parcelList.Show();
                    Close();
                }
                catch (Exception exce)
                {
                    MessageBox.Show(exce.Message);
                }
            }
        }

        private void Button_ClickCloseParcel(object sender, RoutedEventArgs e)
        {
            parcelList.Show();
            Close();
        }

        private void Button_ClickResetParcel(object sender, RoutedEventArgs e)
        {
            senderIdParcel.Text = null;
            recieverIdParcel.Text = null;
            weightParcel.SelectedItem = DO.WeightCatagories.Medium;
            priorityParcel.SelectedItem = DO.Priorities.Regular;
        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            parcelList.Show();
            Close();
        }
    }
}
