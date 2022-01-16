using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BL;
using System.Windows;

namespace PL
{
    internal class Customer_: DependencyObject
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int SumOfParcelsSendedAndProvided { get; set; }
        public int SumOfParcelsSendedAndNotProvided { get; set; }
        public int SumOfParcelsRecieved { get; set; }
        public int SumOfParcelsOnTheWay { get; set; }



        /*public static readonly DependencyProperty FirstNameProperty =
        DependencyProperty.Register("FirstName",
                                    typeof(object),
                                    typeof(Customer_),
                                    new UIPropertyMetadata(0));
        public string Name
        {
            get
            {
                return (string)GetValue(FirstNameProperty);
            }
            set
            {
                SetValue(FirstNameProperty, value);
            }
        }*/



        public Customer_(BO.CustomerToList customer)
        {
            this.ID = customer.ID;
            this.Name = customer.Name;
            this.Phone = customer.Phone;
            this.SumOfParcelsSendedAndProvided = customer.SumOfParcelsSendedAndProvided;
            this.SumOfParcelsSendedAndNotProvided = customer.SumOfParcelsSendedAndNotProvided;
            this.SumOfParcelsRecieved = customer.SumOfParcelsRecieved;
            this.SumOfParcelsOnTheWay = customer.SumOfParcelsOnTheWay;

    }

}
}
