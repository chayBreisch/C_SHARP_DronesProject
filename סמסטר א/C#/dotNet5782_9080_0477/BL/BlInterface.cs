using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface Bl
    {
        //##############################################################
        //Add functions
        //##############################################################
        public void AddCustomer(ulong id, string name, string phone, LocationBL location);
        public void AddParcel(ulong sender, ulong target, int Weight, int priority);
        public void AddStation(int id, int name, LocationBL location, int ChargeSlots);
        public void AddDrone(int id, string model, int maxWeight, int stationID);


        //##############################################################
        //Get list functions
        //##############################################################
        public IEnumerable<StationToList> GetStationToList();
        public IEnumerable<DroneToList> GetDronesToList();
        public IEnumerable<StationToList> GetStationsByChargeSlots(int status);
        public IEnumerable<ParcelToList> GetParcelToList();
        public IEnumerable<CustomerToList> GetCustomerToList();
        public IEnumerable<DroneToList> GetDroneToListByCondition(Predicate<DroneToList> predicate);
        public IEnumerable<ParcelToList> GetParcelToListByCondition(Predicate<ParcelToList> predicate);
        public Array GetweightCategoriesEnumItem();
        public Array GetPrioritiesEnumItem();


        //##############################################################
        //Get specific item functions
        //##############################################################
        public BO.Customer GetSpecificCustomerBL(Predicate<BO.Customer> predicate);
        public BO.Parcel GetSpecificParcelBL(int id);
        public BO.Station GetSpecificStationBL(int id);
        public BO.Drone GetSpecificDroneBL(int id);


        //##############################################################
        //convert functions
        //##############################################################
        public BO.Drone ConvertDroneToListToDroneBL(DroneToList droneToList);
        public BO.Station ConvertStationToListToStationBL(StationToList stationToList);
        public BO.Parcel ConvertParcelToListToParcelBL(ParcelToList parcelToList);
        public BO.Customer ConvertCustomerToListToCustomerlBL(CustomerToList customerToList);


        //##############################################################
        //update functions
        //##############################################################
        public BO.Station UpdateDataStation(int id, int name = 0, int chargeSlots = -1);
        public BO.Drone UpdateSendDroneToCharge(int id);
        public BO.Drone UpdateUnchargeDrone(int id, double timeInCharge);
        public void UpdateConnectParcelToDrone(int id);
        public void UpdateCollectParcelByDrone(int id);
        public void UpdateSupplyParcelByDrone(int id);
        public BO.Drone UpdateDataDroneModel(int id, string model);


        //##############################################################
        //remove functions
        //##############################################################
        public void RemoveParcel(int id);
        public void RemoveStation(int id);


        //##############################################################
        //help functions
        //##############################################################
        public ParcelStatus findParcelStatus(BO.Parcel parcel);

        public IEnumerable<string> GetCustomerByCondition(Predicate<BO.Customer> predicate);













        //public static void checkUniqeIdCustomer(ulong id, IDAL.IDal dalObject);
        //public static void checkUniqeIdParcel(int id, IDAL.IDal dalObject);
        //public static void checkUniqeIdStation(int id, IDAL.IDal dalObject);
        // public List<BO.Station> getStationWithEmptyChargers();
        // public static void checkUniqeIdDrone(int id, IDAL.IDal dalObject);
        //public List<DroneToList> getDronesByDroneStatus(int status);
        //public List<DroneToList> getDronesByDroneWeight(int status);
        //public List<ParcelToList> getParcelsByPriority(int status);
        //public List<ParcelToList> getParcelsByparcelWeight(int status);
        //public IEnumerable<BO.Parcel> GetParcelsWithoutoutDrone();
        //public IEnumerable<BO.Station> GetStationWithEmptyChargers();
        //public IEnumerable<BO.Drone> GetDronesBL();
        //public IEnumerable<ParcelToList> getParcelToListWithFilter(int weight, int prioritty);
        //public IEnumerable<ParcelToList> getPrcelToListByCondition(Predicate<ParcelToList> predicate);
        //public void updateDataCustomer(ulong id, string name = null, string phone = null);

    }
}