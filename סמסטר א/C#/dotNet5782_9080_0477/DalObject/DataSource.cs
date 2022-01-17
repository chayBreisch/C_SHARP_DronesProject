using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{

    internal struct DataSource
    {
        public struct Config
        {
            public static double Available = 0.1;
            public static double LightHeight = 0.2;
            public static double MidHeight = 0.3;
            public static double HeavyHeight = 0.4;
            public static double ChargingRate = 0.5;
        }
        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneChargers = new List<DroneCharge>();
        public static DateTime randomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
        public static void Initialize()
        {
            Random rand = new Random();
            int r = rand.Next(2, 5);
            for (int i = 0; i < r; i++)
            {
                Station station = new Station();
                station.ID = stations.Count + 1;
                station.Name = stations.Count + 1;
                station.Latitude = rand.Next() % 10;
                station.Longitude = rand.Next() % 10;
                station.ChargeSlots = rand.Next(300);
                station.IsActive = true;
                stations.Add(station);
            }

            r = rand.Next(5, 10);
            for (int i = 0; i < r; i++)
            {
                Drone drone = new Drone();
                drone.ID = drones.Count + 1;
                drone.Model = "Hello World";
                drone.MaxWeight = (WeightCatagories)((i % 3) + 1);
                drone.IsActive = true;
                drones.Add(drone);
            }

            r = rand.Next(10, 15);
            for (int i = 0; i < r; i++)
            {
                Customer customer = new Customer();
                customer.ID = (ulong)rand.Next(111111111, 999999999);
                customer.Name = $"customer{i}";
                customer.Phone = $"{rand.Next(111111111, 999999999)}";
                customer.Latitude = rand.Next() % 36;
                customer.Longitude = rand.Next() % 36;
                customer.IsActive = true;
                customers.Add(customer);
            }
            r = rand.Next(10, 15);
            for (int i = 0; i < r; i++)
            {
                Parcel parcel = new Parcel();
                parcel.ID = parcels.Count + 1;
                parcel.SenderID = customers[rand.Next(0, customers.Count)].ID;
                parcel.TargetID = customers[rand.Next(0, customers.Count)].ID;
                parcel.Weight = (WeightCatagories)(rand.Next() % 3) + 1;
                parcel.Priority = (Priorities)(rand.Next() % 3);
                parcel.Requested = randomDay();
                parcel.Scheduled = randomDay();
                if (parcel.Scheduled < parcel.Requested)
                    parcel.Scheduled = null;
                else
                {
                    parcel.PickedUp = randomDay();
                    if (parcel.PickedUp < parcel.Scheduled)
                        parcel.PickedUp = null;
                    else
                    {
                        parcel.Delivered = randomDay();
                        if (parcel.Delivered < parcel.PickedUp)
                            parcel.Delivered = null;
                    }
                }
                parcel.IsActive = true;
                if (parcel.Scheduled != null)
                    parcel.DroneID = rand.Next() % (drones.Count) + 1;
                parcels.Add(parcel);
            }
        }
    }
}
