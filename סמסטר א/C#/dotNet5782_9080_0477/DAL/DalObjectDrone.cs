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

        public List<Drone> GetDronesByList()
        {
            return DataSource.drones;
        }
        public IEnumerable<Drone> GetDrone()
        {
            List<Drone> drones = new List<Drone>();
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                drones.Add(DataSource.drones[i]);
            }
            return drones;
        }

        public Drone GetSpecificDrone(int id)
        {
            try
            {
                return DataSource.drones.First(drone => drone.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exeptions(id);
            }
        }
        public void AddDrone(int id, string model, WeightCatagories weight)
        //public void AddDrone(int id, string model, WeightCatagories weight, DroneStatus status, double battery)
        {
            Drone newDrone = new Drone();
            newDrone.ID = id;
            newDrone.Model = model;
            newDrone.MaxWeight = weight;
            /* newDrone.Status = status;
             newDrone.Battery = battery;*/
            DataSource.drones[DataSource.drones.Count - 1] = newDrone;
        }
    }
    
}
