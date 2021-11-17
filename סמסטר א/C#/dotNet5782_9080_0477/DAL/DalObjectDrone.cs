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
            foreach (var drone in DataSource.drones)
            {
                yield return drone;
            }
        }
        /*public IEnumerable<Customer> GetCustomer()
        {

            foreach (var customer in DataSource.customers)
            {
                yield return customer;
            }
        }*/
        public Drone GetSpecificDrone(int id)
        {
            try
            {
                return DataSource.drones.First(drone => drone.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }
        public void AddDrone(Drone drone)
        {
            DataSource.drones.Add(drone);
        }

        public void updateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(d => d.ID == drone.ID);
            DataSource.drones[index] = drone;
        }
    }
    
}
