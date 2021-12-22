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
        Drone Drone;
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
            weightParcel.ItemsSource = blparcel.getweightCategoriesEnumItem();
            weightParcel.SelectedItem = blparcel.getweightCategoriesEnumItem().GetValue(1);
            priorityParcel.ItemsSource = blparcel.getPrioritiesEnumItem();
            priorityParcel.SelectedItem = blparcel.getweightCategoriesEnumItem().GetValue(0);
        }

        /// <summary>
        /// constructor opened from drone
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="drone"></param>
        public Parcel(BlApi.Bl bl, BO.Parcel parcel, Drone drone)
        {
            InitializeComponent();
            actions.Visibility = Visibility.Visible;
            addStation.Visibility = Visibility.Hidden;
            WindowStyle = WindowStyle.None;
            Drone = drone;
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
            if(parcelBL.Drone != null)
                droneparcel.Text = parcelBL.Drone.ToString();
            //statusparcel.Text = parcelBL.
        }

        /// <summary>
        /// returns the sender id
        /// </summary>
        /// <returns></returns>
        private ulong getSenderId()
        {
            try
            {
                return ulong.Parse(senderIdParcel.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("sender id");
            }
        }

        /// <summary>
        /// returns the reciever id
        /// </summary>
        /// <returns></returns>
        private ulong getRecieverId()
        {
            try
            {
                return ulong.Parse(recieveridparcel.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("reciever id");
            }
        }

        /// <summary>
        /// add parcel to dataSource list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                blparcel.AddParcel(getSenderId(), getRecieverId(), weightParcel.SelectedIndex + 1, priorityParcel.SelectedIndex);
                MessageBox.Show("you added succefuly");
                parcelList.Show();
                Close();
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
            }
        }

        /// <summary>
        /// close this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCloseParcel(object sender, RoutedEventArgs e)
        {
            parcelList.Show();
            Close();
        }

        /// <summary>
        /// reset the data of the parcel to add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickResetParcel(object sender, RoutedEventArgs e)
        {
            senderIdParcel.Text = null;
            recieverIdParcel.Text = null;
            weightParcel.SelectedItem = DO.WeightCatagories.Medium;
            priorityParcel.SelectedItem = DO.Priorities.Regular;
        }

        /// <summary>
        /// remove drone from dataSource list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickRemoveDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                //MessageBox.Show("are you sure you want to remove parcel?");
                blparcel.removeParcel(parcelBL.ID);
                parcelList.Show();
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
