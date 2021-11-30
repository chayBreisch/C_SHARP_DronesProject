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
        /// returns the drones by list from dal
        /// </summary>
        /// <returns>DataSource.drones</returns>

        public List<Drone> GetDronesByList()
        {
            return DataSource.drones;
        }

        /// <summary>
        /// returns the drones from dal
        /// </summary>
        /// <returns>DataSource.drones</returns>
        public IEnumerable<Drone> GetDrone()
        {
            foreach (var drone in DataSource.drones)
            {
                yield return drone;
            }
        }

        /// <summary>
        /// return a specific drone by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Drone</returns>
        /*public Drone GetSpecificDrone(int id)
        {
            try
            {
                return DataSource.drones.Find(drone => drone.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }
*/

        public Drone getDroneById(Predicate<Drone> predicate)
        {
            //try ToDO
            return (from drone in DataSource.drones
                    where predicate(drone)
                    select drone).First();

        }

        /// <summary>
        /// check if uniqe drone 
        /// </summary>
        /// <param name="id"></param>
        public void CheckUniqeDrone(int id)
        {
            if (DataSource.drones.Any(drone => drone.ID == id))
                throw new NotUniqeID(id, typeof(Drone));
        }

        /// <summary>
        /// add drone to dal
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            CheckUniqeDrone(drone.ID);
            DataSource.drones.Add(drone);
        }

        /// <summary>
        /// update the drone in dal
        /// </summary>
        /// <param name="drone"></param>
        public void updateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(d => d.ID == drone.ID);
            DataSource.drones[index] = drone;
        }

        /// <summary>
        /// get the drones with a specific condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Drone> getDroneByCondition(Predicate<Drone> predicate)
        {
            //try todo
            return (from drone in DataSource.drones
                    where predicate(drone)
                    select drone);
        }
    }
}
