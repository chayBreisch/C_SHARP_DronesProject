using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public DroneToList(Drone drone)
            {
                ID = drone.ID;
                Model = drone.Model;
                Weight = drone.Weight;
                BatteryStatus = drone.BatteryStatus;
                Location = drone.Location;
                //NumOfParcelTrans = drone.///////////////////////////////////////////
            }
            public int ID { get; set; }
            public string Model { get; set; }
            public IDAL.DO.WeightCatagories Weight { get; set; }
            public double BatteryStatus { get; set; }
            public DroneStatus DroneStatus { get; set; }
            public LocationBL Location { get; set; }
            public int NumOfParcelTrans { get; set; }

            public override string ToString()
            {
                return $"DroneToList: ID: {ID}, Model: {Model}, Weight: {Weight}, BatteryStatus: {BatteryStatus}, DroneStatus: {DroneStatus}," +
                    $"Location: {Location}, NumOfParcelTrans: {NumOfParcelTrans}";
            }
        }
    }
}