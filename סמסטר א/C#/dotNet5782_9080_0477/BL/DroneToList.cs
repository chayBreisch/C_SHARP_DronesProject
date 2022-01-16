using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneToList
    {
        public DroneToList(Drone drone, IDAL.IDal dalObject)
        {
            ID = drone.ID;
            Model = drone.Model;
            Weight = drone.Weight;
            DroneStatus = drone.DroneStatus;
            BatteryStatus = drone.BatteryStatus;
            Location = drone.Location;
            NumOfParcelTrans = dalObject.GetParcelBy(p => p.DroneID == drone.ID).ID;
        }
        public int ID { get; set; }
        public string Model { get; set; }
        public DO.WeightCatagories Weight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatus DroneStatus { get; set; }
        public LocationBL Location { get; set; }
        public int NumOfParcelTrans { get; set; }

        public override string ToString()
        {
            return $"ID: {ID},\n Model: {Model},\n Weight: {Weight}, \nBatteryStatus: {BatteryStatus},\n DroneStatus: {DroneStatus},\n" +
                $"Location: {Location}, \nNumOfParcelTrans: {NumOfParcelTrans}";
        }
    }
}
