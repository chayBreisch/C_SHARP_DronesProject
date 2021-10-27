using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public Parcel()
            {
                ID = 0;
                SenderID = 0;
                TargetID = 0;
                Weight = 0;
                Priority =0;
                Requested = new DateTime();
                DroneID = 0;
                Scheduled = new DateTime();
                PickedUp = new DateTime();
                Delivered = new DateTime();
            }
            public Parcel(int id, int senderId, int targetId, WeightCatagories weight, Priorities priority, DateTime requested = new DateTime(), int droneID  = 0, DateTime scheduled = new DateTime(), DateTime pickedUp = new DateTime(), DateTime delivered = new DateTime())
            { 
                ID = id;
                SenderID = senderId;
                TargetID = targetId;
                Weight = weight;
                Priority = priority;
                Requested = requested;
                DroneID = droneID;
                Scheduled = scheduled;
                PickedUp = pickedUp;
                Delivered = delivered;
            }
            public int ID { get; set; }
            public int SenderID { get; set; }
            public int TargetID { get; set; }
            public WeightCatagories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public int DroneID { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public override string ToString()
            {
                return $"ID: {ID}, Priority: {Priority}, SenderID: {SenderID}, TargetID: {TargetID}, Weight: {Weight}, droneId: {DroneID}"; ;
            }
        }
    }
}
