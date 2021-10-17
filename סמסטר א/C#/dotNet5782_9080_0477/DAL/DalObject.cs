using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
    struct DalObject
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
        public void AddDrone(Drone newDrone)
        {
            DataSource.Drones[DataSource.Config.DronesIndex] = newDrone;
            DataSource.Config.DronesIndex++;
        }
        public void AddStation(Station newStation)
        {
            DataSource.Stations[DataSource.Config.StationIndex] = newStation;
            DataSource.Config.StationIndex++;
        }
        public void AddCustomer(Customer newCustomer)
        {
            DataSource.Customers[DataSource.Config.CustomerIndex] = newCustomer;
            DataSource.Config.CustomerIndex++;
        }
        public void AddParcial(Parcial newParcial)
        {
            DataSource.Parcials[DataSource.Config.ParcialIndex] = newParcial;
            DataSource.Config.ParcialIndex++;
        }
       
        public void showStations()
        {
            for (int i = 0; i < DataSource.Config.StationIndex; i++)
            {
                Console.WriteLine($"station {i}: ID: {DataSource.Stations[i].ID}  Name: {DataSource.Stations[i].Name}  " +
                    $"Longitude: {DataSource.Stations[i].Longitude}  Latitude: {DataSource.Stations[i].Latitude}");

            }
        }
        public void showDrones()
        {
            for (int i = 0; i < DataSource.Config.DronesIndex; i++)
            {
                Console.WriteLine($"drone: {i}: id: {DataSource.Stations[i].ID} Name: {DataSource.Stations[i].Name}" +
                    $"Longitude: {DataSource.Stations[i].Longitude} Latitude: {DataSource.Stations[i].Latitude}");
            }
        }
        public void showParcials()
        {
            for (int i = 0; i < DataSource.Config.ParcialIndex; i++)
            {
                Console.WriteLine($"parcial {i}: ID: {DataSource.Parcials[i].ID}  PickedUp: {DataSource.Parcials[i].PickedUp}" +
                    $" Priority: {DataSource.Parcials[i].Priority} Requested: {DataSource.Parcials[i].Requested} Scheduled: {DataSource.Parcials[i].Scheduled}" +
                    $" SenderID: {DataSource.Parcials[i].SenderID} TargetID: {DataSource.Parcials[i].TargetID} Weight: {DataSource.Parcials[i].Weight}");
            }
        }
        public void showCustomers()
        {
            for (int i = 0; i < DataSource.Config.CustomerIndex; i++)
            {
                Console.WriteLine($"customer {i}: ID: {DataSource.Customers[i].ID} Name: {DataSource.Customers[i].Name}" +
                    $"Longitude: {DataSource.Customers[i].Longitude} Latitude: {DataSource.Customers[i].Latitude} Phone: {DataSource.Customers[i].Phone}");
            }
        }
        public void updateCollectParcialByDrone(Drone newDrone, Parcial newParcial)
        {
            newDrone.Status = DroneStatus.Delivery;
            
        }
        public void updateConectDroneToParcial(Parcial newParcial)
        {
            Drone newDrone = new Drone();
            for (int i = 0; i < DataSource.Config.DronesIndex; i++)
            {
                if(DataSource.Drones[i].Status == DroneStatus.Available)
                {
                    newDrone = DataSource.Drones[i];
                }
            }
            newParcial.DroneID = newDrone.ID;

        }
        public void updateSupplyParcialToCustomer(Parcial newParcial)
        {
            newParcial.PickedUp = DateTime.Now;
        }
        public void updateSendDroneToCharge(Drone NewDrone, Station station)
        {
            DroneCharge charger = new DroneCharge();
            NewDrone.Status = DroneStatus.Maintenance;
            charger.DroneID = NewDrone.ID;
            charger.StationID = station.ID;
            DataSource.DroneChargers[DataSource.Config.DroneChargeIndex] = charger;
            DataSource.Config.DroneChargeIndex++;
        }
        public void updateUnChargeDrone(Drone NewDrone)
        {
            NewDrone.Status = DroneStatus.Available;
            ///////////להוריד את העמדת טעינה מהמערך
        }
    }
}
