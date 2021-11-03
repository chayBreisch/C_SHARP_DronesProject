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
        public class DroneCharge
        {
            IDAL.DO.DroneCharge droneCharge;

            public DroneCharge()
            {
                droneCharge = new IDAL.DO.DroneCharge();
            }
            public override string ToString()
            {
                return $"customer: {droneCharge.StationID} : {droneCharge.DroneID}";
            }











            /*public DroneCharge()
            {
                DroneID = 0;
                StationID = 0;
            }
            public DroneCharge(int droneId, int stationId)
            {
                DroneID = droneId;
                StationID = stationId;
            }
            public int DroneID { get; set; }
            public int StationID { get; set; }
            public override string ToString()
            {
                return $"{DroneID}";
            }*/
        }
    }
}
