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
    public class Customer_ : DependencyObject
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public LocationBL Location { get; set; }
        public List<ParcelAtCustomer> parcelSendedByCustomer { get; set; }
        public List<ParcelAtCustomer> parcelSendedToCustomer { get; set; }
        public bool IsActive { get; set; }



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



        public Customer_(BO.Customer customer)
        {
            this.ID = customer.ID;
            this.Name = customer.Name;
            this.Phone = customer.Phone;
            this.Location = new LocationBL() { Longitude = customer.Location.Longitude, Latitude = customer.Location.Latitude };
            this.parcelSendedByCustomer = new List<ParcelAtCustomer>(customer.parcelSendedByCustomer);
            this.parcelSendedToCustomer = new List<ParcelAtCustomer>(customer.parcelSendedToCustomer);
            this.IsActive = customer.IsActive;
        }
    }
}
