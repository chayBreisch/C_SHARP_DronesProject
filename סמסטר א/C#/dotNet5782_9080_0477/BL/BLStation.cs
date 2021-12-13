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
    internal partial class BL
    {

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdStation(int id, IDAL.IDal dalObject)
        {
            List<Station> stations = dalObject.GetStationByList();
            if (stations.Any(s => s.ID == id))
                throw new NotUniqeID(id, typeof(Station));
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
            StationBL station = new StationBL();
            station.ID = id;
            station.Name = name;
            station.Location = new LocationBL(location.Longitude, location.Latitude);
            station.ChargeSlots = ChargeSlots;
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
            Station station = new Station();
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
        public List<StationBL> GetStationsBL()
        {

            IEnumerable<Station> stations = dalObject.GetStation();
            List<StationBL> stations1 = new List<StationBL>();
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
        public StationBL GetSpecificStationBL(int id)
        {
            try
            {
                return convertDalStationToBl(dalObject.getStationById(s => s.ID == id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Station));

            }
        }

        /// <summary>
        /// returns the station with empty chargers
        /// </summary>
        /// <returns>List<StationBL></returns>
        public List<StationBL> getStationWithEmptyChargers()
        {
            IEnumerable<Station> stations = dalObject.GetStation();
            List<StationBL> stations1 = new List<StationBL>();
            foreach (var station in stations)
            {
                if (checkStationIfEmptyChargers(station))
                    stations1.Add(convertDalStationToBl(station));
            }
            return stations1;
        }

        /// <summary>
        /// returns if the station has empty chargers
        /// </summary>
        /// <param name="station"></param>
        /// <returns>bool</returns>
        public bool checkStationIfEmptyChargers(Station station)
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
        public StationBL convertDalStationToBl(Station s)
        {
            List<DroneCharge> droneChargers = dalObject.GetDroneCharge().Cast<DroneCharge>().ToList();
            droneChargers = droneChargers.FindAll(d => d.StationID == s.ID);
            List<DroneInCharger> dronesInCharges = new List<DroneInCharger>();
            droneChargers.ForEach(d => dronesInCharges.Add(new DroneInCharger
            { ID = d.DroneID, BatteryStatus = getSpecificDroneBLFromList(d.DroneID).BatteryStatus }));
            return new StationBL
            {
                ID = s.ID,
                Name = s.Name,
                ChargeSlots = s.ChargeSlots,
                Location = new LocationBL() { Longitude = s.Longitude, Latitude = s.Latitude },
                DronesInCharge = dronesInCharges
            };
        }

        /// <summary>
        /// update the station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="chargeSlots"></param>
        public void updateDataStation(int id, int name = 0, int chargeSlots = -1)
        {
            Station station = dalObject.getStationById(s => s.ID == id);
            if (name != 0)
            {
                station.Name = name;
            }
            if (chargeSlots != -1)
            {
                station.ChargeSlots = chargeSlots;
            }
            dalObject.updateStation(station);
        }
    }
}
