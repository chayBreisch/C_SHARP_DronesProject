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
        public static Drone[] GetDrone()
        {
            Drone[] Drones = new Drone[DataSource.Config.DronesIndex];
            for (int i = 0; i < DataSource.Config.DronesIndex; i++)
            {
                Drones[i] = DataSource.Drones[i];
            }
            return Drones;
        }
        public static Station[] GetStation()
        {
            
            Station[] Stations = new Station[DataSource.Config.StationIndex];

            for (int i = 0; i < DataSource.Config.StationIndex; i++)
            {
                Stations[i] = DataSource.Stations[i];
            }
            return Stations;
        }
        public static Customer[] GetCustomer()
        {
            Customer[] Customers = new Customer[DataSource.Config.CustomerIndex];
            for (int i = 0; i < DataSource.Config.CustomerIndex; i++)
            {
                Customers[i] = DataSource.Customers[i];
            }
            return Customers;
        }
        public static Parcial[] GetParcial()
        {
            Parcial[] Parcials = new Parcial[DataSource.Config.ParcialIndex];
            for (int i = 0; i < DataSource.Config.CustomerIndex; i++)
            {
                Parcials[i] = DataSource.Parcials[i];
            }
            return Parcials;
        }

        public static Drone GetSpecificDrone(int id)
        {
            Drone newDrone = new Drone();
            foreach (var drone in DataSource.Drones)
            {
                if (drone.ID == id) 
                    newDrone = drone;
            }
            return newDrone;
        }

        public static Station GetSpecificStation(int id)
        {
            Station newStation = new Station();
            foreach (var station in DataSource.Stations)
            {
                if (station.ID == id)
                    newStation =  station;
            }
            return newStation;
        }

        public static Customer GetSpecificCustomer(int id)
        {
            Customer newCustomer = new Customer();
            foreach (var customer in DataSource.Customers)
            {
                if (customer.ID == id)
                    newCustomer = customer;
            }
            return newCustomer;
        }

        public static void AddDrone(int id, string model, WeightCatagories weight, DroneStatus status, double battery)
        {
            Drone newDrone = new Drone();
            newDrone.ID = id;
            newDrone.Model = model;
            newDrone.MaxWeight = weight;
            newDrone.Status = status;
            newDrone.Battery = battery;
            DataSource.Drones[DataSource.Config.DronesIndex] = newDrone;
            DataSource.Config.DronesIndex++;
        }
        public static void AddStation(int id, int name, int longitude, int latitude, int chargeSlots)
        {
            Station newStation = new Station();
            newStation.ID = id;
            newStation.Name = name;
            newStation.Longitude = longitude;
            newStation.Latitude = latitude;
            newStation.ChargeSlots = chargeSlots;
            DataSource.Stations[DataSource.Config.StationIndex] = newStation;
            DataSource.Config.StationIndex++;
        }
        public static void AddCustomer(int id, string name, string phone, double latitude, double longitude)
        {
            Customer newCustomer = new Customer();
            newCustomer.ID = id;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = latitude;
            newCustomer.Longitude = longitude;
            DataSource.Customers[DataSource.Config.CustomerIndex] = newCustomer;
            DataSource.Config.CustomerIndex++;
        }
        public static void AddParcial(Parcial newParcial)
        {

            DataSource.Parcials[DataSource.Config.ParcialIndex] = newParcial;
            DataSource.Config.ParcialIndex++;
        }

        /*public static void showStations()
        {
            for (int i = 0; i < DataSource.Config.StationIndex; i++)
            {
                Console.WriteLine($"station {i}: ID: {DataSource.Stations[i].ID}  Name: {DataSource.Stations[i].Name}  " +
                    $"Longitude: {DataSource.Stations[i].Longitude}  Latitude: {DataSource.Stations[i].Latitude}");

            }
        }
        public static void showDrones()
        {
            for (int i = 0; i < DataSource.Config.DronesIndex; i++)
            {
                Console.WriteLine($"drone: {i}: id: {DataSource.Stations[i].ID} Name: {DataSource.Stations[i].Name}" +
                    $"Longitude: {DataSource.Stations[i].Longitude} Latitude: {DataSource.Stations[i].Latitude}");
            }
        }
        public static void showParcials()
        {
            for (int i = 0; i < DataSource.Config.ParcialIndex; i++)
            {
                Console.WriteLine($"parcial {i}: ID: {DataSource.Parcials[i].ID}  PickedUp: {DataSource.Parcials[i].PickedUp}" +
                    $" Priority: {DataSource.Parcials[i].Priority} Requested: {DataSource.Parcials[i].Requested} Scheduled: {DataSource.Parcials[i].Scheduled}" +
                    $" SenderID: {DataSource.Parcials[i].SenderID} TargetID: {DataSource.Parcials[i].TargetID} Weight: {DataSource.Parcials[i].Weight}");
            }
        }
        public static void showCustomers()
        {
            for (int i = 0; i < DataSource.Config.CustomerIndex; i++)
            {
                Console.WriteLine($"customer {i}: ID: {DataSource.Customers[i].ID} Name: {DataSource.Customers[i].Name}" +
                    $"Longitude: {DataSource.Customers[i].Longitude} Latitude: {DataSource.Customers[i].Latitude} Phone: {DataSource.Customers[i].Phone}");
            }
        }*/
        public static void updateCollectParcialByDrone(Drone newDrone, Parcial newParcial)
        {
            newDrone.Status = DroneStatus.Delivery;

        }
        public static void updateConectDroneToParcial(Parcial newParcial)
        {
            Drone newDrone = new Drone();
            for (int i = 0; i < DataSource.Config.DronesIndex; i++)
            {
                if (DataSource.Drones[i].Status == DroneStatus.Available)
                {
                    newDrone = DataSource.Drones[i];
                    break;
                }
            }
            newParcial.DroneID = newDrone.ID;

        }
        public static void updateSupplyParcialToCustomer(Parcial newParcial)
        {
            newParcial.PickedUp = DateTime.Now;
        }
        public static void updateSendDroneToCharge(Drone NewDrone, Station station)
        {
            DroneCharge charger = new DroneCharge();
            NewDrone.Status = DroneStatus.Maintenance;
            charger.DroneID = NewDrone.ID;
            charger.StationID = station.ID;
            DataSource.DroneChargers[DataSource.Config.DroneChargeIndex] = charger;
            DataSource.Config.DroneChargeIndex++;
        }
        public static void updateUnChargeDrone(Drone NewDrone)
        {
            NewDrone.Status = DroneStatus.Available;
            ///////////להוריד את העמדת טעינה מהמערך
        }
    }
}
