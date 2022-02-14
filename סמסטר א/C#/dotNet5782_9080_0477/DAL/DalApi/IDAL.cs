using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int AddParcel(Parcel parcel);
        public void AddDroneCharge(DroneCharge droneCharge);
  

        //##########################################################
        //get list functions
        //##########################################################
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate = null);
        public IEnumerable<Station> GetStations(Predicate<Station> predicate = null);
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate = null);
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate = null);
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
    }
}
