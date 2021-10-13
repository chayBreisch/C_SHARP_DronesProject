using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
    class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        public Drone[] GetDrone()
        {
            Drone[] Drones = new Drone[DataSource.Config.DronesIndex];
            for (int i = 0; i < DataSource.Config.DronesIndex; i++)
            {
                Drones[i] = DataSource.Drones[i];
            }
            return Drones;
        }
        public Station[] GetStation()
        {
            Station[] Stations = new Station[DataSource.Config.StationIndex];

            for (int i = 0; i < DataSource.Config.StationIndex; i++)
            {
                Stations[i] = DataSource.Stations[i];
            }
            return Stations;
        }
        public Customer[] GetCustomer()
        {
            Customer[] Customers = new Customer[DataSource.Config.CustomerIndex];
            for (int i = 0; i < DataSource.Config.CustomerIndex; i++)
            {
                Customers[i] = DataSource.Customers[i];
            }
            return Customers;
        }
        public Parcial[] GetParcial()
        {
            Parcial[] Parcials = new Parcial[DataSource.Config.ParcialIndex];
            for (int i = 0; i < DataSource.Config.CustomerIndex; i++)
            {
                Parcials[i] = DataSource.Parcials[i];
            }
            return Parcials;
        }
    }

}
