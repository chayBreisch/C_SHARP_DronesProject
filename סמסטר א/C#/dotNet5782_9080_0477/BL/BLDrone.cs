using DAL;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;

namespace BL
{
    internal partial class BL
    {
        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdDrone(int id, IDAL.IDal dalObject)
        {
            List<DO.Drone> drones = dalObject.GetDrones().ToList();
            if (drones.Any(d => d.ID == id))
                throw new NotUniqeID(id, typeof(DO.Drone));
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
            BO.Drone droneBL = new BO.Drone();
            DO.Station station = dalObject.getStationById(s => s.ID == stationID);
            if (station.ID != 0)
            {
                droneBL.Model = model;
                droneBL.ID = id;
                droneBL.Weight = (DO.WeightCatagories)maxWeight;
                droneBL.BatteryStatus = rand.Next(20, 40);
                droneBL.DroneStatus = DroneStatus.Maintenance;
                droneBL.Location = new LocationBL(station.Longitude, station.Latitude);


                AddDroneToDal(id, model, maxWeight);
                addDroneCharge(stationID, id);
                droneBLList.Add(droneBL);
            }
            else
                throw new NoItemWithThisID(stationID, typeof(DO.Station));
        }

        /// <summary>
        /// add a drone to the dal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        public void AddDroneToDal(int id, string model, int maxWeight)
        {

            DO.Drone drone = new DO.Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = (WeightCatagories)maxWeight;
            dalObject.AddDrone(drone);
        }

        /// <summary>
        /// return all the drones from the dal converted to bl
        /// </summary>
        /// <returns>List<DroneBL>returns>
        public List<BO.Drone> GetDronesBL()
        {

            IEnumerable<DO.Drone> drones = dalObject.GetDrones();
            List<BO.Drone> drone1 = new List<BO.Drone>();
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
        public BO.Drone getSpecificDroneBLFromList(int id)
        {
            try
            {
                return droneBLList.Find(drone => drone.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DO.Drone));
            }
        }

        /// <summary>
        /// returns a specific drone by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DroneBL</returns>
        public BO.Drone GetSpecificDroneBL(int id)
        {
            return getSpecificDroneBLFromList(id);
        }

        /// <summary>
        /// convert a drone from dal to bl
        /// </summary>
        /// <param name="d"></param>
        /// <returns>DroneBL</returns>
        public BO.Drone convertDalDroneToBl(DO.Drone d)
        {
            //לבדוק מה עם parcellattransfor
            return GetSpecificDroneBL(d.ID);
        }

        /// <summary>
        /// update the drone
        /// </summary>
        /// <param name="drone"></param>
        public void updateDrone(BO.Drone drone)
        {
            int index = droneBLList.FindIndex(d => d.ID == drone.ID);
            droneBLList[index] = drone;
        }

        /// <summary>
        /// update the drone model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public BO.Drone updateDataDroneModel(int id, string model)
        {
            BO.Drone droneBl = getSpecificDroneBLFromList(id);
            droneBl.Model = model;
            updateDrone(droneBl);

            DO.Drone drone = dalObject.getDroneById(d => d.ID == id);
            drone.Model = model;
            dalObject.updateDrone(drone);
            return droneBl;
        }

        /// <summary>
        /// get drones with this droneStatus
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
/*        public List<DroneToList> getDronesByDroneStatus(int status)
        {
            List<DroneToList> droneToLists = new List<DroneToList>();
            IEnumerable<BO.Drone> droneQuery =
            from drone in GetDronesBL()
            where drone.DroneStatus == (DroneStatus)status
            select drone;
            foreach (var drone in droneQuery)
            {
                droneToLists.Add(new DroneToList(drone, dalObject));
            }
            return droneToLists;
        }

        /// <summary>
        /// get drones with this weight
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<DroneToList> getDronesByDroneWeight(int status)
        {
            List<DroneToList> droneToLists = new List<DroneToList>();
            IEnumerable<BO.Drone> droneQuery =
            from drone in GetDronesBL()
            where drone.Weight == (DO.WeightCatagories)status
            select drone;
            foreach (var drone in droneQuery)
            {
                droneToLists.Add(new DroneToList(drone, dalObject));
            }
            return droneToLists;
        }*/


        /// <summary>
        /// return all droneToList
        /// </summary>
        /// <returns></returns>
        public List <DroneToList> getDroneToList()
        {
            List<BO.Drone> drones = droneBLList;
            List<DroneToList> drone1 = new List<DroneToList>();
            foreach (var drone in drones)
            {
                drone1.Add(new DroneToList(drone, dalObject));
            }
            return drone1;
        }

        /// <summary>
        /// convert droneToList to droneBL
        /// </summary>
        /// <param name="droneToList"></param>
        /// <returns></returns>
        public BO.Drone convertDroneToListToDroneBL(DroneToList droneToList)
        {
            return getSpecificDroneBLFromList(droneToList.ID);
        }

        /// <summary>
        /// get DroneToList By Condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DroneToList> getDroneToListByCondition(Predicate<DroneToList> predicate)
        {
            //try todo
            return (from drone in getDroneToList()
                    where predicate(drone)
                    select drone);
        }
    }
}
