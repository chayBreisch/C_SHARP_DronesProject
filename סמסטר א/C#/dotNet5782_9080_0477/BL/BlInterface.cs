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

        public IDAL.DO.Station stationWithMinDisAndEmptySlots(LocationBL location);

        public IDAL.DO.Station findClosestStation(LocationBL location);

        //public static void checkUniqeIdCustomer(ulong id, IDAL.IDal dalObject);

        public void checkUniqeIDCustomer(ulong id);
        public void AddCustomer(ulong id, string name, string phone, LocationBL location);

        public void AddCustomerToDal(ulong id, string name, string phone, LocationBL location);

        public List<BO.Customer> GetCustomersBL();

        public BO.Customer GetSpecificCustomerBL(ulong id);
        public BO.Customer convertDalCustomerToBl(IDAL.DO.Customer c);

        public void updateDataCustomer(ulong id, string name = null, string phone = null);
        public void checkIfCustomerWithThisID(ulong id);

        //public static void checkUniqeIdParcel(int id, IDAL.IDal dalObject);
        public void AddParcel(ulong sender, ulong target, int Weight, int priority);

        public void AddParcelToDal(/*ulong id,*/ ulong sender, ulong target, int Weight, int priority);
        public List<BO.Parcel> GetParcelsBL();
        public BO.Parcel GetSpecificParcelBL(int id);

        public List<IDAL.DO.Parcel> getParcelsWithoutoutDrone();
        public BO.Parcel convertDalToParcelBL(IDAL.DO.Parcel p);

        //public ParcelStatus findParcelStatus(IDAL.DO.Parcel parcel);///////////////////////////
        //public static void checkUniqeIdStation(int id, IDAL.IDal dalObject);

        public void addStation(int id, int name, LocationBL location, int ChargeSlots);

        public void AddStationToDal(int id, int name, LocationBL location, int ChargeSlots);
        public List<BO.Station> GetStationsBL();
        public BO.Station GetSpecificStationBL(int id);

        public List<BO.Station> getStationWithEmptyChargers();

        public bool checkStationIfEmptyChargers(IDAL.DO.Station station);
        public BO.Station convertDalStationToBl(IDAL.DO.Station s);////////////////
        public void updateDataStation(int id, int name = 0, int chargeSlots = -1);

        public IBL.BO.Drone updateSendDroneToCharge(int id);

        public IBL.BO.Drone updateUnchargeDrone(int id, double timeInCharge);

        public void updateConnectParcelToDrone(int id);

        public void updateCollectParcelByDrone(int id);

        public void updateSupplyParcelByDrone(int id);
        // public static void checkUniqeIdDrone(int id, IDAL.IDal dalObject);

        public void addDrone(int id, string model, int maxWeight, int stationID);

        public void AddDroneToDal(int id, string model, int maxWeight);

        public List<BO.Drone> GetDronesBL();
        public BO.Drone getSpecificDroneBLFromList(int id);

        public BO.Drone GetSpecificDroneBL(int id);

        public BO.Drone convertDalDroneToBl(IDAL.DO.Drone d);

        public void updateDrone(BO.Drone drone);

        public IBL.BO.Drone updateDataDroneModel(int id, string model);

        public List<BO.Parcel> GetParcelsWithoutoutDrone();

        public List<BO.Station> GetStationWithEmptyChargers();

        public List<DroneToList> getDronesByDroneStatus(int status);
        public List<DroneToList> getDronesByDroneWeight(int status);
        public List<DroneToList> getDroneToList();
        public IBL.BO.Drone convertDroneToListToDroneBL(DroneToList droneToList);
    }
}
