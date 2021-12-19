using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using static BL.ExceptionsBL;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public Parcel(int id, WeightCatagories weight, Priorities priorities, DateTime? requested, DateTime? scheduled, DateTime? delivered, DateTime? pickedUp, Drone drone, ulong SenderId, ulong RecieverId, IDAL.IDal dal)
            {
                ID = id;
                Sender = new CustomerAtParcel(SenderId, dal.getCustomerById(c => c.ID == SenderId).Name);
                Reciever = new CustomerAtParcel(RecieverId, dal.getCustomerById(c => c.ID == RecieverId).Name);
                Weight = weight;
                Priorities = priorities;
                PickedUp = pickedUp;
                Drone = drone;
                Requesed = requested;
                Scheduled = scheduled;
                Delivered = delivered;
            }
            public Parcel()
            { 
                ID = 0;
                Sender = new CustomerAtParcel();
                Reciever = new CustomerAtParcel();
                Weight = 0;
                Priorities = 0;
                PickedUp = null;
                Drone = new Drone();
                Requesed = null;
                Scheduled = null;
                Delivered = null;
            }
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
            public CustomerAtParcel Sender { get; set; }
            public CustomerAtParcel Reciever { get; set; }
            private DO.WeightCatagories weight { get; set; }
            public DO.WeightCatagories Weight
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
