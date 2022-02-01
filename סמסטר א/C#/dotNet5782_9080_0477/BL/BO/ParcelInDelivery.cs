using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class ParcelInDelivery
    {
        public ParcelInDelivery(BO.Parcel parcel, IDAL.IDal dalObject)
        {
            ID = parcel.ID;
            Weight = parcel.Weight;
            Priority = parcel.Priorities;
            isWaiting = parcel.PickedUp == null;
            CustomerInDeliverySender = parcel.Sender;
            CustomerInDeliveryReciever = parcel.Reciever;
            DO.Customer customer = dalObject.GetCustomerById(c => c.ID == parcel.Sender.ID);
            CollectLocation = new LocationBL(customer.Longitude, customer.Latitude);
            customer = dalObject.GetCustomerById(c => c.ID == parcel.Reciever.ID);
            TargetLocation = new LocationBL(customer.Longitude, customer.Latitude);
            TransportDistance = BL.BL.distance(CollectLocation, TargetLocation);
        }
        public int ID { get; set; }
        public DO.WeightCatagories Weight { get; set; }
        public DO.Priorities Priority { get; set; }
        public bool isWaiting { get; set; }
        public CustomerAtParcel CustomerInDeliverySender { get; set; }
        public CustomerAtParcel CustomerInDeliveryReciever { get; set; }
        public LocationBL CollectLocation { get; set; }
        public LocationBL TargetLocation { get; set; }
        public double TransportDistance { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Weight: {Weight}, Priority: {Priority}, ParcelStatus: {isWaiting}" +
                $", CollectLocation: {CollectLocation}, CollectLocation: {CollectLocation}, DeliveryDestinationLocation: {TargetLocation}" +
                $"TransportDistance: {Math.Floor(TransportDistance)}";
        }
    }
}