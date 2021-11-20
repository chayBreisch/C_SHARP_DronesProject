using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IDAL.DO;
using IBL.BO;
using static BL.ExceptionsBL;

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
                    throw new NotUniqeID(droneId, stationId, typeof(DroneCharge));
            });
        }

        /// <summary>
        /// add a drone charge to the bl
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="droneID"></param>
        public void addDroneCharge(int stationID, int droneID)
        {
            ///try////////////////
            Station station=  dalObject.GetSpecificStation(stationID);
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
