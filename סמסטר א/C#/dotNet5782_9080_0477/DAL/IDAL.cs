using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;
namespace IDAL
{
    interface IDal
    {
        public IEnumerable<Drone> GetDrone();
        public IEnumerable<Station> GetStation();
        public IEnumerable<Customer> GetCustomer();
        public IEnumerable<Parcial> GetParcial();
        public IEnumerable<DroneCharge> GetDroneCharge();
        public Drone GetSpecificDrone(int id);
        public Station GetSpecificStation(int id);
        public Customer GetSpecificCustomer(int id);
        public Parcial GetSpecificParcial(int id);
        public void AddDrone(int id, string model, WeightCatagories weight, DroneStatus status, double battery);
        public void AddStation(int id, int name, int longitude, int latitude, int chargeSlots);
        public void AddCustomer(int id, string name, string phone, double latitude, double longitude);
        public void AddParcial(int id, int senderId, int targetId, WeightCatagories weight, Priorities priority);
        public void updateConectDroneToParcial(int id);
        public void updateCollectParcialByDrone(int id);
        public void updateSupplyParcialToCustomer(int id);
        public void updateSendDroneToCharge(int droneId, int statoinId);
        public void updateUnChargeDrone(int id);
        public IEnumerable<Parcial> showParcelsWithoutoutDrone();
        public IEnumerable<Station> showStationWithEmptyChargers();















    }
}
