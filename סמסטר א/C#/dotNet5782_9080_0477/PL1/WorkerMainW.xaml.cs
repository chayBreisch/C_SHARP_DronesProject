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
using BL;

namespace PL1
{
    /// <summary>
    /// Interaction logic for WorkerMainW.xaml
    /// </summary>
    public partial class WorkerMainW : Window
    {
        public WorkerMainW()
        {
            BlApi.IBL BLObject;
            Window ParentWindow;


            /// <summary>
            /// constructor
            /// </summary>
            public WorkerMainW(BlApi.IBL blobject, Window parentWindow)
            {
                try
                {
                    InitializeComponent();
                    BLObject = blobject;
                    ParentWindow = parentWindow;
                }
                catch (ExceptionsBL.CantReturnBLObject e)
                {
                    throw new ExceptionsBL.CantReturnBLObject(e);
                }
            }

            /// <summary>
            /// open the droneList window
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Button_Click_DroneList(object sender, RoutedEventArgs e)
            {
                new DroneList(BLObject, this).Show();
                Hide();
            }

            /// <summary>
            /// open the StationList window
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Button_ClickStationList(object sender, RoutedEventArgs e)
            {
                new StationList(BLObject, this).Show();
                Hide();
            }

            /// <summary>
            /// open the ParcelList window
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Button_ClickParcelList(object sender, RoutedEventArgs e)
            {
                new ParcelList(BLObject, this).Show();
                Hide();
            }

            /// <summary>
            /// open the CustomerList window
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Button_ClickCustomerList(object sender, RoutedEventArgs e)
            {
                new CustomerList(BLObject, this).Show();
                Hide();
            }
        }
    }
}