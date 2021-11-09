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

        public void AddDroneCharge(DroneCharge droneCharge)
        {
            DataSource.droneChargers.Add(droneCharge);
        }
    }
}
