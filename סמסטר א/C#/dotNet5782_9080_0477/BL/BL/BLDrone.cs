﻿using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;
using DALException;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL
    {
        #region add drone functions

        /// <summary>
        /// add a drone to the bl
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="stationID"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, int maxWeight, int stationID)
        {
            lock (dalObject)
            {
                checkUniqeIdDrone(id, dalObject);
                if (maxWeight < 1 || maxWeight > 3)
                {
                    throw new OutOfRange("weight");
                }
                BO.Drone droneBL = new BO.Drone();
                DO.Station station = dalObject.GetStationById(s => s.ID == stationID && s.IsActive == true);
                if (station.ID != 0)
                {
                    droneBL.Model = model;
                    droneBL.ID = id;
                    droneBL.Weight = (DO.WeightCatagories)maxWeight;
                    droneBL.BatteryStatus = rand.Next(20, 40);
                    droneBL.DroneStatus = DroneStatus.Maintenance;
                    droneBL.Location = new Location(station.Longitude, station.Latitude);
                    droneBL.IsActive = true;
                    addDroneToDal(id, model, maxWeight);
                    addDroneCharge(stationID, id);
                    droneBLList.Add(droneBL);
                }
                else
                    throw new NoItemWithThisID(stationID, typeof(DO.Station));
            }
        }

        /// <summary>
        /// add a drone to the dal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        private void addDroneToDal(int id, string model, int maxWeight)
        {

            DO.Drone drone = new DO.Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = (WeightCatagories)maxWeight;
            drone.IsActive = true;
            dalObject.AddDrone(drone);
        }
        #endregion

        #region get drone/s functions

        /// <summary>
        /// returns a specific drone by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DroneBL</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone GetSpecificDroneBL(int id)
        {
            try
            {
                lock (dalObject)
                {
                    return droneBLList.Find(drone => drone.ID == id && drone.IsActive == true);
                }
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DO.Drone), e);
            }
        }

        /// <summary>
        /// get specific drone bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone GetSpecificDroneBLWithDeleted(int id)
        {
            try
            {
                lock (dalObject)
                {
                    return droneBLList.Find(drone => drone.ID == id);
                }
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DO.Drone), e);
            }
        }

        /// <summary>
        /// return all droneToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDronesToList()
        {
            lock (dalObject)
            {
                List<BO.Drone> drones = droneBLList;
                List<DroneToList> drone1 = new List<DroneToList>();
                foreach (var drone in drones)
                {
                    drone1.Add(new DroneToList(drone, dalObject));
                }
                return drone1;
            }
        }

        /// <summary>
        /// get DroneToList By Condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDronesToListByCondition(Predicate<DroneToList> predicate)
        {
            //try todo
            lock (dalObject)
            {
                return (from drone in GetDronesToList()
                        where predicate(drone)
                        select drone);
            }
        }

        /// <summary>
        /// get deleted droneToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDeletedDronesToList()
        {
            //try todo
            lock (dalObject)
            {
                return (from drone in GetDronesToList()
                        where GetSpecificDroneBL(drone.ID) == null
                        select drone);
            }
        }
        #endregion

        #region update drone functions

        /// <summary>
        /// update the drone
        /// </summary>
        /// <param name="drone"></param>
        private void updateDrone(BO.Drone drone)
        {
            lock (dalObject)
            {
                int index = droneBLList.FindIndex(d => d.ID == drone.ID);
                droneBLList[index] = drone;
                dalObject.UpdateDrone(convertDroneBlToDal(drone));
            }
        }

        /// <summary>
        /// update the drone model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone UpdateDataDroneModel(int id, string model)
        {
            lock (dalObject)
            {
                BO.Drone droneBl = GetSpecificDroneBL(id);
                droneBl.Model = model;
                updateDrone(droneBl);

                DO.Drone drone = dalObject.GetDroneById(d => d.ID == id && d.IsActive == true);
                drone.Model = model;
                dalObject.UpdateDrone(drone);
                return droneBl;
            }
        }

        /// <summary>
        /// update drone
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone UpdateDataDrone(BO.Drone drone)
        {
            lock (dalObject)
            {
                return new BO.Drone
                {
                    ID = drone.ID,
                    Model = drone.Model,
                    Location = drone.Location,
                    Weight = drone.Weight,
                    DroneStatus = drone.DroneStatus,
                    BatteryStatus = drone.BatteryStatus,
                    IsActive = drone.IsActive,
                    parcelInDelivery = drone.parcelInDelivery
                };
            }
        }
        #endregion

        #region convert drone functions

        /// <summary>
        /// convert a drone from dal to bl
        /// </summary>
        /// <param name="d"></param>
        /// <returns>DroneBL</returns>
        private BO.Drone convertDalDroneToBl(DO.Drone d)
        {
            //לבדוק מה עם parcellattransfor
            return GetSpecificDroneBLWithDeleted(d.ID);
        }

        /// <summary>
        /// convert droneBl to dal
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        private DO.Drone convertDroneBlToDal(BO.Drone drone)
        {
            return new DO.Drone { ID = drone.ID, IsActive = drone.IsActive, MaxWeight = drone.Weight, Model = drone.Model };
        }

        /// <summary>
        /// convert droneToList to droneBL
        /// </summary>
        /// <param name="droneToList"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone ConvertDroneToListToDroneBL(DroneToList droneToList)
        {
            return GetSpecificDroneBLWithDeleted(droneToList.ID);
        }

        /// <summary>
        /// convert droneBL to droneToList
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        public BO.DroneToList ConvertDroneBLToDroneToList(BO.Drone drone)
        {
            return new DroneToList(drone, dalObject);
        }
        #endregion

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void checkUniqeIdDrone(int id, IDAL.IDal dalObject)
        {
            lock (dalObject)
            {
                IEnumerable<DO.Drone> drones = dalObject.GetDrones();
                if (drones.Any(d => d.ID == id))
                    throw new NotUniqeID(id, typeof(DO.Drone));
            }
        }

        /// <summary>
        /// remove drone
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDrone(int id)
        {
            lock (dalObject)
            {
                dalObject.RemoveDrone(id);
            }
            droneBLList[id - 1].IsActive = false;
        }
    }
}
