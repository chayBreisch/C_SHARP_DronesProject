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
        public class StationBL
        {


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
                        throw new OutOfRange("station id");
                    Id = value;
                }
            }
            public int Name { get; set; }
            //public int ChargeSlots { get; set; }
            private int chargeSlots { get; set; }

            public int ChargeSlots
            {
                get
                {
                    return chargeSlots;
                }
                set
                {
                    if (value < 0)
                        throw new OutOfRange("station charge slots");
                    chargeSlots = value;
                }
            }
            public LocationBL Location { get; set; }
            public List<DroneInCharger> DronesInCharge { get; set; }
            public override string ToString()
            {
                return $"station: ID: {ID} Name: {Name} ChargeSlots: {ChargeSlots} Location: {Location},  ";
            }
            /*public Station()
            {
                ID = 0;
                Name = 0;
                Longitude = 0;
                Latitude = 0;
                ChargeSlots = 0;
            }
            public Station(int id = 0, int name, double longitude, double latitude, int chargeSlots)
            {
                ID = id;
                Name = name;
                Longitude = longitude;
                Latitude = latitude;
                ChargeSlots = chargeSlots;
            }
            public int ID { get; set; }
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public int ChargeSlots { get; set; }
            public override string ToString()
            {
                return $"id: {ID}, Name: {Name}, Longitude: {Longitude}, Latitude: {Latitude}, ChargeSlots: {ChargeSlots}";
            }*/
        }
    }
}
