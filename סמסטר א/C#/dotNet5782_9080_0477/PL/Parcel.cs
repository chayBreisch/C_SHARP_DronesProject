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
    public class Parcel_: DependencyObject
    {
        public int ID { get; set; }
        public string NameCustomerSender { get; set; }
        public string NameCustomerReciver { get; set; }
        public DO.WeightCatagories Weight { get; set; }
        public DO.Priorities Priority { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public string content { get; set; }
        public string pathTo { get; set; }
        public Parcel_(ParcelToList parcel)
        {

            ID = parcel.ID;
            NameCustomerSender = parcel.NameCustomerSender;
            NameCustomerReciver = parcel.NameCustomerReciver;
            Weight = parcel.Weight;
            Priority = parcel.Priority;
            ParcelStatus = parcel.ParcelStatus;
            content = parcel.content;
            pathTo = parcel.pathTo;


    }
    }
}
