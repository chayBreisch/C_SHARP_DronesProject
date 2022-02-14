﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using IDAL;
using System.Runtime.CompilerServices;
using DALException;

namespace Dal
{

    internal partial class DalObject : IDal
    {
        /// <summary>
        /// returns the droneChargers from dal
        /// </summary>
        /// <returns>DataSource.droneChargers</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            return from droneCharge in DataSource.droneChargers
            select droneCharge;
        }

        /// <summary>
        /// remove a drone charge from dal
        /// </summary>
        /// <param name="droneCharge"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDroneCharge(DroneCharge droneCharge)
        {
            DataSource.droneChargers.RemoveAt(getIndexOfDroneChargeByStationID(droneCharge.StationID));
        }

        /// <summary>
        /// get index from specific drone charge by station id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>int</returns>
        private int getIndexOfDroneChargeByStationID(int id)
        {
            try
            {
                return DataSource.droneChargers.FindIndex(p => p.StationID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DroneCharge), e);
            }
        }

        /// <summary>
        /// add drone charge to dal
        /// </summary>
        /// <param name="droneCharge"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            checkUniqeIdDroneChargeBL(droneCharge.DroneID, droneCharge.StationID);
            DataSource.droneChargers.Add(droneCharge);
        }

        /// <summary>
        /// check uniqe drone charge
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        private void checkUniqeIdDroneChargeBL(int droneId, int stationId)
        {
            if (DataSource.droneChargers.Any(dronecharge => dronecharge.DroneID == droneId && dronecharge.StationID == stationId))
                throw new NotUniqeID(droneId, stationId, typeof(Station));
        }

        /// <summary>
        /// get a droneCharge by the id
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneChargeById(Predicate<DroneCharge> predicate)
        {
            DroneCharge droneCharge1 = new DroneCharge();
            try
            {
                droneCharge1 = (from dronecharge in DataSource.droneChargers
                                where predicate(dronecharge)
                                select dronecharge).First();
            }
            catch (NotExistObjWithID e) { }
            return droneCharge1;

        }
    }
}
