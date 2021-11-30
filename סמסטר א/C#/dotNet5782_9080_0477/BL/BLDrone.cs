using DAL;
using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;

namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdDrone(int id, IDAL.IDal dalObject)
        {
            List<Drone> drones = dalObject.GetDrone().ToList();
            if (drones.Any(d => d.ID == id))
                throw new NotUniqeID(id, typeof(Drone));

        }

        /// <summary>
        /// add a drone to the bl
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="stationID"></param>
        public void addDrone(int id, string model, int maxWeight, int stationID)
        {
            checkUniqeIdDrone(id, dalObject);
            if (maxWeight < 1 || maxWeight > 3)
            {
                throw new OutOfRange("weight");
            }
            DroneBL droneBL = new DroneBL();
            Station station = dalObject.getStationById(s => s.ID == stationID);
            if (station.ID != 0)
            {
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
            else
                throw new NoItemWithThisID(stationID, typeof(Station));
        }

        /// <summary>
        /// add a drone to the dal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        public void AddDroneToDal(int id, string model, int maxWeight)
        {

            Drone drone = new Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = (WeightCatagories)maxWeight;
            dalObject.AddDrone(drone);
        }

        /// <summary>
        /// return all the drones from the dal converted to bl
        /// </summary>
        /// <returns>List<DroneBL>returns>
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

        /// <summary>
        /// returns a specific drone by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DroneBL</returns>
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

        /// <summary>
        /// returns a specific drone by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DroneBL</returns>
        public DroneBL GetSpecificDroneBL(int id)
        {
            return getSpecificDroneBLFromList(id);
        }

        /// <summary>
        /// convert a drone from dal to bl
        /// </summary>
        /// <param name="d"></param>
        /// <returns>DroneBL</returns>
        public DroneBL convertDalDroneToBl(Drone d)
        {
            //לבדוק מה עם parcellattransfor
            return GetSpecificDroneBL(d.ID);
        }

        /// <summary>
        /// update the drone
        /// </summary>
        /// <param name="drone"></param>
        public void updateDrone(DroneBL drone)
        {
            int index = droneBLList.FindIndex(d => d.ID == drone.ID);
            droneBLList[index] = drone;
        }

        /// <summary>
        /// update the drone model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void updateDataDroneModel(int id, string model)
        {
            DroneBL droneBl = getSpecificDroneBLFromList(id);
            droneBl.Model = model;
            updateDrone(droneBl);

            Drone drone = dalObject.getDroneById(d => d.ID == id);
            drone.Model = model;
            dalObject.updateDrone(drone);
        }

        public List<DroneBL> getDronesByDroneStatus(int status)
        {
            IEnumerable<DroneBL> droneQuery =
            from drone in GetDronesBL()
            where drone.DroneStatus == (DroneStatus)status
            select drone;
            return droneQuery.ToList();
        }
        public List<DroneBL> getDronesByDroneWeight(int status)
        {
            IEnumerable<DroneBL> droneQuery =
            from drone in GetDronesBL()
            where drone.Weight == (WeightCatagories)status
            select drone;
            return droneQuery.ToList();
        }
    }
}
