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
        BlApi.Bl BLObject;
        public NewParcel(BlApi.Bl blobject)
        {
            BLObject = blobject;
            InitializeComponent();
            weightCombo.ItemsSource = BLObject.GetweightCategoriesEnumItem();
            priorityCombo.ItemsSource = BLObject.GetPrioritiesEnumItem();
            customers.Visibility = Visibility.Hidden;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            customers.ItemsSource = BLObject.
        }
    }
}
