using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IDAL.DO;
using IBL.BO;
namespace BL
{
    public partial class BL
    {
        public static void checkUniqeIdDroneCharge(int droneId, int stationId, IDAL.IDal dalObject)
        {
            List<DroneCharge> droneCharges = dalObject.GetDroneCharge().ToList();
            droneCharges.ForEach(d =>
            {
                if (d.DroneID == droneId && d.StationID == stationId)
                    throw new NotUniqeID(droneId, stationId, typeof(Station));
            });
        }
        public void addDroneCharge(int stationID, int droneID)
        {
            checkUniqeIdDroneCharge(droneID, stationID, dalObject);
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneID = droneID;
            droneCharge.StationID = stationID;
            dalObject.AddDroneCharge(droneCharge);
        }
    }

}
