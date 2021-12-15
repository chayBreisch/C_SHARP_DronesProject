using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static BL.ExceptionsBL;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public enum ParcelStatus { Requesed, Scheduled, PickedUp, Delivered }
            private int Id { get; set; }

            public int ID
            {
                get
                {
                    return Id;
                }
                set
                {
                    if (value < 0)
                        throw new OutOfRange("parcel id");
                    Id = value;
                }
            }
            public Customer Sender { get; set; }
            public Customer Reciever { get; set; }
            private IDAL.DO.WeightCatagories weight { get; set; }
            public IDAL.DO.WeightCatagories Weight
            {
                get
                {
                    return weight;
                }
                set
                {
                    if (value < 0)
                        throw new OutOfRange("weight");
                    weight = value;
                }
            }
            public Priorities Priorities { get; set; }
            public Drone Drone { get; set; }
            public DateTime? Requesed { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }


            public override string ToString()
            {
                return $"ID: {ID}, senderId: {Sender.ID}, recieverId: {Reciever.ID},Weight: {Weight}, Priorities: {Priorities}" +
                    $"Drone: {Drone}, Requesed: {Requesed}, Scheduled: {Scheduled}, PickedUp: {PickedUp}, Delivered: {Delivered}\n\n";


            }
        }
    }
}
