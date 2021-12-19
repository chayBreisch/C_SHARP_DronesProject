using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using IDAL;
using DAL;
namespace DalObject
{

    internal partial class DalObject
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

        
        /*public Station GetSpecificStation(int id)
        {
            try
            {
                return DataSource.stations.Find(station => station.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }*/

        /// <summary>
        /// get spscific station by the id
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Station getStationById(Predicate<Station> predicate)
        {
            //try todo
            Station station1 = new Station();
            try
            {
                station1 = (from station in DataSource.stations
                            where predicate(station)
                            select station).First();
            }
            catch (NotExistObjWithID e) { }
            return station1;
        }

        /// <summary>
        /// get the stations with a specific condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Station> getStationByCondition(Predicate<Station> predicate)
        {
            //try todo
            return (from station in DataSource.stations
                    where predicate(station)
                    select station);
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
