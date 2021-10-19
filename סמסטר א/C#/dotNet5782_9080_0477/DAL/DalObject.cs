﻿using System;
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
            for (int i = 0; i < DataSource.Config.ParcialIndex; i++)
            {
                Parcials[i] = DataSource.Parcials[i];
            }
            return Parcials;
        }

        public static DroneCharge[] GetDroneCharge()
        {
            DroneCharge[] droneChargers = new DroneCharge[DataSource.Config.DroneChargeIndex];
            for (int i = 0; i < DataSource.Config.DroneChargeIndex; i++)
            {
                droneChargers[i] = DataSource.DroneChargers[i];
            }
            return droneChargers;
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
        public static Parcial GetSpecificParcial(int id)
        {
            Parcial newParcial = new Parcial();
            foreach (var parcial in DataSource.Parcials)
            {
                if (parcial.ID == id)
                    newParcial = parcial;
            }
            return newParcial;
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
        public static void AddParcial(int id, int senderId, int targetId, WeightCatagories weight, Priorities priority)
        {
            Parcial newParcial = new Parcial();
            newParcial.ID = id;
            newParcial.SenderID = senderId;
            newParcial.TargetID = targetId;
            newParcial.Weight = weight;
            newParcial.Priority = priority;
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

        public static void updateConectDroneToParcial(int id)
        {
            Parcial newParcial = findCorrectParcial(id);
            Drone newDrone = new Drone();
            for (int i = 0; i < DataSource.Config.DronesIndex; i++)
            {
                if (DataSource.Drones[i].Status == DroneStatus.Available)
                {
                    DataSource.Drones[i].Status = DroneStatus.Delivery;
                    newDrone = DataSource.Drones[i];
                    break;
                }
            }
            newParcial.DroneID = newDrone.ID;

        }

        
        public static void updateCollectParcialByDrone(int id)
        {
           
            Parcial newParcial =  findCorrectParcial(id);
            if(newParcial.DroneID == 0)
            {
                Console.WriteLine("you didnt conect a drone");
            }
            newParcial.PickedUp = DateTime.Now;

        }
       
        public static void updateSupplyParcialToCustomer(int id)
        {
            Parcial newParcial = findCorrectParcial(id);
            //if (newParcial.PickedUp.)///////////////////////
            {
                Console.WriteLine("you didnt collect a drone");
            }
            newParcial.Delivered = DateTime.Now;
        }
        public static void updateSendDroneToCharge(int droneId, int statoinId/*Drone NewDrone, Station station*/)///////////////////////////////////////
        {
            DroneCharge droneCharge = new DroneCharge();
            int numOfChargers = 0;
            Station station = findCorrectStation(statoinId);
            Drone newDrone = findCorrectDrone(droneId);
                numOfChargers = 0;
                for (int j = 0; j < DataSource.Config.DroneChargeIndex; j++)
                {
                    if (station.ID == DataSource.DroneChargers[j].StationID)
                        numOfChargers++;
                }
                if (numOfChargers < station.ChargeSlots)
                {
                    droneCharge.DroneID = newDrone.ID;
                    droneCharge.StationID = station.ID;

                }

            newDrone.Status = DroneStatus.Maintenance;
            DataSource.DroneChargers[DataSource.Config.DroneChargeIndex] = droneCharge;
            DataSource.Config.DroneChargeIndex++;
        }
        public static void updateUnChargeDrone(int id)
        {
            Drone NewDrone = findCorrectDrone(id);
            int index = 0;
            for(int i = 0; i < DataSource.Config.DroneChargeIndex; i++)
            {
                if(DataSource.DroneChargers[i].DroneID == NewDrone.ID)
                {
                    index = i;
                    break;
                }
            }
            for(int i = index; i < DataSource.Config.DroneChargeIndex - 1; i++)
            {
                DataSource.Drones[i + 1] = DataSource.Drones[i];
            }
            DataSource.Config.DroneChargeIndex--;
            NewDrone.Status = DroneStatus.Available;
            NewDrone.Battery = 100;
        }


        public static Parcial findCorrectParcial(int id)
        {
            Parcial newParcial = new Parcial();
            for (int i = 0; i < DataSource.Config.ParcialIndex; i++)
            {
                if (DataSource.Parcials[i].ID == id)
                {
                    newParcial = DataSource.Parcials[i];
                    break;
                }
            }
            return newParcial;
        }
        public static Station findCorrectStation(int id)
        {
            Station newStation = new Station();
            for (int i = 0; i < DataSource.Config.ParcialIndex; i++)
            {
                if (DataSource.Stations[i].ID == id)
                {
                    newStation = DataSource.Stations[i];
                    break;
                }
            }
            return newStation;
        }
        public static Drone findCorrectDrone(int id)
        {
            Drone newDrone = new Drone();
            for (int i = 0; i < DataSource.Config.ParcialIndex; i++)
            {
                if (DataSource.Stations[i].ID == id)
                {
                    newDrone = DataSource.Drones[i];
                    break;
                }
            }
            return newDrone;
        }
    }
}
