using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalFacade;
using DALException;

namespace DalObject
{

    internal partial class DalObject
    {
        /// <summary>
        /// returns the stations from dal
        /// </summary>
        /// <returns>DataSource.stations</returns>
        public IEnumerable<Station> GetStations()
        {
            return from station in DataSource.stations
                   where station.IsActive == true
                   select station;
         /*   foreach (var station in DataSource.stations)
            {
                yield return station;
            }*/
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
        public Station GetStationById(Predicate<Station> predicate)
        {
            //try todo
            Station station1 = new Station();
            try
            {
                station1 = (from station in DataSource.stations
                            where predicate(station)
                            select station).First();
            }
            catch (Exception e)
            {
                throw new NotExistObjWithID(typeof(Station), e);
            }
            return station1;
        }

        /// <summary>
        /// get the stations with a specific condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /*public IEnumerable<Station> getStationsByCondition(Predicate<Station> predicate)
        {
            //try todo
            return (from station in DataSource.stations
                    where predicate(station)
                    select station);
        }*/


        /// <summary>
        /// add station to dal
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            checkUniqestation(station.ID);
            DataSource.stations.Add(station);
        }

        /// <summary>
        /// update station in dal
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(Station station)
        {
            int index = getIndexOfStation(station.ID);
            DataSource.stations[index] = station;
        }

        /// <summary>
        /// check uniqe station in dal by id
        /// </summary>
        /// <param name="id"></param>
        private void checkUniqestation(int id)
        {
            if (DataSource.stations.Any(station => station.ID == id))
                throw new NotUniqeID(id, typeof(Station));
        }

        /// <summary>
        /// return length of station in dal
        /// </summary>
        /// <returns>int</returns>
        public int LengthStation()
        {
            return DataSource.stations.Count;
        }

        /// <summary>
        /// remove station fron dataSource
        /// </summary>
        /// <param name="idRemove"></param>
        public void RemoveStation(int idRemove)
        {
            Station station = DataSource.stations[getIndexOfStation(idRemove)];
            station.IsActive = false;
            UpdateStation(station);
        }

        /// <summary>
        /// return index of parcel in dataSource list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int getIndexOfStation(int id)
        {
            try
            {
                return DataSource.stations.FindIndex(s => s.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Station), e);
            }
        }
        public IEnumerable<Station> GetDeletedStations()
        {
            return from station in DataSource.stations
                   where station.IsActive == false
                   select station;
         /*   foreach (var station in DataSource.stations)
            {
                yield return station;
            }*/
        }
    }
}
