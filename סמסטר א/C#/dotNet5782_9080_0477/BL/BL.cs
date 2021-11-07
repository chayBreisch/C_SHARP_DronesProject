using System;
using DalObject;
using IBL.BO;
using IBL;
using System.Collections.Generic;
using IDAL.DO;

namespace BL
{
    class BL : Bl
    {
        IDAL.IDal dalObject;
        public BL()
        {
            IDAL.IDal dalObject = new DalObject.DalObject();
            double[] arrayEletric = dalObject.requestElectric();
            double electricAvailable = arrayEletric[0];
            double electricLightHeight = arrayEletric[1];
            double electricMidHeight = arrayEletric[2];
            double electricHeavyHeight = arrayEletric[3];
            double electricChargingRate = arrayEletric[4];

            //List<DalObject.DataSource.drones> drones =  dalObject.GetDronesByList();
        }



        public void AddCustomer(ulong id, string name, string phone, double longitude, double latitude)
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer();
            customer.ID = id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Longitude = longitude;
            customer.Latitude = latitude;
            dalObject.AddCustomer(customer);
        }

        public void AddStation(int id, int name, double longitude, double latitude, int ChargeSlots)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = longitude;
            station.Latitude = latitude;
            station.ChargeSlots = ChargeSlots;
            dalObject.AddStation(station);
        }


        public void AddDrone(int id, string model, WeightCatagories maxWeight)
        {
            IDAL.DO.Drone drone = new IDAL.DO.Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
            dalObject.AddDrone(drone);
        }

        public void AddParcel(int id, int sender, int target, WeightCatagories Weight, Priorities priority)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
            parcel.ID = id;
            parcel.SenderID = sender;
            parcel.TargetID = target;
            parcel.Weight = Weight;
            parcel.Priority = priority;
            dalObject.AddParcel(parcel);
        }















        public bool CheckLongIdIsValid(ulong id)
        {
            return id > 100000000 && id < 1000000000;
        }

        public bool CheckValidIdCustomer(ulong id)
        {
            int sum = 0, digit;
            for (int i = 0; i < 9; i++)
            {
                digit = (int)(id % 10);
                if ((i % 2) == 0)
                {
                    sum += digit * 2;
                }
                else
                {
                    sum += digit;
                }
                id /= 10;
            }
            if ((sum % 10) == 0)
            {
                return true;
            }
            return false;
        }














        /* public void AddCustomer(int id, string name, string phone, double latitude, double longitude)
         {
             Customer newCustomer = new Customer(id, name, phone, latitude, longitude);
            *//* newCustomer.ID = id;
             newCustomer.Name = name;
             newCustomer.Phone = phone;
             newCustomer.Latitude = latitude;
             newCustomer.Longitude = longitude;*//*
             dalObject.AddCustomer(newCustomer);
         }

        */

        //################################################
        //functions that update the dataSource array 
        //################################################
        /*public void updateConectDroneToParcial(int id)
        {
            Parcel newParcial = dalObject.GetSpecificParcial(id);
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
            DataSource.parcels[index1] = newParcial;
        }


        public void updateCollectParcialByDrone(int id)
        {

            Parcel newParcial = dalObject.GetSpecificParcial(id);
            if (newParcial.DroneID == 0)
            {
                Console.WriteLine("you didnt conect a drone");
            }
            newParcial.PickedUp = DateTime.Now;

        }

        public void updateSupplyParcialToCustomer(int id)
        {
            Parcel newParcial = dalObject.GetSpecificParcial(id);

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
            Station station = dalObject.GetSpecificStation(statoinId);
            Drone newDrone = dalObject.GetSpecificDrone(droneId);
            numOfChargers = 0;
            for (int j = 0; j < dalObject.returnLengthDroneCharger(); j++)
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
            Drone NewDrone = dalObject.GetSpecificDrone(id);
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
        }*/
    }
}
