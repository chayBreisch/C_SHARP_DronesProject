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
    class PLLists : DependencyObject
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

        public void AddCustomer(CustomerToList bl)
        {
            Customers.Add(new Customer_(bl));
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

            foreach (var customer in BLObject.GetCustomersToList())
            {
                AddCustomer(customer);
            }
        }
    }
}
