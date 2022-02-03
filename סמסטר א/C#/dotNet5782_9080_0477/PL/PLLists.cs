using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;
namespace PL
{
    public class PLLists : DependencyObject
    {
        private BlApi.IBL BLObject = BL.FactoryBL.factory();

        public ObservableCollection<Drone_> Drones = new ObservableCollection<Drone_>();

        public ObservableCollection<Parcel_> Parcels = new ObservableCollection<Parcel_>();

        public ObservableCollection<Station_> Stations = new ObservableCollection<Station_>();

        public ObservableCollection<Customer_> Customers = new ObservableCollection<Customer_>();

        public void AddDrone(DroneToList bl)
        {
            Drones.Add(new Drone_(bl));
        }

        public void AddParcel(ParcelToList bl)
        {
            Parcels.Add(new Parcel_(bl));
        }

        public void AddStation(StationToList bl)
        {
            Stations.Add(new Station_(bl));
        }

        public void AddCustomer(BO.Customer bl)
        {
            Customers.Add(new Customer_(bl));
        }



        public void UpdateDrone(BO.Drone drone)
        {
            int index = Drones.IndexOf(Drones.Where(D => D.ID == drone.ID).FirstOrDefault());
            Drones[index] = new Drone_(BLObject.ConvertDroneBLToDroneToList(drone));
        }

        public void UpdateParcel(Parcel_ parcel)
        {
            int index = Parcels.IndexOf(Parcels.Where(P => P.ID == parcel.ID).FirstOrDefault());
            Parcels[index] = parcel;
        }

        public void UpdateStation(Station_ station)
        {
            int index = Stations.IndexOf(Stations.Where(S => S.ID == station.ID).FirstOrDefault());
            Stations[index] = station;
        }

        public void UpdateCustomer(Customer_ customer)
        {
            int index = Customers.IndexOf(Customers.Where(C => C.ID == customer.ID).FirstOrDefault());
            Customers[index] = customer;
        }

        public PLLists()
        {
            foreach (var drone in BLObject.GetDronesToList())
            {
                AddDrone(drone);
            }

            foreach (var parcel in BLObject.GetParcelsToList())
            {
                AddParcel(parcel);
            }

            foreach (var station in BLObject.GetStationsToList())
            {
                AddStation(station);
            }

            foreach (var customer in BLObject.GetCustomersByCondition(C=>C.ID != 0))
            {
                AddCustomer(customer);
            }
        }
    }
}
