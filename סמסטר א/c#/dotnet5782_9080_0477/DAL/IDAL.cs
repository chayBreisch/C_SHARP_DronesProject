using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using DO;
namespace IDAL
{
    public interface IDal
    {
        //##########################################################
        //Add functions
        //##########################################################
        public void AddDrone(Drone drone);
        public void AddStation(Station station);
        public void AddCustomer(Customer customer);
        public void AddParcel(Parcel parcel);
        public void AddDroneCharge(DroneCharge droneCharge);


        //##########################################################
        //get list functions
        //##########################################################
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<Station> GetStations();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<DroneCharge> GetDroneCharges();
        public IEnumerable<Drone> getDronesByCondition(Predicate<Drone> predicate);
        public IEnumerable<Parcel> getParcelesByCondition(Predicate<Parcel> predicate);
        public IEnumerable<Station> getStationsByCondition(Predicate<Station> predicate);
        public IEnumerable<Customer> getCustomersByCondition(Predicate<Customer> predicate);
        public IEnumerable<DroneCharge> getDroneChargesByCondition(Predicate<DroneCharge> predicate);


        //#############################################################
        //get specific item functions
        //#############################################################
        public Drone getDroneById(Predicate<Drone> predicate);
        public Parcel getParcelById(Predicate<Parcel> predicate);
        public Station getStationById(Predicate<Station> predicate);
        public Customer getCustomerById(Predicate<Customer> predicate);
        public DroneCharge getDroneChargeById(Predicate<DroneCharge> predicate);


        //#############################################################
        //update functions
        //#############################################################
        public void updateDrone(Drone drone);
        public void updateStation(Station station);
        public void updateParcel(Parcel parcel);
        public void updateCustomer(Customer customer);


        //#############################################################
        //check functions
        //#############################################################
        public void CheckUniqestation(int id);
        public void CheckUniqeParcel(int id);
        public void checkUniqeIdDroneChargeBL(int droneId, int stationId);
        public void CheckUniqeDrone(int id);
        public void CheckUniqeCustomer(ulong id);


        //#############################################################
        //remove functions
        //#############################################################
        public void RemoveParcel(int idRemove);
        public void removeDroneCharge(DroneCharge droneCharge);


        //#############################################################
        //help functions
        //#############################################################
        public int getIndexOfDroneChargeByStationID(int id);
        public double[] requestElectric();
        public int lengthStation();
        public int lengthParcel();
        public int getIndexOfParcel(int id);
        public void RemoveStation(int idRemove);
        public int getIndexOfStation(int id);







        //public IEnumerable<Parcel> showParcelsWithoutoutDrone();
        //public IEnumerable<Station> showStationWithEmptyChargers();
        //public List<Drone> GetDronesByList();
        //public List<DroneCharge> GetDroneChargeByList();
        //public List<Parcel> GetParcelByList();
        //public void updateConectDroneToParcial(int id);
        //public void updateCollectParcialByDrone(int id);
        //public void updateSupplyParcialToCustomer(int id);
        //public void updateSendDroneToCharge(int droneId, int statoinId);
        //public void updateUnChargeDrone(int id);
        //public void updateDroneCharge(DroneCharge droneCharge);
    }
}
