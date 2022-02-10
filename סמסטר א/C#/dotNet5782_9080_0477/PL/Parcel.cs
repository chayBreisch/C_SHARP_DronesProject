using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BL;
using DO;
using System.Windows;

namespace PL
{
    public class Parcel_ : DependencyObject
    {
        public int ID { get; set; }
        public string NameCustomerSender { get; set; }
        public string NameCustomerReciver { get; set; }
        public DO.WeightCatagories Weight { get; set; }
        public DO.Priorities Priority { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public int droneId { get; set; }
        public DateTime? Requesed { get; set; }
        public DateTime? Scheduled { get; set; }


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
            Requesed = parcel.Requesed;
            Scheduled = parcel.Scheduled;
            PickedUp = parcel.PickedUp;
            Delivered = parcel.Delivered;


        }
    }
}
