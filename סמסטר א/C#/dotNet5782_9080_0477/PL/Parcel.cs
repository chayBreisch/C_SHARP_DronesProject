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
    internal class Parcel_: DependencyObject
    {
        public int ID { get; set; }
        public string NameCustomerSender { get; set; }
        public string NameCustomerReciver { get; set; }
        public DO.WeightCatagories Weight { get; set; }
        public DO.Priorities Priority { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public string content { get; set; }
        public string pathTo { get; set; }
        public Parcel_(BO.ParcelToList parcel)
        {

            this.ID = parcel.ID;
            this.NameCustomerSender = parcel.NameCustomerSender;
            this.NameCustomerReciver = parcel.NameCustomerReciver;
            this.Weight = parcel.Weight;
            this.Priority = parcel.Priority;
            this.ParcelStatus = parcel.ParcelStatus;
            this.content = parcel.content;
            this.pathTo = parcel.pathTo;


    }
    }
}
