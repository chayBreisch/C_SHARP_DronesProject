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


        public static readonly DependencyProperty IDProperty =
        DependencyProperty.Register("ID",
                                    typeof(object),
                                    typeof(Customer_),
                                    new UIPropertyMetadata(0));
        public ulong ID
        {
            get
            {
                return (ulong)GetValue(IDProperty);
            }
            set
            {
                SetValue(IDProperty, value);
            }
        }


        public static readonly DependencyProperty NameProperty =
       DependencyProperty.Register("Name",
                                   typeof(object),
                                   typeof(Customer_),
                                   new UIPropertyMetadata(0));
       public string Name
       {
           get
           {
               return (string)GetValue(NameProperty);
           }
           set
           {
               SetValue(NameProperty, value);
           }
       }



        public static readonly DependencyProperty PhoneProperty =
       DependencyProperty.Register("Phone",
                                   typeof(object),
                                   typeof(Customer_),
                                   new UIPropertyMetadata(0));
       public string Phone
       {
           get
           {
               return (string)GetValue(PhoneProperty);
           }
           set
           {
               SetValue(PhoneProperty, value);
           }
       }


        public static readonly DependencyProperty LocationProperty =
       DependencyProperty.Register("Location",
                                   typeof(object),
                                   typeof(Customer_),
                                   new UIPropertyMetadata(0));
       public Location Location
        {
           get
           {
               return (Location)GetValue(LocationProperty);
           }
           set
           {
               SetValue(LocationProperty, value);
           }
       }


        public static readonly DependencyProperty parcelSendedByCustomerProperty =
       DependencyProperty.Register("parcelSendedByCustomer",
                                   typeof(object),
                                   typeof(Customer_),
                                   new UIPropertyMetadata(0));
       public List<ParcelAtCustomer> parcelSendedByCustomer
        {
           get
           {
               return (List<ParcelAtCustomer>)GetValue(parcelSendedByCustomerProperty);
           }
           set
           {
               SetValue(parcelSendedByCustomerProperty, value);
           }
       }


        public static readonly DependencyProperty parcelSendedToCustomerProperty =
       DependencyProperty.Register("parcelSendedToCustomer",
                                   typeof(object),
                                   typeof(Customer_),
                                   new UIPropertyMetadata(0));
        public List<ParcelAtCustomer> parcelSendedToCustomer
        {
           get
           {
               return (List<ParcelAtCustomer>)GetValue(parcelSendedToCustomerProperty);
           }
           set
           {
               SetValue(parcelSendedToCustomerProperty, value);
           }
       }


        public static readonly DependencyProperty IsActiveProperty =
       DependencyProperty.Register("IsActive",
                                   typeof(object),
                                   typeof(Customer_),
                                   new UIPropertyMetadata(0));
       public bool IsActive
        {
           get
           {
               return (bool)GetValue(IsActiveProperty);
           }
           set
           {
               SetValue(IsActiveProperty, value);
           }
        }



        public static readonly DependencyProperty SumOfParcelsSendedAndProvidedProperty =
      DependencyProperty.Register("SumOfParcelsSendedAndProvided",
                                  typeof(object),
                                  typeof(Customer_),
                                  new UIPropertyMetadata(0));

        public int SumOfParcelsSendedAndProvided
        {
            get
            {
                return (int)GetValue(SumOfParcelsSendedAndProvidedProperty);
            }
            set
            {
                SetValue(SumOfParcelsSendedAndProvidedProperty, value);
            }
        }



        public static readonly DependencyProperty SumOfParcelsSendedAndNotProvidedProperty =
      DependencyProperty.Register("SumOfParcelsSendedAndNotProvided",
                                  typeof(object),
                                  typeof(Customer_),
                                  new UIPropertyMetadata(0));

        public int SumOfParcelsSendedAndNotProvided
        {
            get
            {
                return (int)GetValue(SumOfParcelsSendedAndNotProvidedProperty);
            }
            set
            {
                SetValue(SumOfParcelsSendedAndNotProvidedProperty, value);
            }
        }


        public static readonly DependencyProperty SumOfParcelsOnTheWayProperty =
      DependencyProperty.Register("SumOfParcelsOnTheWay",
                                  typeof(object),
                                  typeof(Customer_),
                                  new UIPropertyMetadata(0));


        public int SumOfParcelsOnTheWay
        {
            get
            {
                return (int)GetValue(SumOfParcelsOnTheWayProperty);
            }
            set
            {
                SetValue(SumOfParcelsOnTheWayProperty, value);
            }
        }



        public static readonly DependencyProperty ParcelsRecievedProperty =
      DependencyProperty.Register("ParcelsRecieved",
                                  typeof(object),
                                  typeof(Customer_),
                                  new UIPropertyMetadata(0));

        public int ParcelsRecieved
        {
            get
            {
                return (int)GetValue(ParcelsRecievedProperty);
            }
            set
            {
                SetValue(ParcelsRecievedProperty, value);
            }
        }

        
                  
                
              


        public Customer_(BO.Customer customer)
        {
            this.ID = customer.ID;
            this.Name = customer.Name;
            this.Phone = customer.Phone;
            this.Location = new Location() { Longitude = customer.Location.Longitude, Latitude = customer.Location.Latitude };
            this.parcelSendedByCustomer = new List<ParcelAtCustomer>(customer.parcelSendedByCustomer);
            this.parcelSendedToCustomer = new List<ParcelAtCustomer>(customer.parcelSendedToCustomer);
            this.IsActive = customer.IsActive;
            this.SumOfParcelsSendedAndProvided = parcelSendedByCustomer.Where(P => P.ParcelStatus == ParcelStatus.Delivered).Count();
            this.SumOfParcelsSendedAndNotProvided = parcelSendedByCustomer.Where(P => P.ParcelStatus == ParcelStatus.PickedUp).Count();
            this.SumOfParcelsOnTheWay = parcelSendedByCustomer.Where(P => P.ParcelStatus == ParcelStatus.Requesed).Count();
            this.ParcelsRecieved = parcelSendedToCustomer.Where(P => P.ParcelStatus == ParcelStatus.Delivered).Count();
        }
    }
}
