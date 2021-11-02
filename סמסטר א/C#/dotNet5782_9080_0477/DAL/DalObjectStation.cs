using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;
using DAL;
namespace DalObject
{

    public partial class DalObject
    {

        public List<Station> GetStationByList()
        {
            return DataSource.stations;
        }
        public IEnumerable<Station> GetStation()
        {
            List<Station> stations = new List<Station>();
            for (int i = 0; i < DataSource.stations.Count; i++)
            {
                stations.Add(DataSource.stations[i]);
            }
            return stations;
        }
        public Station GetSpecificStation(int id)
        {
            try
            {
                return DataSource.stations.First(station => station.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exeptions(id);
            }
        }

        public void AddStation(int id, int name, int longitude, int latitude, int chargeSlots)
        {
            Station newStation = new Station();
            newStation.ID = id;
            newStation.Name = name;
            newStation.Longitude = longitude;
            newStation.Latitude = latitude;
            newStation.ChargeSlots = chargeSlots;
            DataSource.stations[DataSource.stations.Count - 1] = newStation;
        }
    }
}
