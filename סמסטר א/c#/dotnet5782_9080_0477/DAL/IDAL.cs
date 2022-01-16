using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using DO;
namespace DalFacade
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
        public IEnumerable<Parcel> GetParcelesByCondition(Predicate<Parcel> predicate);
        public IEnumerable<Station> GetDeletedStations();
        public IEnumerable<Parcel> GetDeletedParcels();


        //#############################################################
        //get specific item functions
        //#############################################################
        public Drone GetDroneById(Predicate<Drone> predicate);
        public Parcel GetParcelBy(Predicate<Parcel> predicate);
        public Station GetStationById(Predicate<Station> predicate);
        public Customer GetCustomerById(Predicate<Customer> predicate);
        public DroneCharge GetDroneChargeById(Predicate<DroneCharge> predicate);


        //#############################################################
        //update functions
        //#############################################################
        public void UpdateDrone(Drone drone);
        public void UpdateCustomer(Customer customer);
        public void UpdateStation(Station station);
        public void UpdateParcel(Parcel parcel);


        //#############################################################
        //remove functions
        //#############################################################
        public void RemoveParcel(int idRemove);
        public void RemoveDroneCharge(DroneCharge droneCharge);
        public void RemoveStation(int idRemove);
        public void RemoveCustomer(ulong idRemove);
        public void RemoveDrone(int idRemove);


        //#############################################################
        //help functions
        //#############################################################
        public double[] RequestElectric();
        public int LengthStation();
        public int LengthParcel();









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
        //public IEnumerable<Drone> getDronesByCondition(Predicate<Drone> predicate);
        //public IEnumerable<Station> getStationsByCondition(Predicate<Station> predicate);
        //public IEnumerable<Customer> getCustomersByCondition(Predicate<Customer> predicate);
        //public IEnumerable<DroneCharge> getDroneChargesByCondition(Predicate<DroneCharge> predicate);
        //public void updateCustomer(Customer customer);
    }
}
