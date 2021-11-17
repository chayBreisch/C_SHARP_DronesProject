using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;
namespace IDAL
{
    public interface IDal
    {
        public IEnumerable<Drone> GetDrone();
        public IEnumerable<Station> GetStation();
        public IEnumerable<Customer> GetCustomer();

       /* public IEnumerable<Customer> GetCustomer();*/
        public IEnumerable<Parcel> GetParcel();
        public IEnumerable<DroneCharge> GetDroneCharge();
        public Drone GetSpecificDrone(int id);
        public Station GetSpecificStation(int id);
        public Customer GetSpecificCustomer(ulong id);
        public Parcel GetSpecificParcel(int id);
        public Parcel GetSpecificParcelByDroneID(int id);
        public Parcel GetParcelByDroneID(int id);
        //public void AddDrone(int id, string model, WeightCatagories weight, DroneStatus status, double battery);
        public DroneCharge getSpecificDroneChargeByStationID(int id);
        public DroneCharge getSpecificDroneChargeByDroneID(int id);
        public int getIndexOfDroneChargeByDroneID(int id);
        public int getIndexOfDroneChargeByStationID(int id);
        public void removeDroneCharge(DroneCharge droneCharge);
        public void AddDrone(Drone drone);
        public void AddStation(Station station);
        public void AddCustomer(Customer customer);
        public void AddParcel(Parcel parcel);
        public void AddDroneCharge(DroneCharge droneCharge);
        public void updateConectDroneToParcial(int id);
        public void updateCollectParcialByDrone(int id);
        public void updateSupplyParcialToCustomer(int id);
        public void updateSendDroneToCharge(int droneId, int statoinId);
        public void updateUnChargeDrone(int id);
        public IEnumerable<Parcel> showParcelsWithoutoutDrone();
        public IEnumerable<Station> showStationWithEmptyChargers();
        public double[] requestElectric();
        public List<Customer> GetCustomersByList();
        public List<Station> GetStationByList();
        public List<Drone> GetDronesByList();
        public List<DroneCharge> GetDroneChargeByList();
        public List<Parcel> GetParcelByList();
        public void updateDrone(Drone drone);
        public void updateStation(Station station);
        public void updateParcel(Parcel parcel);
        public void updateDroneCharge(DroneCharge droneCharge);
        public void updateCustomer(Customer customer);
        public int lengthStation();
        public int lengthParcel();

        public void CheckUniqestation(int id);
        public void CheckUniqeParcel(int id);
        public void checkUniqeIdDroneChargeBL(int droneId, int stationId);

        public void CheckUniqeDrone(int id);
        public void CheckUniqeCustomer(ulong id);

    }
}
