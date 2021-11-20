using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;
using DAL;
namespace DalObject
{

    public partial class DalObject
    {
        public List<DroneCharge> GetDroneChargeByList()
        {
            return DataSource.droneChargers;
        }

        public IEnumerable<DroneCharge> GetDroneCharge()
        {
            foreach (var droneCharge in DataSource.droneChargers)
            {
                yield return droneCharge;
            }
        }

        public DroneCharge getSpecificDroneChargeByStationID(int id)
        {
            try
            {
                return DataSource.droneChargers.Find(droneCharge => droneCharge.StationID == id);
            }

            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }

        }

        public DroneCharge getSpecificDroneChargeByDroneID(int id)
        {
            try
            {
                return DataSource.droneChargers.Find(droneCharge => droneCharge.DroneID == id);
                }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }

        public void removeDroneCharge(DroneCharge droneCharge)
        {
            DataSource.droneChargers.RemoveAt(getIndexOfDroneChargeByStationID(droneCharge.StationID));
        }

        public int getIndexOfDroneChargeByStationID(int id)
        {
            try
            {
                return DataSource.droneChargers.FindIndex(p => p.StationID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }

        public int getIndexOfDroneChargeByDroneID(int id)
        {
            try
            {
                return DataSource.droneChargers.FindIndex(p => p.DroneID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }

        public void AddDroneCharge(DroneCharge droneCharge)
        {
            checkUniqeIdDroneChargeBL(droneCharge.DroneID, droneCharge.StationID);
            DataSource.droneChargers.Add(droneCharge);
        }

        public void checkUniqeIdDroneChargeBL(int droneId, int stationId)
        {

            foreach (var dronecharge in DataSource.droneChargers)
            {
                if (dronecharge.DroneID == droneId && dronecharge.StationID == stationId)
                    throw new NotUniqeID(droneId, stationId, typeof(Station));
            }
        }

        public void updateDroneCharge(DroneCharge droneCharge)
        {
            int index = DataSource.droneChargers.FindIndex(d => d.StationID == droneCharge.StationID);
            DataSource.droneChargers[index] = droneCharge;
        }
    }
}
