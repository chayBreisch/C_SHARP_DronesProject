using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    class PLLists : DependencyObject
    {
        private BlApi.Bl BLObject = BL.FactoryBL.factory();
        public ObservableCollection<Drone_> Drones = new ObservableCollection<Drone_>();

        public ObservableCollection<Parcel_> Parcels = new ObservableCollection<Parcel_>();

        public ObservableCollection<Station_> Stations = new ObservableCollection<Station_>();

        public ObservableCollection<Customer_> Customers = new ObservableCollection<Customer_>();

        public void AddDrone(BO.DroneToList bl)
        {
            Drones.Add(new Drone_(bl));
        }

        public void AddParcel(BO.ParcelToList bl)
        {
            Parcels.Add(new Parcel_(bl));
        }

        public void AddStation(BO.StationToList bl)
        {
            Stations.Add(new Station_(bl));
        }

        public void AddCustomer(BO.CustomerToList bl)
        {
            Customers.Add(new Customer_(bl));
        }

        public PLLists()
        {
            foreach (var drone in BLObject.GetDronesToList())
            {
                AddDrone(drone);
            }

            foreach (var parcel in BLObject.GetParcelToList())
            {
                AddParcel(parcel);
            }

            foreach (var station in BLObject.GetStationToList())
            {
                AddStation(station);
            }

            foreach (var customer in BLObject.GetCustomerToList())
            {
                AddCustomer(customer);
            }


        }

    }
}
