using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        internal static Drone[] Drones = new Drone[10];
        internal static Station[] Stations = new Station[5];
        internal static Customer[] Customers = new Customer[100];
        internal static Parcial[] Parcials = new Parcial[1000];

        public static void Initialize()
        {
            Random r = new Random();
            int rInt = r.Next(2, 100);
            Console.WriteLine($"Enter {rInt} Stations");

            for (int i = 0; i < rInt; i++)
            {
                Stations[i].ID = Convert.ToInt32(Console.ReadLine());
                Config.StationIndex++;
            }

            Console.WriteLine($"Enter {rInt} drones");
            rInt = r.Next(5, 100);
            for (int i = 0; i < rInt; i++)
            {
                Drones[i].ID = Convert.ToInt32(Console.ReadLine());
                Config.DronesIndex++;
            }

            Console.WriteLine($"Enter {rInt} customer");
            rInt = r.Next(10, 100);
            for (int i = 0; i < rInt; i++)
            {
                Customers[i].ID = Convert.ToInt32(Console.ReadLine());
                Config.CustomerIndex++;
            }

            Console.WriteLine($"Enter {rInt} parcials");
            rInt = r.Next(10, 100);
            for (int i = 0; i < rInt; i++)
            {
                Parcials[i].ID = Convert.ToInt32(Console.ReadLine());
                Config.ParcialIndex++;
            }
        }
        internal class Config
        {
            internal static int DronesIndex = 0;
            internal static int StationIndex = 0;
            internal static int CustomerIndex = 0;
            internal static int ParcialIndex = 0;
        }
    }
}
