﻿using BO;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBL
    {
        //##############################################################
        //Add functions
        //##############################################################
        public void AddCustomer(ulong id, string name, string phone, Location location);
        public int AddParcel(ulong sender, ulong target, int Weight, int priority);
        public void AddStation(int id, int name, Location location, int ChargeSlots);
        public void AddDrone(int id, string model, int maxWeight, int stationID);


        //##############################################################
        //Get list functions
        //##############################################################
        public IEnumerable<StationToList> GetStationsToList();
        public IEnumerable<StationToList> GetStationsByChargeSlots(int status);
        public IEnumerable<DroneToList> GetDronesToListByCondition(Predicate<DroneToList> predicate);
        public IEnumerable<DroneToList> GetDronesToList();
        public IEnumerable<CustomerToList> GetCustomersToList();
        public IEnumerable<BO.Customer> GetCustomersByCondition(Predicate<BO.Customer> predicate);
        public IEnumerable<ParcelToList> GetParcelsToList();
        public IEnumerable<ParcelToList> GetParcelsToListByCondition(Predicate<ParcelToList> predicate);
        public IEnumerable<BO.Parcel> GetParcelsByCondition(Predicate<BO.Parcel> predicate);
        public Array GetweightCategoriesEnumItem();
        public Array GetPrioritiesEnumItem();
        public IEnumerable<StationToList> GetDeletedStationsToList();
        public IEnumerable<DroneToList> GetDeletedDronesToList();
        public IEnumerable<CustomerToList> GetDeletedCustomersToList();
        public IEnumerable<ParcelToList> GetDeletedParcelsToList();


        //##############################################################
        //Get specific item functions
        //##############################################################
        public BO.Drone GetSpecificDroneBLWithDeleted(int id);
        public BO.Customer GetSpecificCustomerBL(Predicate<BO.Customer> predicate);
        public CustomerToList GetSpecificCustomerToList(ulong id);
        public BO.Parcel GetSpecificParcelBL(int id);
        public BO.Station GetSpecificStationBL(int id);
        public BO.Drone GetSpecificDroneBL(int id);
        public double getRateOfCharging();


        //##############################################################
        //convert functions
        //##############################################################
        public BO.Drone ConvertDroneToListToDroneBL(DroneToList droneToList);
        public BO.DroneToList ConvertDroneBLToDroneToList(BO.Drone drone);
        public BO.Station ConvertStationToListToStationBL(StationToList stationToList);
        public BO.Parcel ConvertParcelToListToParcelBL(ParcelToList parcelToList);
        public BO.Customer ConvertCustomerToListToCustomerlBL(CustomerToList customerToList);
        public DO.Parcel convertParcelBlToDal(BO.Parcel parcel);


        //##############################################################
        //update functions
        //##############################################################
        public BO.Station UpdateStation(BO.Station station);
        public BO.Drone UpdateSendDroneToCharge(int id);
        public BO.Drone UpdateUnchargeDrone(int id, double timeInCharge);
        public void UpdateConnectParcelToDrone(int id);
        public void UpdateCollectParcelByDrone(int id);
        public void UpdateSupplyParcelByDrone(int id);
        public BO.Drone UpdateDataDroneModel(int id, string model);
        public void updateParecl(BO.ParcelToList parcel);
        public void updateParecl(BO.Parcel parcel);
        public BO.Drone UpdateDataDrone(BO.Drone drone);


        //##############################################################
        //remove functions
        //##############################################################
        public void RemoveParcel(int id);
        public void RemoveStation(int id);
        public void RemoveCustomer(ulong id);
        public void RemoveDrone(int id);


        //##############################################################
        //help functions
        //##############################################################
        public ParcelStatus findParcelStatus(BO.Parcel parcel);
        public IEnumerable<string> GetCustomersNamesByCondition(Predicate<BO.Customer> predicate);
        public void StartSimulation(BO.Drone drone, BackgroundWorker worker, Action<BO.Drone> updateDrone, Func<bool> needToStop);
    }
}