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

        /// <summary>
        /// returns the stations by list from dal
        /// </summary>
        /// <returns>DataSource.stations</returns>
        public List<Station> GetStationByList()
        {
            return DataSource.stations;
        }

        /// <summary>
        /// returns the stations from dal
        /// </summary>
        /// <returns>DataSource.stations</returns>
        public IEnumerable<Station> GetStation()
        {
            foreach (var station in DataSource.stations)
            {
                yield return station;
            }
        }

        /// <summary>
        /// returns a specific station from dal
        /// </summary>
        /// <returns>station</returns>
        public Station GetSpecificStation(int id)
        {
            try
            {
                return DataSource.stations.Find(station => station.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }

        /// <summary>
        /// add station to dal
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            CheckUniqestation(station.ID);
            DataSource.stations.Add(station);
        }

        /// <summary>
        /// update station in dal
        /// </summary>
        /// <param name="station"></param>
        public void updateStation(Station station)
        {
            int index = DataSource.stations.FindIndex(d => d.ID == station.ID);
            DataSource.stations[index] = station;
        }

        /// <summary>
        /// check uniqe station in dal by id
        /// </summary>
        /// <param name="id"></param>
        public void CheckUniqestation(int id)
        {
            if (DataSource.stations.Any(station => station.ID == id))
                throw new NotUniqeID(id, typeof(Station));
        }

        /// <summary>
        /// return length of station in dal
        /// </summary>
        /// <returns>int</returns>
        public int lengthStation()
        {
            return DataSource.stations.Count;
        }
    }
}
