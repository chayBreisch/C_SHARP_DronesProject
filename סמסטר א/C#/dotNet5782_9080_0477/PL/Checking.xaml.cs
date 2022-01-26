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

namespace PL
{
    /// <summary>
    /// Interaction logic for Checking.xaml
    /// </summary>
    public partial class Checking : Window
    {
        BlApi.IBL BLObject;
        char CheckIdentity;
        public Checking(BlApi.IBL Blobject, char c)
        {
            InitializeComponent();
            BLObject = Blobject;
            CheckIdentity = c;
        }

        private void Button_ClickComeIn(object sender, RoutedEventArgs e)
        {
            try
            {
                ulong ID = (ulong)Convert.ToInt32(checkID.Text);
                string name = Convert.ToString(chechName.Text);
                BO.Customer customer = BLObject.GetSpecificCustomerBL(p => p.ID == ID);
                if (customer.Name != name) { throw new Exception(); }
                if (CheckIdentity == 'w')
                {
                    if (Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("enter worker password")) != 12345) { throw new Exception("wrong password"); }
                    new WorkerMain(BLObject, new MainWindow()).Show();
                    this.Close();
                }
                else
                {
                    new Customer(BLObject, BLObject.GetSpecificCustomerBL(p => p.ID == ID), CheckIdentity).Show();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("wrong user name or Id");
            }
        }
    }
}

