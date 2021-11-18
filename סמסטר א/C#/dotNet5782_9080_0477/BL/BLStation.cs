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
        public static void checkUniqeIdStation(int id, IDAL.IDal dalObject)
        {
            List<Station> stations = dalObject.GetStationByList();
            stations.ForEach(s =>
            {
                if (s.ID == id)
                    throw new NotUniqeID(id, typeof(Station));
            });
        }

        public void addStation(int id, int name, LocationBL location, int ChargeSlots)
        {
            checkUniqeIdStation(id, dalObject);
            StationBL station = new StationBL();
            station.ID = id;
            station.Name = name;
            station.location = new LocationBL(location.Longitude, location.Latitude);
            station.ChargeSlots = ChargeSlots;
            AddStationToDal(id, name, location, ChargeSlots);
        }

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

        public StationBL GetSpecificStationBL(int id)
        {
            try
            {
                return convertDalStationToBl(dalObject.GetSpecificStation(id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Station));

            }
        }

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

        private StationBL convertDalStationToBl(Station s)
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
                location = new LocationBL() { Longitude = s.Longitude, Latitude = s.Latitude },
                dronesInCharge = dronesInCharges
            };
        }

        public void updateDataStation(int id, int name = 0, int chargeSlots = -1)
        {
            Station station = dalObject.GetSpecificStation(id);
            if (name != null)
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
