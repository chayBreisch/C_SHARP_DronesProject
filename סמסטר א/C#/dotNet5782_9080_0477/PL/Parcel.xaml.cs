using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>


    public partial class Parcel : Window
    {
        BlApi.IBL BLObject;
        BO.Parcel parcelBL;
        BO.Customer Customer;
        PLLists PLLists;
        Parcel_ parcelPL;

        /// <summary>
        /// constructor add parcel
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneList"></param>
        public Parcel(BlApi.IBL bl, PLLists pllists)
        {
            BLObject = bl;
            PLLists = pllists;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            CustomerSendParcel.Visibility = Visibility.Hidden;
            addParcel.Visibility = Visibility.Visible;
            weightParcel.ItemsSource = BLObject.GetweightCategoriesEnumItem();
            weightParcel.SelectedItem = BLObject.GetweightCategoriesEnumItem().GetValue(1);
            priorityParcel.ItemsSource = BLObject.GetPrioritiesEnumItem();
            priorityParcel.SelectedItem = BLObject.GetPrioritiesEnumItem().GetValue(0);
        }

        /// <summary>
        /// constructor opened from drone
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="parentWindow"></param>
        public Parcel(BlApi.IBL bl, BO.Parcel parcel, PLLists pllists)
        {
            InitializeComponent();
            PLLists = pllists;
            CustomerSendParcel.Visibility = Visibility.Hidden;
            actions.Visibility = Visibility.Visible;
            addParcel.Visibility = Visibility.Hidden;
            WindowStyle = WindowStyle.None;
            BLObject = bl;
            parcelBL = parcel;
            parcelPL = new Parcel_(parcelBL);
            DataContext = parcelPL;
            if (parcelBL.Drone.ID == 0)
                droneparcel.Text = "no drone";
            if (parcelBL.Scheduled == null)
            {
                checkBoxPicked.Visibility = Visibility.Hidden;
                checkBoxDelivered.Visibility = Visibility.Hidden;
            }
            else if (parcelBL.PickedUp == null)
            {
                checkBoxPicked.Visibility = Visibility.Visible;
                checkBoxDelivered.Visibility = Visibility.Hidden;

            }
            else if (parcelBL.Delivered == null)
            {
                checkBoxDelivered.Visibility = Visibility.Visible;
                checkBoxPicked.Visibility = Visibility.Hidden;
            }
            else
            {
                checkBoxPicked.Visibility = Visibility.Hidden;
                checkBoxDelivered.Visibility = Visibility.Hidden;
            }
        }

        public Parcel(BlApi.IBL blobject, BO.Customer customer, PLLists pllists)
        {
            BLObject = blobject;
            Customer = customer;
            PLLists = pllists;
            InitializeComponent();
            actions.Visibility = Visibility.Hidden;
            addParcel.Visibility = Visibility.Hidden;
            weightCombo.ItemsSource = BLObject.GetweightCategoriesEnumItem();
            priorityCombo.ItemsSource = BLObject.GetPrioritiesEnumItem();
            CustomerSendParcel.Visibility = Visibility.Visible;
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
                return ulong.Parse(recieverIdParcel.Text);
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
                if (getSenderId() == getRecieverId())
                {
                    MessageBox.Show("sender id and reciever id can't be the same");
                }
               int id = BLObject.AddParcel(getSenderId(), getRecieverId(), weightParcel.SelectedIndex + 1, priorityParcel.SelectedIndex);
                MessageBox.Show("you added succefuly");
                PLLists.AddParcel(BLObject.GetParcelsByCondition(P=>P.ID == id).FirstOrDefault());
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
        private void Button_ClickRemoveParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("are you sure you want to remove parcel?", "remove parcel", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    BLObject.RemoveParcel(parcelBL.ID);
                    Button_ClickCloseParcel(sender, e);
                    parcelPL.IsActive = false;
                    PLLists.UpdateParcel(parcelPL);
                    /* ParentWindow.Show();
                     Close();*/
                    MessageBox.Show("parcel removed sucssesfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("can't remove parcel because it's connected to a drone");
            }
        }

        /// <summary>
        /// open sender window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickOpenSender(object sender, RoutedEventArgs e)
        {
            if (BLObject.findParcelStatus(parcelBL) == BO.ParcelStatus.Scheduled || BLObject.findParcelStatus(parcelBL) == BO.ParcelStatus.PickedUp)
            {

                var win = new Customer(BLObject, BLObject.GetSpecificCustomerBL(p => p.ID == parcelBL.Sender.ID), 'w', PLLists);
                Visibility = Visibility.Hidden;
                win.ShowDialog();
                Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// open reciever window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickOpenReciever(object sender, RoutedEventArgs e)
        {
            if (BLObject.findParcelStatus(parcelBL) == BO.ParcelStatus.Scheduled || BLObject.findParcelStatus(parcelBL) == BO.ParcelStatus.PickedUp)
            {
                var win = new Customer(BLObject, BLObject.GetSpecificCustomerBL(p => p.ID == parcelBL.Reciever.ID), 'w', PLLists);
                Visibility = Visibility.Hidden;
                win.ShowDialog();
                Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// open drone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickOpenDrone(object sender, RoutedEventArgs e)
        {
            if (parcelBL.Drone.ID != 0)
            {
                Visibility = Visibility.Hidden;
                Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// update picked up parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxPicked_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxPicked.Visibility = Visibility.Hidden;
            checkBoxDelivered.Visibility = Visibility.Visible;
            parcelBL.PickedUp = DateTime.Now;
            BLObject.updateParecl(parcelBL);
            parcelPL.PickedUp = parcelBL.PickedUp;
            PLLists.UpdateParcel(parcelPL);
        }

        /// <summary>
        /// update deliverd parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxDelivered_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxDelivered.Visibility = Visibility.Hidden;
            parcelBL.Delivered = DateTime.Now;
            BLObject.updateParecl(parcelBL);
            parcelPL.Delivered = parcelBL.PickedUp;
            PLLists.UpdateParcel(parcelPL);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string firsts = targetName.Text;
            customers.Visibility = Visibility.Visible;
            customers.ItemsSource = BLObject.GetCustomersNamesByCondition(C => { return C.Name.IndexOf(firsts) == 0; });
        }


        private void MouseDoubleClick_chose(object sender, RoutedEventArgs e)
        {
            targetName.Text = (sender as ListView).SelectedValue as string;
            customers.Visibility = Visibility.Hidden;
        }

        private void Button_ClickSend(object sender, RoutedEventArgs e)
        {
            try
            {
                if (weightCombo.SelectedItem == null || priorityCombo == null) { throw new Exception("fill the details"); }


                BLObject.AddParcel(Customer.ID, BLObject.GetSpecificCustomerBL(C => { return C.Name == targetName.Text; }).ID, weightCombo.SelectedIndex + 1, priorityCombo.SelectedIndex + 1);
                this.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
    }
}
