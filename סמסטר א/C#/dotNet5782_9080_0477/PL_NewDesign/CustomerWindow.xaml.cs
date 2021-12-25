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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BlApi.Bl BLObject;
        Window ParentWindow;
        BO.Customer Customer;
        public CustomerWindow(BlApi.Bl Blobject, Window parentWindow, BO.Customer customer)
        {
            InitializeComponent();
            BLObject = Blobject;
            ParentWindow = parentWindow;
            Customer = customer;
            headTitle.Content += Customer.Name;
        }

        private void Button_ClickAllParcels(object sender, RoutedEventArgs e)
        {
            theList.SelectedItem = BLObject.GetParcelToListByCondition((P) => { return P.NameCustomerReciver == Customer.Name || P.NameCustomerSender == Customer.Name; });
        }
    }
}
