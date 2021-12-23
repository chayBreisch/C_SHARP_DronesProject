using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DO;
using BO;
using static BL.ExceptionsBL;

namespace BL
{
    internal partial class BL
    {

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdDroneCharge(int droneId, int stationId, IDAL.IDal dalObject)
        {
            IEnumerable<DroneCharge> droneCharges = dalObject.GetDroneCharges();
            if (droneCharges.Any(d => d.DroneID == droneId && d.StationID == stationId))
                throw new NotUniqeID(droneId, stationId, typeof(DroneCharge));
        }

        /// <summary>
        /// add a drone charge to the bl
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="droneID"></param>
        public void addDroneCharge(int stationID, int droneID)
        {
            DO.Station station = dalObject.getStationById(s => s.ID == stationID);
            if (!checkStationIfEmptyChargers(station))
                throw new NotEmptyChargeSlots(stationID);
            checkUniqeIdDroneCharge(droneID, stationID, dalObject);
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneID = droneID;
            droneCharge.StationID = stationID;
            dalObject.AddDroneCharge(droneCharge);
        }
    }

}
