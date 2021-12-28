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

namespace PL1
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>


    public partial class Parcel : Window
    {
        Window ParentWindow;
        BlApi.Bl blparcel;
        BO.Parcel parcelBL;
        /// <summary>
        /// constructor add parcel
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneList"></param>
        public Parcel(BlApi.Bl bl, Window parentWindow)
        {
            ParentWindow = parentWindow;
            blparcel = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            addStation.Visibility = Visibility.Visible;
            weightParcel.ItemsSource = blparcel.GetweightCategoriesEnumItem();
            weightParcel.SelectedItem = blparcel.GetweightCategoriesEnumItem().GetValue(1);
            priorityParcel.ItemsSource = blparcel.GetPrioritiesEnumItem();
            priorityParcel.SelectedItem = blparcel.GetPrioritiesEnumItem().GetValue(0);
        }

        /// <summary>
        /// constructor opened from drone
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="parentWindow"></param>
        public Parcel(BlApi.Bl bl, BO.Parcel parcel, Window parentWindow)
        {
            InitializeComponent();
            actions.Visibility = Visibility.Visible;
            addStation.Visibility = Visibility.Hidden;
            WindowStyle = WindowStyle.None;
            ParentWindow = parentWindow;
            blparcel = bl;
            parcelBL = parcel;
            idparcel.Text = parcelBL.ID.ToString();
            senderidparcel.Text = parcelBL.Sender.ID.ToString();
            recieveridparcel.Text = parcelBL.Reciever.ID.ToString();
            weightparcel.Text = parcelBL.Weight.ToString();
            priorityparcel.Text = parcelBL.Priorities.ToString();
            droneparcel.Text = parcelBL.Drone.ToString();
            if (parcelBL.Drone.ID == 0)
                droneparcel.Text = "no drone";
            RequesedParcel.Text = parcelBL.Requesed.ToString();
            ScheduledParcel.Text = parcelBL.Scheduled.ToString();
            pickedUpParcel.Text = parcelBL.PickedUp.ToString();
            DeliveredParcel.Text = parcelBL.Delivered.ToString();
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
                //pickedUpParcel.Text = parcelBL.PickedUp.ToString();
                checkBoxDelivered.Visibility = Visibility.Visible;
                checkBoxPicked.Visibility = Visibility.Hidden;
            }
            else
            {
                checkBoxPicked.Visibility = Visibility.Hidden;
                checkBoxDelivered.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// constructor actions parcel
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="parcel"></param>
        /// <param name="parentWindow"></param>
        /*public Parcel(BlApi.Bl bl, BO.Parcel parcel, Window parentWindow)
        {
            InitializeComponent();
            actions.Visibility = Visibility.Visible;
            addStation.Visibility = Visibility.Hidden;
            WindowStyle = WindowStyle.None;
            ParentWindow = parentWindow;
            blparcel = bl;
            parcelBL = parcel;
            idparcel.Text = parcelBL.ID.ToString();
            senderidparcel.Text = parcelBL.Sender.ID.ToString();
            recieveridparcel.Text = parcelBL.Reciever.ID.ToString();
            weightparcel.Text = parcelBL.Weight.ToString();
            priorityparcel.Text = parcelBL.Priorities.ToString();
            if (parcelBL.Drone != null)
                droneparcel.Text = parcelBL.Drone.ID.ToString();
            statusparcel.Text = blparcel.findParcelStatus(parcelBL).ToString();
        }*/



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
                if (getSenderId() == getRecieverId())
                {
                    MessageBox.Show("sender id and reciever id can't be the same");
                }
                blparcel.AddParcel(getSenderId(), getRecieverId(), weightParcel.SelectedIndex + 1, priorityParcel.SelectedIndex);
                MessageBox.Show("you added succefuly");
                ParentWindow.Show();
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
            //ParentWindow.Refresh();
            //refreshList(this.parcelBL);
            //ParentWindow.ParcelListView.Items.Refresh();
            ParentWindow.Show();
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
                    blparcel.RemoveParcel(parcelBL.ID);
                    Button_ClickCloseParcel(sender, e);
                    /* ParentWindow.Show();
                     Close();*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nparcel is connected to a drone");
            }
        }

        /// <summary>
        /// open sender window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickOpenSender(object sender, RoutedEventArgs e)
        {
            new Customer(blparcel, blparcel.GetSpecificCustomerBL(p => p.ID == parcelBL.Sender.ID), this).Show();
            Hide();
        }

        /// <summary>
        /// open reciever window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickOpenReciever(object sender, RoutedEventArgs e)
        {
            new Customer(blparcel, blparcel.GetSpecificCustomerBL(p => p.ID == parcelBL.Reciever.ID), this).Show();
            Hide();
        }

        /// <summary>
        /// open drone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickOpenDrone(object sender, RoutedEventArgs e)
        {
            new Drone(blparcel, parcelBL.Drone, this).Show();
            Hide();
        }

        private void checkBoxPicked_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxPicked.Visibility = Visibility.Hidden;
            checkBoxDelivered.Visibility = Visibility.Visible;
            parcelBL.PickedUp = DateTime.Now;
            pickedUpParcel.Text = parcelBL.PickedUp.ToString();
            blparcel.updateParecl(parcelBL);
        }

        private void checkBoxDelivered_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxDelivered.Visibility = Visibility.Hidden;
            parcelBL.Delivered = DateTime.Now;
            DeliveredParcel.Text = parcelBL.Delivered.ToString();
            blparcel.updateParecl(parcelBL);
        }

        /// <summary>
        /// refrsh the listView in the window
        /// </summary>
        /// <param name="parcel"></param>
        /*private void refreshList(BO.Parcel parcel)
         {
             // view.GroupDescriptions.Clear();

          /*   if (ParentWindow.parcelPriority.SelectedItem != null)
             {
                 if ((ParentWindow.parcelPriority.SelectedItem.ToString() == parcel.Priorities.ToString()))
                 {
                     {
                         ObservableCollection<BO.ParcelToList> MyList = new ObservableCollection<BO.ParcelToList>();

                         foreach (var item in blparcel.getParcelToListByCondition((P) => P.Priority == parcel.Priorities))
                             MyList.Add(item);
                         DataContext = MyList;
                         ParentWindow.view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);

                         //ParentWindow.ParcelListView.ItemsSource = blparcel.getParcelToListByCondition((P) => P.Priority == parcel.Priorities);
                     }
                 }
             }
             else if (ParentWindow.parcelWeight.SelectedItem != null)
             {
                 if (ParentWindow.parcelWeight.SelectedItem.ToString() == parcel.Weight.ToString())
                 {
                     ObservableCollection<BO.ParcelToList> MyList = new ObservableCollection<BO.ParcelToList>();

                     foreach (var item in blparcel.getParcelToListByCondition((P) => P.Weight == parcel.Weight))
                         MyList.Add(item);
                     DataContext = MyList;
                     ParentWindow.view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);

                     //ParentWindow.ParcelListView.ItemsSource = blparcel.getParcelToListByCondition((p) => p.Weight == parcel.Weight);
                 }

             }
             else
             {
                 ObservableCollection<BO.ParcelToList> MyList = new ObservableCollection<BO.ParcelToList>();
                 IEnumerable<BO.ParcelToList> p = blparcel.getParcelToList();
                 ParentWindow.view.GroupDescriptions.Clear();

                 foreach (var item in p)
                     MyList.Add(item);
                 DataContext = MyList;
                 ParentWindow.view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);

                 //ParentWindow.ParcelListView.ItemsSource = blparcel.getParcelToList();
             }
        }*/
    }
}
