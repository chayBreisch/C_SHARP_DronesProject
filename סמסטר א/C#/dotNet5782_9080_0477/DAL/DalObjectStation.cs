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
                throw new Exeptions(id);
            }
        }

        public void AddStation(Station station)
        {

            DataSource.stations.Add(station);
        }
    }
}
