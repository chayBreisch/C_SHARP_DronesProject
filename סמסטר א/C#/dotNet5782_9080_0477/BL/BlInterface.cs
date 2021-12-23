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
        public void AddCustomerToDal(ulong id, string name, string phone, LocationBL location);
        public void AddParcel(ulong sender, ulong target, int Weight, int priority);
        public void AddParcelToDal(/*ulong id,*/ ulong sender, ulong target, int Weight, int priority);
        public void addStation(int id, int name, LocationBL location, int ChargeSlots);
        public void AddStationToDal(int id, int name, LocationBL location, int ChargeSlots);
        public void addDrone(int id, string model, int maxWeight, int stationID);
        public void AddDroneToDal(int id, string model, int maxWeight);


        //##############################################################
        //Get list functions
        //##############################################################
        public List<BO.Customer> GetCustomersBL();
        public List<DO.Parcel> getParcelsWithoutoutDrone();
        public List<BO.Station> GetStationsBL();
        public List<BO.Parcel> GetParcelsBL();
        public List<BO.Parcel> GetParcelsWithoutoutDrone();
        public List<BO.Station> GetStationWithEmptyChargers();
        public List<BO.Drone> GetDronesBL();
        public List<StationToList> getStationToList();
        public List<DroneToList> getDroneToList();
        public List<StationToList> getStationsByChargeSlots(int status);
        public List<ParcelToList> getParcelToList();
        public List<CustomerToList> getCustomerToList();
        public IEnumerable<DroneToList> getDroneToListByCondition(Predicate<DroneToList> predicate);
        public IEnumerable<ParcelToList> getParcelToListByCondition(Predicate<ParcelToList> predicate);
        public Array getweightCategoriesEnumItem();
        public Array getPrioritiesEnumItem();
        public IEnumerable<ParcelToList> getParcelToListWithFilter(int weight, int prioritty);
        public IEnumerable<ParcelToList> getPrcelToListByCondition(Predicate<ParcelToList> predicate);


        //##############################################################
        //Get specific item functions
        //##############################################################
        public BO.Customer GetSpecificCustomerBL(ulong id);
        public BO.Parcel GetSpecificParcelBL(int id);
        public BO.Station GetSpecificStationBL(int id);
        public BO.Drone getSpecificDroneBLFromList(int id);
        public BO.Drone GetSpecificDroneBL(int id);

        //##############################################################
        //convert functions
        //##############################################################
        public BO.Customer convertDalCustomerToBl(DO.Customer c);
        public BO.Drone convertDalDroneToBl(DO.Drone d);
        public BO.Drone convertDroneToListToDroneBL(DroneToList droneToList);
        public BO.Station convertStationToListToStationBL(StationToList stationToList);
        public BO.Parcel convertParcelToListToParcelBL(ParcelToList parcelToList);
        public BO.Customer convertCustomerToListToCustomerlBL(CustomerToList customerToList);
        public BO.Station convertDalStationToBl(DO.Station s);
        public BO.Parcel convertDalToParcelBL(DO.Parcel p);


        //##############################################################
        //update functions
        //##############################################################
        public BO.Station updateDataStation(int id, int name = 0, int chargeSlots = -1);
        public BO.Drone updateSendDroneToCharge(int id);
        public BO.Drone updateUnchargeDrone(int id, double timeInCharge);
        public void updateConnectParcelToDrone(int id);
        public void updateDataCustomer(ulong id, string name = null, string phone = null);
        public void updateCollectParcelByDrone(int id);
        public void updateSupplyParcelByDrone(int id);
        public void updateDrone(BO.Drone drone);
        public BO.Drone updateDataDroneModel(int id, string model);


        //##############################################################
        //remove functions
        //##############################################################
        public void removeParcel(int id);
        public void removeStation(int id);


        //##############################################################
        //check functions
        //##############################################################
        public void checkUniqeIDCustomer(ulong id);
        public void checkIfCustomerWithThisID(ulong id);
        public bool checkStationIfEmptyChargers(DO.Station station);


        //##############################################################
        //help functions
        //##############################################################
        public double calcElectry(LocationBL locatin1, LocationBL location2, int weight);
        public DO.Station stationWithMinDisAndEmptySlots(LocationBL location);
        public DO.Station findClosestStation(LocationBL location);
        public ParcelStatus findParcelStatus(BO.Parcel parcel);














        //public static void checkUniqeIdCustomer(ulong id, IDAL.IDal dalObject);
        //public static void checkUniqeIdParcel(int id, IDAL.IDal dalObject);
        //public static void checkUniqeIdStation(int id, IDAL.IDal dalObject);
        // public List<BO.Station> getStationWithEmptyChargers();
        // public static void checkUniqeIdDrone(int id, IDAL.IDal dalObject);
        //public List<DroneToList> getDronesByDroneStatus(int status);
        //public List<DroneToList> getDronesByDroneWeight(int status);
        //public List<ParcelToList> getParcelsByPriority(int status);
        //public List<ParcelToList> getParcelsByparcelWeight(int status);

    }
}