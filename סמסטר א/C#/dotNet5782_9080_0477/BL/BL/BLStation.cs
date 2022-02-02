using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using static BL.ExceptionsBL;
using DALException;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL
    {

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void checkUniqeIdStation(int id, IDAL.IDal dalObject)
        {
            lock (dalObject)
            {
                IEnumerable<DO.Station> stations = dalObject.GetStations();
                if (stations.Any(s => s.ID == id))
                    throw new NotUniqeID(id, typeof(DO.Station));
            }
        }

        /// <summary>
        /// add a station to the bl
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="ChargeSlots"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int id, int name, LocationBL location, int ChargeSlots)
        {
            lock (dalObject)
            {
                checkUniqeIdStation(id, dalObject);
                /*IEnumerable<DroneCharge> droneChargers = dalObject.GetDroneCharges();
                droneChargers = droneChargers.Where(d => d.StationID == id);
                List<DroneInCharger> dronesInCharges = new List<DroneInCharger>();
                droneChargers.ForEach(d => dronesInCharges.Add(new DroneInCharger(GetSpecificDroneBL(d.DroneID))));*/
                //BO.Station station = new BO.Station(id, name, ChargeSlots, new LocationBL(location.Longitude, location.Latitude), dronesInCharges);
                addStationToDal(id, name, location, ChargeSlots);
            }
        }

        /// <summary>
        /// add a station to the dal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="ChargeSlots"></param>
        private void addStationToDal(int id, int name, LocationBL location, int ChargeSlots)
        {
            DO.Station station = new DO.Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = location.Longitude;
            station.Latitude = location.Latitude;
            station.ChargeSlots = ChargeSlots;
            station.IsActive = true;
            dalObject.AddStation(station);
        }

        /// <summary>
        /// return all the stations from the dal converted to bl
        /// </summary>
        /// <returns>List<StationBL></returns>
        private IEnumerable<BO.Station> getStationsBL()
        {

            IEnumerable<DO.Station> stations = dalObject.GetStations();
            List<BO.Station> stations1 = new List<BO.Station>();
            foreach (var station in stations)
            {
                stations1.Add(convertDalStationToBl(station));
            }
            return stations1;
        }

        /// <summary>
        /// returns a specific station by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station GetSpecificStationBL(int id)
        {
            try
            {
                lock (dalObject)
                {
                    return convertDalStationToBl(dalObject.GetStationById(s => s.ID == id && s.IsActive == true));
                }
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DO.Station), e);

            }
        }

        /// <summary>
        /// returns the station with empty chargers
        /// </summary>
        /// <returns>List<StationBL></returns>
       /* public List<BO.Station> getStationWithEmptyChargers()
        {
            IEnumerable<DO.Station> stations = dalObject.GetStation();
            List<BO.Station> stations1 = new List<BO.Station>();
            foreach (var station in stations)
            {
                if (checkStationIfEmptyChargers(station))
                    stations1.Add(convertDalStationToBl(station));
            }
            return stations1;
        }*/

        /// <summary>
        /// returns if the station has empty chargers
        /// </summary>
        /// <param name="station"></param>
        /// <returns>bool</returns>
        private bool checkStationIfEmptyChargers(DO.Station station)
        {
            IEnumerable<DroneCharge> droneChargers = dalObject.GetDroneCharges();
            int numOfChargers = 0;
            foreach (var droneCharge in droneChargers)
            {
                if (station.ID == droneCharge.StationID)
                    numOfChargers++;
            }
            if (numOfChargers < station.ChargeSlots)
                return true;
            return false;
        }

        /// <summary>
        /// convert a parcel from dal to bl
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private BO.Station convertDalStationToBl(DO.Station s)
        {
            List<DroneInCharger> dronesInCharges = new List<DroneInCharger>();
            foreach (var d in dalObject.GetDroneCharges())
                if (d.StationID == s.ID)
                    dronesInCharges.Add(new DroneInCharger(GetSpecificDroneBLWithDeleted(d.DroneID)));
            return new BO.Station(s.ID, s.Name, s.ChargeSlots, new LocationBL(s.Longitude, s.Latitude), s.IsActive, dronesInCharges);
        }

        /// <summary>
        /// update the station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="chargeSlots"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station UpdateStation(BO.Station stationBL)
        {
            lock (dalObject)
            {
                DO.Station station = dalObject.GetStationById(s => s.ID == stationBL.ID);
                /*if (name != 0)
                    station.Name = name;
                if (chargeSlots != -1)
                    station.ChargeSlots = chargeSlots;*/
                dalObject.UpdateStation(station);
                return convertDalStationToBl(station);
            }
        }

        /// <summary>
        /// get stations by charge slots
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStationsByChargeSlots(int status)
        {
            lock (dalObject)
            {
                if (status == 0)
                    return (
                    from station in GetStationsToList()
                    where station.ChargeSlotsFree == (int)status
                    select station);
                else
                    return (
                from station in GetStationsToList()
                where station.ChargeSlotsFree != 0
                select station);
            }
        }

        /// <summary>
        /// return list of stationToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStationsToList()
        {
            lock (dalObject)
            {
                IEnumerable<BO.Station> stations = getStationsBL();
                List<StationToList> stations1 = new List<StationToList>();
                foreach (var station in stations)
                {
                    stations1.Add(new StationToList(station));
                }
                return stations1;
            }
        }

        /// <summary>
        /// convert stationToList to stationBL
        /// </summary>
        /// <param name="stationToList"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station ConvertStationToListToStationBL(StationToList stationToList)
        {
            return GetSpecificStationBL(stationToList.ID);
        }

        /// <summary>
        /// remove a station from dataSource list
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveStation(int id)
        {
            lock (dalObject)
            {
                List<DroneInCharger> droneInCharger = GetSpecificStationBL(id).DronesInCharge;
                /*if (droneInCharger.Count > 0)
                    throw new CantRemoveItem(typeof(BO.Station));*/
                dalObject.RemoveStation(id);
            }
        }

        /// <summary>
        /// get deleted stations
        /// </summary>
        /// <returns></returns>
        private IEnumerable<BO.Station> getDeletedStationsBL()
        {

            IEnumerable<DO.Station> stations = dalObject.GetDeletedStations();
            List<BO.Station> stations1 = new List<BO.Station>();
            foreach (var station in stations)
            {
                stations1.Add(convertDalStationToBl(station));
            }
            return stations1;
        }

        /// <summary>
        /// get deleted stationToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetDeletedStationsToList()
        {
            lock (dalObject)
            {
                IEnumerable<BO.Station> stations = getDeletedStationsBL();
                List<StationToList> stations1 = new List<StationToList>();
                foreach (var station in stations)
                {
                    stations1.Add(new StationToList(station));
                }
                return stations1;
            }
        }
    }
}
