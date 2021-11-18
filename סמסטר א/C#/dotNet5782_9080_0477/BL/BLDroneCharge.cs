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

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdDroneCharge(int droneId, int stationId, IDAL.IDal dalObject)
        {
            List<DroneCharge> droneCharges = dalObject.GetDroneCharge().ToList();
            droneCharges.ForEach(d =>
            {
                if (d.DroneID == droneId && d.StationID == stationId)
                    throw new NotUniqeID(droneId, stationId, typeof(Station));
            });
        }

        /// <summary>
        /// add a drone charge to the bl
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="droneID"></param>
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
