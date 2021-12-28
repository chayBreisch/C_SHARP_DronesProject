using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelAtCustomer
    {
        public ParcelAtCustomer(BO.Parcel parcel, ulong myId, IDAL.IDal dal)
        {
            ID = parcel.ID;
            Weight = parcel.Weight;
            Priority = parcel.Priorities;
            ParcelStatus = parcel.Delivered != null ? ParcelStatus.Requesed :
                parcel.PickedUp != null ? ParcelStatus.PickedUp :
                parcel.Scheduled != null ? ParcelStatus.Scheduled : ParcelStatus.Requesed;
            customerAtParcel = parcel.Sender.ID == myId ? new CustomerAtParcel(myId, dal.GetCustomerById(c => c.ID == myId).Name) :
                new CustomerAtParcel(parcel.Reciever.ID, dal.GetCustomerById(c => c.ID == parcel.Reciever.ID).Name);
        }
        public int ID { get; set; }
        public DO.WeightCatagories Weight { get; set; }
        public DO.Priorities Priority { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public CustomerAtParcel customerAtParcel { get; set; }
        public override string ToString()
        {
            return $"ParcelAtCustomer: ID: {ID}, Weight: {Weight}, Priority: {Priority}, ParcelStatus: {ParcelStatus}, customerAtParcel: {customerAtParcel}";
        }
    }
}