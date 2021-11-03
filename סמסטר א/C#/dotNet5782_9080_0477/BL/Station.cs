using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace IBL
{
    namespace BO
    {
        public class Station
        {
            IDAL.DO.Station station;

            public Station()
            {
                station = new IDAL.DO.Station();
            }

            public Location location { get; set; }
            List<DroneInCharger> dronesInCharge = new List<DroneInCharger>();
            public override string ToString()
            {
                return $"customer: {station.Name} : {station.ID}";
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
