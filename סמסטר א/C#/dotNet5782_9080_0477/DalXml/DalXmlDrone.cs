using DAL;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalXml
{
    public partial class DalXml
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
            IEnumerable<Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath);
            checkUniqeDrone(drone.ID);
            droneList.ToList().Add(drone);
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
            Drone drone = GetDroneById(id);
            drone.IsActive = false;
            UpdateDrone(drone);
        }

        /// <summary>
        /// update drone
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
        public Drone GetDroneById(int id)
        {
            try
            {
                IEnumerable<Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath);
                return (from drone in droneList
                        where drone.ID == id
                        select drone).First();
            }
            catch (Exception e)
            {
                throw new NotExistObjWithID(id, typeof(Drone), e);
            }

        }

        /// <summary>
        /// get all drones
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