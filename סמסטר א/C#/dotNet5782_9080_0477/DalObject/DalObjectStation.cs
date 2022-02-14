using System;
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
        /// returns the stations from dal
        /// </summary>
        /// <returns>DataSource.stations</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations(Predicate<Station> predicate = null)
        {
            return from station in DataSource.stations
                   where station.IsActive == true
                   select station;
        }

        /// <summary>
        /// get spscific station by the id
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        /// add station to dal
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station station)
        {
            checkUniqestation(station.ID);
            DataSource.stations.Add(station);
        }

        /// <summary>
        /// update station in dal
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int LengthStation()
        {
            return DataSource.stations.Count;
        }

        /// <summary>
        /// remove station fron dataSource
        /// </summary>
        /// <param name="idRemove"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        /// <summary>
        /// get deleted stations
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetDeletedStations()
        {
            return from station in DataSource.stations
                   where station.IsActive == false
                   select station;
        }
    }
}
