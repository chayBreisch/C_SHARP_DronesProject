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
    /// Interaction logic for NewParcel.xaml
    /// </summary>
    public partial class NewParcel : Window
    {
        BlApi.IBL BLObject;
        BO.Customer Customer;
        public NewParcel(BlApi.IBL blobject , BO.Customer customer)
        {
            BLObject = blobject;
            Customer = customer;
            InitializeComponent();
            weightCombo.ItemsSource = BLObject.GetweightCategoriesEnumItem();
            priorityCombo.ItemsSource = BLObject.GetPrioritiesEnumItem();
            customers.Visibility = Visibility.Hidden;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string firsts = targetName.Text;
            customers.Visibility = Visibility.Visible;
            customers.ItemsSource = BLObject.GetCustomerNamesByCondition(C => { return C.Name.IndexOf(firsts) == 0; });
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
