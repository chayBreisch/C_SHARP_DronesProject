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
            List<DroneCharge> droneChargers = new List<DroneCharge>();
            for (int i = 0; i < DataSource.droneChargers.Count; i++)
            {
                droneChargers.Add(DataSource.droneChargers[i]);
            }
            return droneChargers;
        }
    }
}
