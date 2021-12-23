using DAL;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    internal partial class BL
    {

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdStation(int id, IDAL.IDal dalObject)
        {
            IEnumerable<DO.Station> stations = dalObject.GetStation();
            if (stations.Any(s => s.ID == id))
                throw new NotUniqeID(id, typeof(DO.Station));
        }

        /// <summary>
        /// add a station to the bl
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="ChargeSlots"></param>
        public void addStation(int id, int name, LocationBL location, int ChargeSlots)
        {
            checkUniqeIdStation(id, dalObject);
            List<DroneCharge> droneChargers = dalObject.GetDroneCharge().ToList();
            droneChargers = droneChargers.FindAll(d => d.StationID == id);
            List<DroneInCharger> dronesInCharges = new List<DroneInCharger>();
            droneChargers.ForEach(d => dronesInCharges.Add(new DroneInCharger(getSpecificDroneBLFromList(d.DroneID))));
            BO.Station station = new BO.Station(id, name, ChargeSlots, new LocationBL(location.Longitude, location.Latitude), dronesInCharges);
            AddStationToDal(id, name, location, ChargeSlots);
        }

        /// <summary>
        /// add a station to the dal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="ChargeSlots"></param>
        public void AddStationToDal(int id, int name, LocationBL location, int ChargeSlots)
        {
            DO.Station station = new DO.Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = location.Longitude;
            station.Latitude = location.Latitude;
            station.ChargeSlots = ChargeSlots;
            dalObject.AddStation(station);
        }

        /// <summary>
        /// return all the stations from the dal converted to bl
        /// </summary>
        /// <returns>List<StationBL></returns>
        public List<BO.Station> GetStationsBL()
        {

            IEnumerable<DO.Station> stations = dalObject.GetStation();
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
        public BO.Station GetSpecificStationBL(int id)
        {
            try
            {
                return convertDalStationToBl(dalObject.getStationById(s => s.ID == id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DO.Station));

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
        public bool checkStationIfEmptyChargers(DO.Station station)
        {
            IEnumerable<DroneCharge> droneChargers = dalObject.GetDroneCharge();
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
        public BO.Station convertDalStationToBl(DO.Station s)
        {
            List<DroneCharge> droneChargers = dalObject.GetDroneCharge().ToList();
            droneChargers = droneChargers.FindAll(d => d.StationID == s.ID);
            List<DroneInCharger> dronesInCharges = new List<DroneInCharger>();
            droneChargers.ForEach(d => dronesInCharges.Add(new DroneInCharger(getSpecificDroneBLFromList(d.DroneID))));
            return new BO.Station(s.ID, s.Name, s.ChargeSlots, new LocationBL(s.Longitude, s.Latitude), dronesInCharges);
        }

        /// <summary>
        /// update the station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="chargeSlots"></param>
        public BO.Station updateDataStation(int id, int name = 0, int chargeSlots = -1)
        {
            DO.Station station = dalObject.getStationById(s => s.ID == id);
            if (name != 0)
            {
                station.Name = name;
            }
            if (chargeSlots != -1)
            {
                station.ChargeSlots = chargeSlots;
            }
            dalObject.updateStation(station);
            return convertDalStationToBl(station);
        }

        public List<StationToList> getStationsByChargeSlots(int status)
        {
            if (status == 0)
                return (
                from station in getStationToList()
                where station.ChargeSlotsFree == (int)status
                select station).ToList();
            else
                return (
            from station in getStationToList()
            where station.ChargeSlotsFree != 0
            select station).ToList();
        }


        public List<StationToList> getStationToList()
        {
            List<BO.Station> stations = GetStationsBL();
            List<StationToList> stations1 = new List<StationToList>();
            foreach (var station in stations)
            {
                stations1.Add(new StationToList(station));
            }
            return stations1;
        }

        public BO.Station convertStationToListToStationBL(StationToList stationToList)
        {
            return GetSpecificStationBL(stationToList.ID);
        }

        public void removeStation(int id)
        {
            /*dalObject.RemoveStation(id);

            BO.Station station = GetSpecificStationBL(getDroneToListByCondition(p => p. == id).ID);
            if (drone != null)
            {
                drone.parcelInDelivery = null;
                drone.DroneStatus = DroneStatus.Available;
                updateDrone(drone);
            }*/
        }
    }
}
