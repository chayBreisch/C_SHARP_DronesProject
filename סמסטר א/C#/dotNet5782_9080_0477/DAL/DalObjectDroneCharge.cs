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
            return DataSource.droneChargers.First(droneCharge => droneCharge.StationID == id);

        }
        public DroneCharge getSpecificDroneChargeByDroneID(int id)
        {
            return DataSource.droneChargers.First(droneCharge => droneCharge.DroneID == id);
        }
        public void removeDroneCharge(DroneCharge droneCharge)
        {
            DataSource.droneChargers.RemoveAt(getIndexOfDroneChargeByStationID(droneCharge.StationID));
        }
        public int getIndexOfDroneChargeByStationID(int id)
        {
            return DataSource.droneChargers.FindIndex(p => p.StationID == id);
        }
        public int getIndexOfDroneChargeByDroneID(int id)
        {
            return DataSource.droneChargers.FindIndex(p => p.DroneID == id);
        }
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            DataSource.droneChargers.Add(droneCharge);
        }
        public void updateDroneCharge(DroneCharge droneCharge)
        {
            int index = DataSource.droneChargers.FindIndex(d => d.StationID == droneCharge.StationID);
            DataSource.droneChargers[index] = droneCharge;
        }
    }
}
