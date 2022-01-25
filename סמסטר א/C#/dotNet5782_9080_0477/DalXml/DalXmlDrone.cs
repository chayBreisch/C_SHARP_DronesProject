using DALException;
using DO;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public partial class DalXml : IDal
    {
        /// <summary>
        /// check uniqe drone
        /// </summary>
        /// <param name="id"></param>
        private void checkUniqeDrone(int id)
        {
            if (GetDrones().Any(drone => drone.ID == id))
                throw new NotUniqeID(id, typeof(Drone));
        }

        /// <summary>
        /// add drone
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            List<Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath).ToList();
            checkUniqeDrone(drone.ID);
            droneList.Add(drone);
            XMLTools.SaveListToXMLSerializer<Drone>(droneList, dir + droneFilePath);
        }

        /// <summary>
        /// remove drone
        /// </summary>
        /// <param name="id"></param>
        public void RemoveDrone(int id)
        {
            IEnumerable<Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath);
            if (!droneList.Any(d => d.ID == id))
            {
                throw new NotExistObjWithID(id, typeof(Drone));
            }
            Drone drone = GetDroneById(d => d.ID == id);
            drone.IsActive = false;
            UpdateDrone(drone);
        }

        /// <summary>
        /// update drone details
        /// </summary>
        /// <param name="drone"></param>
        public void UpdateDrone(Drone drone)
        {
            List<Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath).ToList();

            int index = droneList.FindIndex(d => d.ID == drone.ID);

            if (index == -1)
                throw new NotExistObjWithID(drone.ID, typeof(Drone));
            droneList[index] = drone;
            XMLTools.SaveListToXMLSerializer<Drone>(droneList, dir + droneFilePath);
        }

        /// <summary>
        /// get drone by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone GetDroneById(Predicate<Drone> predicat = null)
        {
            Drone drone1 = new Drone();
            try
            {
                IEnumerable<Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath);
                drone1 = (from drone in droneList
                        where predicat(drone)
                        select drone).First();
            }
            catch (Exception e) { }
            return drone1;
        }

        /// <summary>
        /// get drones
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicat = null)
        {
            IEnumerable<Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath);
            predicat ??= ((st) => true);
            return from drone in droneList
                   where predicat(drone) && drone.IsActive == true
                   orderby drone.ID
                   select drone;
        }
    }
}