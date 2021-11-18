using DAL;
using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL
    {
        public static void checkUniqeIdDrone(int id, IDAL.IDal dalObject)
        {
            List<Drone> drones = dalObject.GetDrone().ToList();
            drones.ForEach(d =>
            {
                if (d.ID == id)
                    throw new NotUniqeID(id, typeof(Drone));
            });
        }

        public void addDrone(int id, string model, int maxWeight, int stationID)
        {
            checkUniqeIdDrone(id, dalObject);
            DroneBL droneBL = new DroneBL();
            Station station = dalObject.GetSpecificStation(id);

            droneBL.Model = model;
            droneBL.ID = id;
            droneBL.Weight = (WeightCatagories)maxWeight;
            droneBL.BatteryStatus = rand.Next(20, 40);
            droneBL.DroneStatus = DroneStatus.Maintenance;
            droneBL.Location = new LocationBL(station.Longitude, station.Latitude);


            AddDroneToDal(id, model, maxWeight);
            addDroneCharge(stationID, id);
            droneBLList.Add(droneBL);
        }

        public void AddDroneToDal(int id, string model, int maxWeight)
        {

            Drone drone = new Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = (WeightCatagories)maxWeight;
            dalObject.AddDrone(drone);
        }

        public List<DroneBL> GetDronesBL()
        {

            IEnumerable<Drone> drones = dalObject.GetDrone();
            List<DroneBL> drone1 = new List<DroneBL>();
            foreach (var drone in drones)
            {
                drone1.Add(convertDalDroneToBl(drone));
            }
            return drone1;
        }

        public DroneBL getSpecificDroneBLFromList(int id)
        {
            try
            {
                return droneBLList.Find(drone => drone.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Drone));
            }
        }

        public DroneBL GetSpecificDroneBL(int id)
        {
            return getSpecificDroneBLFromList(id);
        }

        private DroneBL convertDalDroneToBl(Drone d)
        {
            return GetSpecificDroneBL(d.ID);
        }

        public void updateDrone(DroneBL drone)
        {
            int index = droneBLList.FindIndex(d => d.ID == drone.ID);
            droneBLList[index] = drone;
        }

        public void updateDataDroneModel(int id, string model)
        {
            DroneBL droneBl = getSpecificDroneBLFromList(id);
            droneBl.Model = model;
            updateDrone(droneBl);

            Drone drone = dalObject.GetSpecificDrone(id);
            drone.Model = model;
            dalObject.updateDrone(drone);
        }

    }
}
