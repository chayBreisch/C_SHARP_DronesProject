using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface Bl
    {
        public double calcElectry(LocationBL locatin1, LocationBL location2, int weight);

        public Station stationWithMinDisAndEmptySlots(LocationBL location);

        public Station findClosestStation(LocationBL location);

        //public static void checkUniqeIdCustomer(ulong id, IDAL.IDal dalObject);

        public void checkUniqeIDCustomer(ulong id);
        public void AddCustomer(ulong id, string name, string phone, LocationBL location);

        public void AddCustomerToDal(ulong id, string name, string phone, LocationBL location);

        public List<CustomerBL> GetCustomersBL();

        public CustomerBL GetSpecificCustomerBL(ulong id);
        public CustomerBL convertDalCustomerToBl(Customer c);

        public void updateDataCustomer(ulong id, string name = null, string phone = null);
        public void checkIfCustomerWithThisID(ulong id);

        //public static void checkUniqeIdParcel(int id, IDAL.IDal dalObject);
        public void AddParcel(ulong sender, ulong target, int Weight, int priority);

        public void AddParcelToDal(/*ulong id,*/ ulong sender, ulong target, int Weight, int priority);
        public List<ParcelBL> GetParcelsBL();
        public ParcelBL GetSpecificParcelBL(int id);

        public List<Parcel> getParcelsWithoutoutDrone();
        public ParcelBL convertDalToParcelBL(Parcel p);

        public ParcelStatus findParcelStatus(Parcel parcel);///////////////////////////
        //public static void checkUniqeIdStation(int id, IDAL.IDal dalObject);

        public void addStation(int id, int name, LocationBL location, int ChargeSlots);

        public void AddStationToDal(int id, int name, LocationBL location, int ChargeSlots);
        public List<StationBL> GetStationsBL();
        public StationBL GetSpecificStationBL(int id);

        public List<StationBL> getStationWithEmptyChargers();

        public bool checkStationIfEmptyChargers(Station station);
        public StationBL convertDalStationToBl(Station s);////////////////
        public void updateDataStation(int id, int name = 0, int chargeSlots = -1);

        public void updateSendDroneToCharge(int id);

        public void updateUnchargeDrone(int id, double timeInCharge);

        public void updateConnectParcelToDrone(int id);

        public void updateCollectParcelByDrone(int id);

        public void updateSupplyParcelByDrone(int id);
        // public static void checkUniqeIdDrone(int id, IDAL.IDal dalObject);

        public void addDrone(int id, string model, int maxWeight, int stationID);

        public void AddDroneToDal(int id, string model, int maxWeight);

        public List<DroneBL> GetDronesBL();
        public DroneBL getSpecificDroneBLFromList(int id);

        public DroneBL GetSpecificDroneBL(int id);

        public DroneBL convertDalDroneToBl(Drone d);

        public void updateDrone(DroneBL drone);

        public void updateDataDroneModel(int id, string model);

        public List<ParcelBL> GetParcelsWithoutoutDrone();

        public List<StationBL> GetStationWithEmptyChargers();

        public List<DroneBL> getDronesByDroneStatus(int status);
        public List<DroneBL> getDronesByDroneWeight(int status);

    }
}
