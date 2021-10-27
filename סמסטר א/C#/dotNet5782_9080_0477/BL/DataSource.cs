using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BlObject
{
    public struct DataSource
    {
        
        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcials =  new List<Parcel>();
        internal static List<DroneCharge> droneChargers = new List<DroneCharge>();

        public static void Initialize()
        {
            Random rand = new Random();
            int r = rand.Next(2,5);
            for (int i = 0; i < r; i++)
            {
                Station station = new Station(stations.Count + 1, stations.Count + 1, rand.Next(), rand.Next(), rand.Next(300));
                stations.Add(station);
            }

            r = rand.Next(5, 10);
            for (int i = 0; i < r; i++)
            {
                Drone drone = new Drone(drones.Count + 1, "MarvicAir2", WeightCatagories.Heavy + i, DroneStatus.Delivery + i, rand.Next(100));
                drones.Add(drone);
            }

            r = rand.Next(10, 15);
            for (int i = 0; i < r; i++)
            {
                Customer customer = new Customer(customers.Count + 1, $"customer{i}", $"{rand.Next(111111111, 999999999)}", rand.Next(), rand.Next());
                customers.Add(customer);
            }
            r = rand.Next(10, 15);
            for (int i = 0; i < r; i++)
            {
                Parcel parcel = new Parcel(parcials.Count + 1, rand.Next() % (parcials.Count + 1), rand.Next() % (parcials.Count + 1),
                    (WeightCatagories)(rand.Next() % 3), (Priorities)(rand.Next() % 3),
                    new DateTime(), rand.Next() % (parcials.Count + 1), new DateTime(), new DateTime(), new DateTime());
                parcials.Add(parcel);

            }
        }

    }
}
