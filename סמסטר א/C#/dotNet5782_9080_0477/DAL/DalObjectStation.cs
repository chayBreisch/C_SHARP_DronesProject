﻿using System;
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
            foreach (var station in DataSource.stations)
            {
                yield return station;
            }
        }
        public Station GetSpecificStation(int id)
        {
            try
            {
                return DataSource.stations.First(station => station.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }

        public void AddStation(Station station)
        {

            DataSource.stations.Add(station);
        }
        public void updateStation(Station station)
        {
            int index = DataSource.stations.FindIndex(d => d.ID == station.ID);
            DataSource.stations[index] = station;
        }
        public int lengthStation()
        {
            return DataSource.stations.Count;
        }
    }
}
