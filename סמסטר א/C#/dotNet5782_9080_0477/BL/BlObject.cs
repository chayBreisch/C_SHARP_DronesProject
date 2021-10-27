using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IBL;
namespace BlObject
{
    public class BlObject : InterFaceIbl
    {
        public BlObject()
        {
            DataSource.Initialize();
        }
        //################################################################
        //functions that returns the basic data arrays from dataSource
        //################################################################
        public IEnumerable<Drone> GetDrone()
        {
            List<Drone> drones = new List<Drone>();
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                drones.Add(DataSource.drones[i]);
            }
            return drones;
        }
        public IEnumerable<Station> GetStation()
        {
            List<Station> stations = new List<Station>();
            for (int i = 0; i < DataSource.stations.Count; i++)
            {
                stations.Add(DataSource.stations[i]);
            }
            return stations;
        }
        public IEnumerable<Customer> GetCustomer()
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < DataSource.customers.Count; i++)
            {
                customers.Add(DataSource.customers[i]);
            }
            return customers;
        }
        public IEnumerable<Parcel> GetParcial()
        {
            List<Parcel> parcels = new List<Parcel>();
            for (int i = 0; i < DataSource.parcials.Count; i++)
            {
                parcels.Add(DataSource.parcials[i]);
            }
            return parcels;
        }

        public IEnumerable<DroneCharge> GetDroneCharge()
        {
            List<DroneCharge> droneChargers = new List<DroneCharge>();
            for (int i = 0; i < DataSource.droneChargers.Count; i++)
            {
                droneChargers.Add(DataSource.droneChargers[i]);
            }
            return droneChargers;
        }
        //#######################################################################
        //functions that returns the specific item from the dataSource by ID
        //#######################################################################
        public Drone GetSpecificDrone(int id)
        {
            return DataSource.drones.FirstOrDefault(drone => drone.ID == id);
        }

        public Station GetSpecificStation(int id)
        {
            
            return DataSource.stations.FirstOrDefault(station => station.ID == id);
        }
        public Customer GetSpecificCustomer(int id)
        {
            return DataSource.customers.FirstOrDefault(customer => customer.ID == id);

        }
        public Parcel GetSpecificParcial(int id)
        {
            return DataSource.parcials.FirstOrDefault(parcial => parcial.ID == id);

        }
        //###############################################################
        //functions that are adding an item to the dataSources array
        //###############################################################
        public void AddDrone(int id, string model, WeightCatagories weight, DroneStatus status, double battery)
        {
            Drone newDrone = new Drone(id, model, weight, status, battery); 
            DataSource.drones[DataSource.drones.Count - 1] = newDrone;
        }
        public void AddStation(int id, int name, int longitude, int latitude, int chargeSlots)
        {
            Station newStation = new Station(id, name, longitude, latitude, chargeSlots);
            DataSource.stations[DataSource.stations.Count - 1] = newStation;
        }
        public void AddCustomer(int id, string name, string phone, double latitude, double longitude)
        {
            Customer newCustomer = new Customer(id, name, phone, latitude, longitude);
            DataSource.customers[DataSource.customers.Count - 1] = newCustomer;
        }
        public void AddParcial(int id, int senderId, int targetId, WeightCatagories weight, Priorities priority)
        {
            Parcel newParcial = new Parcel(id, senderId, targetId,  weight, priority);
            DataSource.parcials[DataSource.parcials.Count - 1] = newParcial;
        }

        //################################################
        //functions that update the dataSource array 
        //################################################
        public void updateConectDroneToParcial(int id)
        {
            Parcel newParcial = GetSpecificParcial(id);
            Drone drone = new Drone();
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].Status == DroneStatus.Available)
                {
                    drone = DataSource.drones[i];
                    drone.Status = DroneStatus.Delivery;
                    break;
                }
            }
            newParcial.DroneID = drone.ID;
            int index1 = DataSource.drones.FindIndex(p => p.ID == newParcial.ID);
            int index = DataSource.drones.FindIndex(d => d.ID == drone.ID);
            DataSource.drones[index] = drone;
            DataSource.parcials[index1] = newParcial;
        }


        public void updateCollectParcialByDrone(int id)
        {

            Parcel newParcial = GetSpecificParcial(id);
            if (newParcial.DroneID == 0)
            {
                Console.WriteLine("you didnt conect a drone");
            }
            newParcial.PickedUp = DateTime.Now;

        }

        public void updateSupplyParcialToCustomer(int id)
        {
            Parcel newParcial = GetSpecificParcial(id);

            //if (newParcial.PickedUp)////////////////////////////////////////////////////
            {
                Console.WriteLine("you didnt collect a drone");
            }
            newParcial.Delivered = DateTime.Now;
        }
        public void updateSendDroneToCharge(int droneId, int statoinId)
        {
            DroneCharge droneCharge = new DroneCharge();
            int numOfChargers = 0;
            Station station = GetSpecificStation(statoinId);
            Drone newDrone = GetSpecificDrone(droneId);
            numOfChargers = 0;
            for (int j = 0; j < DataSource.droneChargers.Count; j++)
            {
                if (station.ID == DataSource.droneChargers[j].StationID)
                    numOfChargers++;
            }
            if (numOfChargers < station.ChargeSlots)
            {
                droneCharge.DroneID = newDrone.ID;
                droneCharge.StationID = station.ID;

            }

            newDrone.Status = DroneStatus.Maintenance;
            DataSource.droneChargers[DataSource.droneChargers.Count - 1] = droneCharge;
        }
        public void updateUnChargeDrone(int id)
        {
            Drone NewDrone = GetSpecificDrone(id);
            int index = 0;
            for (int i = 0; i < DataSource.droneChargers.Count; i++)
            {
                if (DataSource.droneChargers[i].DroneID == NewDrone.ID)
                {
                    DataSource.droneChargers.RemoveAt(i);
                    index = i;
                    break;
                }
            }
            NewDrone.Status = DroneStatus.Available;
            NewDrone.Battery = 100;
            int index1 = DataSource.drones.FindIndex(d => d.ID == NewDrone.ID);
            DataSource.drones[index1] = NewDrone;
        }



        //################################################
        //functions to show information
        //################################################


        public IEnumerable<Parcel> showParcelsWithoutoutDrone()
        {
            IEnumerable<Parcel> parcels = GetParcial();
            foreach (var parcel in parcels)
            {
                if (parcel.DroneID == 0)
                {
                    yield return parcel;
                }
            }
        }


        public IEnumerable<Station> showStationWithEmptyChargers()
        {
            int numOfChargers = 0;
            IEnumerable<Station> stations = GetStation();
            IEnumerable<DroneCharge> droneChargers = GetDroneCharge();

            foreach (var station in stations)
            {
                foreach (var droneCharge in droneChargers)
                {
                    if (station.ID == droneCharge.StationID)
                        numOfChargers++;
                }
                if (numOfChargers < station.ChargeSlots)
                {
                    yield return station;
                }
            }
        }
    }
}