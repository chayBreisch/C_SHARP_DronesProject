using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class DroneInParcel
    {
        public DroneInParcel(BO.Drone drone)
        {
            ID = drone.ID;
            BatteryStatus = drone.BatteryStatus;
            CurrentLocation = drone.Location;
        }
        public int ID { get; set; }
        public double BatteryStatus { get; set; }
        public LocationBL CurrentLocation { get; set; }

        public override string ToString()
        {
            return $"DroneInParcel: ID: {ID}, BatteryStatus: {BatteryStatus}, CurrentLocation: {CurrentLocation}";
        }
    }
}
