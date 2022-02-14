using BO;
using System;
using System.Windows;

namespace PL
{
    public class Parcel_ : DependencyObject
    {
        public int ID { get; set; }


        public static readonly DependencyProperty NameCustomerSenderProperty =
           DependencyProperty.Register("NameCustomerSender",
               typeof(object),
               typeof(Parcel_),
               new UIPropertyMetadata(0));
        public string NameCustomerSender
        {
            get
            {
                return (string)GetValue(NameCustomerSenderProperty);
            }
            set
            {
                SetValue(NameCustomerSenderProperty, value);
            }
        }


        public static readonly DependencyProperty NameCustomerReciverProperty =
           DependencyProperty.Register("NameCustomerReciver",
               typeof(object),
               typeof(Parcel_),
               new UIPropertyMetadata(0));
        public string NameCustomerReciver {
            get
            {
                return (string)GetValue(NameCustomerReciverProperty);
            }
            set
            {
                SetValue(NameCustomerReciverProperty, value);
            }
        }


        public static readonly DependencyProperty WeightProperty =
            DependencyProperty.Register("Weight",
                typeof(object),
                typeof(Parcel_),
                new UIPropertyMetadata(0));

        public DO.WeightCatagories Weight
        {
            get
            {
                return (DO.WeightCatagories)GetValue(WeightProperty);
            }
            set
            {
                SetValue(WeightProperty, value);
            }
        }

        public static readonly DependencyProperty PriorityProperty =
            DependencyProperty.Register("Priority",
                typeof(object),
                typeof(Parcel_),
                new UIPropertyMetadata(0));

        public DO.Priorities Priority {
            get
            {
                return (DO.Priorities)GetValue(PriorityProperty);
            }
            set
            {
                SetValue(PriorityProperty, value);
            }
        }

        public static readonly DependencyProperty ParcelStatusProperty =
            DependencyProperty.Register("ParcelStatus",
                typeof(object),
                typeof(Parcel_),
                new UIPropertyMetadata(0));

        public ParcelStatus ParcelStatus
        {
            get
            {
                return (ParcelStatus)GetValue(ParcelStatusProperty);
            }
            set
            {
                SetValue(ParcelStatusProperty, value);
            }
        }


        public static readonly DependencyProperty droneIdProperty =
            DependencyProperty.Register("droneId",
                typeof(object),
                typeof(Parcel_),
                new UIPropertyMetadata(0));

        public int droneId
        {
            get
            {
                return (int)GetValue(droneIdProperty);
            }
            set
            {
                SetValue(droneIdProperty, value);
            }
        }



        public static readonly DependencyProperty RequestedProperty =
            DependencyProperty.Register("Requested",
                typeof(object),
                typeof(Parcel_),
                new UIPropertyMetadata(0));
        public DateTime? Requested {
            get
            {
                return (DateTime?)GetValue(RequestedProperty);
            }
            set
            {
                SetValue(RequestedProperty, value);
            }
        }



        public static readonly DependencyProperty ScheduledProperty =
            DependencyProperty.Register("Scheduled",
                typeof(object),
                typeof(Parcel_),
                new UIPropertyMetadata(0));
        public DateTime? Scheduled {
            get
            {
                return (DateTime?)GetValue(ScheduledProperty);
            }
            set
            {
                SetValue(ScheduledProperty, value);
            }
        }


        public static readonly DependencyProperty PickedUpProperty =
            DependencyProperty.Register("PickedUp",
                typeof(object),
                typeof(Parcel_),
                new UIPropertyMetadata(0));


        public DateTime? PickedUp {
            get {
                return (DateTime?)GetValue(PickedUpProperty);
            }
            set {
                SetValue(PickedUpProperty, value);
            }
        }

        public static readonly DependencyProperty DeliveredProperty =
            DependencyProperty.Register("Delivered",
                typeof(object),
                typeof(Parcel_),
                new UIPropertyMetadata(0));

        public DateTime? Delivered {
            get
            {
                return (DateTime?)GetValue(DeliveredProperty);
            }
            set
            {
                SetValue(DeliveredProperty, value);
            }
        }
        public string content { get; set; }
        public string pathTo { get; set; }
        public Parcel_(BO.Parcel parcel)
        {

            ID = parcel.ID;
            NameCustomerSender = parcel.Sender.CustomerName;
            NameCustomerReciver = parcel.Reciever.CustomerName;
            Weight = parcel.Weight;
            Priority = parcel.Priorities;
            droneId = parcel.Drone.ID;
            Requested = parcel.Requesed;
            Scheduled = parcel.Scheduled;
            PickedUp = parcel.PickedUp;
            Delivered = parcel.Delivered;
            ParcelStatus = Scheduled == null ? ParcelStatus.Requesed : PickedUp == null ? ParcelStatus.Scheduled : Delivered == null ? ParcelStatus.PickedUp : ParcelStatus.Delivered;

        }
    }
}
