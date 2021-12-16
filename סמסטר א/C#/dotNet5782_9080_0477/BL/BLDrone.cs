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
    internal partial class BL
    {
        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdDrone(int id, IDAL.IDal dalObject)
        {
            List<IDAL.DO.Drone> drones = dalObject.GetDrone().ToList();
            if (drones.Any(d => d.ID == id))
                throw new NotUniqeID(id, typeof(IDAL.DO.Drone));
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
            IBL.BO.Drone droneBL = new IBL.BO.Drone();
            IDAL.DO.Station station = dalObject.getStationById(s => s.ID == stationID);
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
                throw new NoItemWithThisID(stationID, typeof(IDAL.DO.Station));
        }

        /// <summary>
        /// add a drone to the dal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        public void AddDroneToDal(int id, string model, int maxWeight)
        {

            IDAL.DO.Drone drone = new IDAL.DO.Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = (WeightCatagories)maxWeight;
            dalObject.AddDrone(drone);
        }

        /// <summary>
        /// return all the drones from the dal converted to bl
        /// </summary>
        /// <returns>List<DroneBL>returns>
        public List<IBL.BO.Drone> GetDronesBL()
        {

            IEnumerable<IDAL.DO.Drone> drones = dalObject.GetDrone();
            List<IBL.BO.Drone> drone1 = new List<IBL.BO.Drone>();
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
        public IBL.BO.Drone getSpecificDroneBLFromList(int id)
        {
            try
            {
                return droneBLList.Find(drone => drone.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(IDAL.DO.Drone));
            }
        }

        /// <summary>
        /// returns a specific drone by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DroneBL</returns>
        public IBL.BO.Drone GetSpecificDroneBL(int id)
        {
            return getSpecificDroneBLFromList(id);
        }

        /// <summary>
        /// convert a drone from dal to bl
        /// </summary>
        /// <param name="d"></param>
        /// <returns>DroneBL</returns>
        public IBL.BO.Drone convertDalDroneToBl(IDAL.DO.Drone d)
        {
            //לבדוק מה עם parcellattransfor
            return GetSpecificDroneBL(d.ID);
        }

        /// <summary>
        /// update the drone
        /// </summary>
        /// <param name="drone"></param>
        public void updateDrone(IBL.BO.Drone drone)
        {
            int index = droneBLList.FindIndex(d => d.ID == drone.ID);
            droneBLList[index] = drone;
        }

        /// <summary>
        /// update the drone model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public IBL.BO.Drone updateDataDroneModel(int id, string model)
        {
            IBL.BO.Drone droneBl = getSpecificDroneBLFromList(id);
            droneBl.Model = model;
            updateDrone(droneBl);

            IDAL.DO.Drone drone = dalObject.getDroneById(d => d.ID == id);
            drone.Model = model;
            dalObject.updateDrone(drone);
            return droneBl;
        }

        /// <summary>
        /// get drones with this droneStatus
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<IBL.BO.Drone> getDronesByDroneStatus(int status)
        {
            IEnumerable<IBL.BO.Drone> droneQuery =
            from drone in GetDronesBL()
            where drone.DroneStatus == (DroneStatus)status
            select drone;
            return droneQuery.ToList();
        }

        /// <summary>
        /// get drones with this weight
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<IBL.BO.Drone> getDronesByDroneWeight(int status)
        {
            IEnumerable<IBL.BO.Drone> droneQuery =
            from drone in GetDronesBL()
            where drone.Weight == (WeightCatagories)status
            select drone;
            return droneQuery.ToList();
        }

        public List <DroneToList> getDroneToList()
        {
            IEnumerable<IDAL.DO.Drone> drones = dalObject.GetDrone();
            List<DroneToList> drone1 = new List<DroneToList>();
            foreach (var drone in drones)
            {
                drone1.Add(new DroneToList(convertDalDroneToBl(drone), dalObject));
            }
            return drone1;
        }

        public IBL.BO.Drone convertDroneToListToDroneBL(DroneToList droneToList)
        {
            IBL.BO.Drone drone = new IBL.BO.Drone();
            drone.ID = droneToList.ID;
            drone.Model = droneToList.Model;
            drone.Location = droneToList.Location;
            drone.Weight = droneToList.Weight;
            drone.BatteryStatus = droneToList.BatteryStatus;
            drone.DroneStatus = droneToList.DroneStatus;
            drone.parcelInDelivery = new ParcelInDelivery(convertDalToParcelBL(dalObject.getParcelById(p => p.ID == droneToList.NumOfParcelTrans)),dalObject);
            return drone;
        }
    }
}
