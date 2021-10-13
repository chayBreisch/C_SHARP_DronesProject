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
        internal class Config
        {
            static internal int DronesIndex = 0;
            static internal int StationIndex = 0;
            static internal int CustomerIndex = 0;
            static internal int ParcialIndex = 0;
        }
        internal static Drone[] Drones = new Drone[10];
        internal static Station[] Stations = new Station[5];
        internal static Customer[] Customers = new Customer[100];
        internal static Parcial[] Parcials = new Parcial[1000];

        public static void Initialize()
        {
            Random r = new Random();
            int rInt = r.Next(2, 100);
            //Console.WriteLine($"Enter {rInt} Stations");



            for (int i = 0; i < rInt; i++)
            {
                Stations[i].ID = Config.StationIndex + 1;
                Stations[i].Name = $"station{i}";
                Stations[i].Longitude = i;
                Stations[i].Latitude = i;
                Stations[i].ChargeSlots = i;
                Config.StationIndex++;
            }


        //Console.WriteLine($"Enter {rInt} drones");
        rInt = r.Next(5, 100);
            for (int i = 0; i < rInt; i++)
            {
                
                Drones[i].ID = Config.StationIndex + 1;
                Drones[i].Model = $"station{i}";
                /*Drones[i].MaxWeight = i;
                Drones[i].Status = i;*/
                Drones[i].Battery = i;
                Drones[i].ID = Convert.ToInt32(Console.ReadLine());
                Config.DronesIndex++;
            }

            //Console.WriteLine($"Enter {rInt} customer");
            rInt = r.Next(10, 100);
            for (int i = 0; i < rInt; i++)
            {
                Customers[i].ID = Convert.ToInt32(Console.ReadLine());
                Config.CustomerIndex++;
            }

            //Console.WriteLine($"Enter {rInt} parcials");
            rInt = r.Next(10, 100);
            for (int i = 0; i < rInt; i++)
            {
                Parcials[i].ID = Convert.ToInt32(Console.ReadLine());
                Config.ParcialIndex++;
            }
        }
       
    }
}
