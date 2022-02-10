using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class CustomerToList
    {
        public CustomerToList(Customer customer, IDAL.IDal dalObject)
        {
            ID = customer.ID;
            Name = customer.Name;
            Phone = customer.Phone;
            SumOfParcelsSendedAndProvided = customer.parcelSendedByCustomer.Where(p => p.ParcelStatus == ParcelStatus.Delivered).Count();//GetParcels
            SumOfParcelsSendedAndNotProvided = customer.parcelSendedByCustomer.Where(p => p.ParcelStatus != ParcelStatus.Delivered).Count();
            SumOfParcelsRecieved = customer.parcelSendedToCustomer.Where(p => p.ParcelStatus == ParcelStatus.Delivered).Count();
            SumOfParcelsOnTheWay = customer.parcelSendedToCustomer.Where(p => p.ParcelStatus == ParcelStatus.PickedUp).Count();

        }

        public CustomerToList(DO.Customer customer, IDAL.IDal dalObject)
        {
            ID = customer.ID;
            Name = customer.Name;
            Phone = customer.Phone;
            SumOfParcelsSendedAndProvided = dalObject.GetParcelesByCondition(p => p.SenderID == ID && p.Delivered != null).Count();//GetParcels
            SumOfParcelsSendedAndNotProvided = dalObject.GetParcelesByCondition(p => p.SenderID == ID && p.Delivered == null).Count();
            SumOfParcelsRecieved = dalObject.GetParcelesByCondition(p => p.TargetID == ID && p.Delivered != null).Count();
            SumOfParcelsOnTheWay = dalObject.GetParcelesByCondition(p => p.TargetID == ID && p.PickedUp != null && p.Delivered == null).Count();
        }

        public ulong ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int SumOfParcelsSendedAndProvided { get; set; }
        public int SumOfParcelsSendedAndNotProvided { get; set; }
        public int SumOfParcelsRecieved { get; set; }
        public int SumOfParcelsOnTheWay { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Phone: {Phone}, SumOfParcelsSendedAndProvided: {SumOfParcelsSendedAndProvided}" +
                $"SumOfParcelsSendedAndNotProvided: {SumOfParcelsSendedAndNotProvided}, SumOfParcelsRecieved: {SumOfParcelsRecieved}, SumOfParcelsOnTheWay: {SumOfParcelsOnTheWay}";
        }
    }
}
