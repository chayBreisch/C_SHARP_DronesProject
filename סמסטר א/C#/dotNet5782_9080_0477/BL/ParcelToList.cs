using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelToList
    {
        public ParcelToList(Parcel parcel, IDAL.IDal dalObject)
        {
            ID = parcel.ID;
            NameCustomerSender = dalObject.getCustomerById(c => c.ID == parcel.Sender.ID).Name;
            NameCustomerReciver = dalObject.getCustomerById(c => c.ID == parcel.Reciever.ID).Name;
            Weight = parcel.Weight;
            Priority = parcel.Priorities;
            ParcelStatus = parcel.Delivered != null ? ParcelStatus.Delivered :
            parcel.PickedUp != null ? ParcelStatus.pickedUp :
            parcel.Requesed != null ? ParcelStatus.Requesed : ParcelStatus.Scheduled;
        }
        public int ID { get; set; }
        public string NameCustomerSender { get; set; }
        public string NameCustomerReciver { get; set; }
        public DO.WeightCatagories Weight { get; set; }
        public DO.Priorities Priority { get; set; }
        public ParcelStatus ParcelStatus { get; set; }

        public override string ToString()
        {
            return $"*****************************************\nParcelToList: ID: {ID},\n NameCustomerSender: {NameCustomerSender},\n NameCustomerReciver: {NameCustomerReciver}" +
                $"\nWeight: {Weight},\n Priority: {Priority},\n ParcelStatus: {ParcelStatus}\n*****************************************";
        }
    }
}
