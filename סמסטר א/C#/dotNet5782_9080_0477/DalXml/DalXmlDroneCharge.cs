using DAL;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalXml
{
    public partial class DalXml
    {

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            IEnumerable<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dir + droneChargeFilePath);
            return from droneCharge in droneChargeList
                   orderby droneCharge.StationID
                   select droneCharge;
        }
        public void RemoveDroneCharge(DroneCharge droneCharge)
        {
            IEnumerable<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dir + droneChargeFilePath);
            if (!droneChargeList.Any(d => d.DroneID == droneCharge.DroneID))
            {
                throw new NotExistObjWithID(droneCharge.DroneID, typeof(Drone));
            }
            droneChargeList.ToList().Remove(droneCharge);
            XMLTools.SaveListToXMLSerializer<DroneCharge>(droneChargeList, dir + droneChargeFilePath);
        }
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            IEnumerable<DroneCharge> droneList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dir + droneChargeFilePath);
            checkUniqeIdDroneChargeBL(droneCharge.DroneID);
            droneList.ToList().Add(droneCharge);
            XMLTools.SaveListToXMLSerializer<DroneCharge>(droneList, dir + droneChargeFilePath);
        }

        private void checkUniqeIdDroneChargeBL(int id)
        {
            if (GetDroneCharges().Any(drone => drone.DroneID == id))
                throw new NotUniqeID(id, typeof(DroneCharge));
        }

        public void updateDroneCharge(DroneCharge DroneCharge)
        {
            List<DroneCharge> DroneChargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dir + droneChargeFilePath).ToList();

            int index = DroneChargeList.FindIndex(d => d.DroneID == DroneCharge.DroneID);

            if (index == -1)
                throw new NotExistObjWithID(DroneCharge.DroneID, typeof(DroneCharge));
            DroneChargeList[index] = DroneCharge;
            XMLTools.SaveListToXMLSerializer<DroneCharge>(DroneChargeList, dir + droneChargeFilePath);
        }

        public DroneCharge GetDroneChargeById(Predicate<DroneCharge> predicate)
        {
            DroneCharge droneCharge1 = new DroneCharge();
            try
            {
                IEnumerable<DroneCharge> DroneChargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dir + droneChargeFilePath);
                droneCharge1 = (from DroneCharge in DroneChargeList
                        where predicate(DroneCharge)
                        select DroneCharge).First();
            }
            catch (NotExistObjWithID e) { }
            return droneCharge1;
        }
    }
}
