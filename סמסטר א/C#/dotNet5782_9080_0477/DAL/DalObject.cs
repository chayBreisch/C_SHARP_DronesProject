using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        //################################################################
        //functions that returns the basic data arrays from dataSource
        //################################################################
        public static List<Drone> GetDrone()
        {
            List<Drone> drones = new List<Drone>();
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                drones[i] = DataSource.drones[i];
            }
            return drones;
        }
        public static List<Station> GetStation()
        {

            List<Station> stations = new List<Station>();
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                stations[i] = DataSource.stations[i];
            }
            return stations;
        }
        public static List<Customer> GetCustomer()
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < DataSource.customers.Count; i++)
            {
                customers[i] = DataSource.customers[i];
            }
            return customers;
        }
        public static List<Parcial> GetParcial()
        {
            List<Parcial> parcels = new List<Parcial>();
            for (int i = 0; i < DataSource.parcials.Count; i++)
            {
                parcels[i] = DataSource.parcials[i];
            }
            return parcels;
        }

        public static List<DroneCharge> GetDroneCharge()
        {
            List<DroneCharge> droneChargers = new List<DroneCharge>();
            for (int i = 0; i < DataSource.droneChargers.Count; i++)
            {
                droneChargers[i] = DataSource.droneChargers[i];
            }
            return droneChargers;
        }
        //#######################################################################
        //functions that returns the specific item from the dataSource by ID
        //#######################################################################
        public static Drone GetSpecificDrone(int id)
        {
            Drone newDrone = new Drone();
            foreach (var drone in DataSource.drones)
            {
                if (drone.ID == id)
                    newDrone = drone;
            }
            return newDrone;
        }

        public static Station GetSpecificStation(int id)
        {
            Station newStation = new Station();
            foreach (var station in DataSource.stations)
            {
                if (station.ID == id)
                    newStation = station;
            }
            return newStation;
        }
        public static Customer GetSpecificCustomer(int id)
        {
            Customer newCustomer = new Customer();
            foreach (var customer in DataSource.customers)
            {
                if (customer.ID == id)
                    newCustomer = customer;
            }
            return newCustomer;
        }
        public static Parcial GetSpecificParcial(int id)
        {
            Parcial newParcial = new Parcial();
            foreach (var parcial in DataSource.parcials)
            {
                if (parcial.ID == id)
                    newParcial = parcial;
            }
            return newParcial;
        }
        //###############################################################
        //functions that are adding an item to the dataSources array
        //###############################################################
        public static void AddDrone(int id, string model, WeightCatagories weight, DroneStatus status, double battery)
        {
            Drone newDrone = new Drone();
            newDrone.ID = id;
            newDrone.Model = model;
            newDrone.MaxWeight = weight;
            newDrone.Status = status;
            newDrone.Battery = battery;
            DataSource.drones[DataSource.drones.Count] = newDrone;
        }
        public static void AddStation(int id, int name, int longitude, int latitude, int chargeSlots)
        {
            Station newStation = new Station();
            newStation.ID = id;
            newStation.Name = name;
            newStation.Longitude = longitude;
            newStation.Latitude = latitude;
            newStation.ChargeSlots = chargeSlots;
            DataSource.stations[DataSource.stations.Count] = newStation;
        }
        public static void AddCustomer(int id, string name, string phone, double latitude, double longitude)
        {
            Customer newCustomer = new Customer();
            newCustomer.ID = id;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = latitude;
            newCustomer.Longitude = longitude;
            DataSource.customers[DataSource.customers.Count] = newCustomer;
        }
        public static void AddParcial(int id, int senderId, int targetId, WeightCatagories weight, Priorities priority)
        {
            Parcial newParcial = new Parcial();
            newParcial.ID = id;
            newParcial.SenderID = senderId;
            newParcial.TargetID = targetId;
            newParcial.Weight = weight;
            newParcial.Priority = priority;
            DataSource.parcials[DataSource.parcials.Count] = newParcial;
        }

        //################################################
        //functions that update the dataSource array 
        //################################################
        public static void updateConectDroneToParcial(int id)
        {
            Parcial newParcial = GetSpecificParcial(id);
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


        public static void updateCollectParcialByDrone(int id)
        {

            Parcial newParcial = GetSpecificParcial(id);
            if (newParcial.DroneID == 0)
            {
                Console.WriteLine("you didnt conect a drone");
            }
            newParcial.PickedUp = DateTime.Now;

        }

        public static void updateSupplyParcialToCustomer(int id)
        {
            Parcial newParcial = GetSpecificParcial(id);

            //if (newParcial.PickedUp.)////////////////////////////////////////////////////
            {
                Console.WriteLine("you didnt collect a drone");
            }
            newParcial.Delivered = DateTime.Now;
        }
        public static void updateSendDroneToCharge(int droneId, int statoinId)
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
            DataSource.droneChargers[DataSource.droneChargers.Count] = droneCharge;
        }
        public static void updateUnChargeDrone(int id)
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
    }
 }
