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
                SumOfParcelsSendedAndNotProvided = dalObject.getParceleByCondition(p => p.SenderID == ID && p.Delivered != null).Count();
                SumOfParcelsSendedAndNotProvided = dalObject.getParceleByCondition(p => p.SenderID == ID && p.Delivered == null).Count();
                SumOfParcelsRecieved = dalObject.getParceleByCondition(p => p.TargetID == ID && p.PickedUp != null).Count();
                SumOfParcelsOnTheWay = dalObject.getParceleByCondition(p => p.TargetID == ID && p.Requested != null && p.PickedUp == null).Count();
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
