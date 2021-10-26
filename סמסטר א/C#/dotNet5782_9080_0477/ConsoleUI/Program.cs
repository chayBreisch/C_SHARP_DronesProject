using System;
using IDAL.DO;
using DalObject;
using System.Collections.Generic;

namespace ConsoleUI
{
    
    class Program
    {
        static void Main(string[] args)
        {
            DalObject.DalObject dalObject = new DalObject.DalObject();

/*            DalObject.DataSource.Initialize();
*/            int choice = 0;

            while (choice != 5)
            {
                Console.WriteLine("to add enter 1:" +
                    "\nto update enter 2: " +
                    "\nto show a specific one enter 3: " +
                    "\nto show list enter 4: " +
                    "\nto exit enter 5:  ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {

                    case 1:
                        optionAdd(dalObject);
                        break;
                    case 2:
                        updateOption(dalObject);
                        break;
                    case 3:
                        showOneItem(dalObject);
                        break;
                    case 4:
                        showAllItems(dalObject);
                        break;
                    case 5:
                        Console.WriteLine("you wanted to exit.....\nBye Bye");
                        break;
                    default:
                        Console.WriteLine("not a good choice try again");
                        break;
                }
            }
        }


        public static void showParcelsWithoutoutDrone(DalObject.DalObject dalObject)
        {
            IEnumerable <Parcial> parcels = dalObject.GetParcial();
            foreach (var parcel in parcels)
            {
                 Console.WriteLine(parcel);
            }
        }


        public static void showStationWithEmptyChargers(DalObject.DalObject dalObject)
        {
            IEnumerable<Station> stations = dalObject.GetStation();
            foreach (var station in stations)
            {
                Console.WriteLine(station);
            }
        }


        public static void optionAdd(DalObject.DalObject dalObject)
        {
            Console.WriteLine("to add a station enter 1: " +
                "\nto add a drone enter 2: " +
                "\nto add a customer enter 3: " +
                "\nto add a parcial enter 4: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            int id = 0, name = 0, longitude = 0, latiude = 0;
            switch (choice)
            {

                case 1:
                    Console.WriteLine("Enter an id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter name: ");
                    name = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter longitude: ");
                    longitude = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter latiude: ");
                    latiude = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter chargeslots: ");
                    int chargeslots = Convert.ToInt32(Console.ReadLine());
                    dalObject.AddStation(id, name, longitude, latiude, chargeslots);
                    break;
                case 2:
                    Console.WriteLine("Enter an id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter name: ");
                    string model = Console.ReadLine();
                    Console.WriteLine("Enter maxWeight: ");
                    string mySring = Console.ReadLine();
                    WeightCatagories maxWeight = (WeightCatagories)Enum.Parse(typeof(WeightCatagories), mySring);
                    Console.WriteLine("Enter status: ");
                    mySring = Console.ReadLine();
                    DroneStatus status = (DroneStatus)Enum.Parse(typeof(DroneStatus), mySring);
                    Console.WriteLine("Enter battery: ");
                    double battery = Convert.ToInt32(Console.ReadLine());
                    dalObject.AddDrone(id, model, maxWeight, status, battery);

                    break;
                case 3:

                    Console.WriteLine("Enter an id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter name: ");
                    string Name = Console.ReadLine();
                    Console.WriteLine("Enter a phone: ");
                    string phone = Console.ReadLine();
                    Console.WriteLine("Enter longitude: ");
                    double Longitude = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter latiude: ");
                    double Latitude = Convert.ToDouble(Console.ReadLine());
                    dalObject.AddCustomer(id, Name, phone, Latitude, Longitude);
                    break;
                case 4:

                    Console.WriteLine("Enter an id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter an sender id: ");
                    int senderID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter an target id: ");
                    int targetID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter an weight: ");
                    string str = Console.ReadLine();
                    WeightCatagories weight = (WeightCatagories)Enum.Parse(typeof(WeightCatagories), str);
                    Console.WriteLine("Enter priority: ");
                    str = Console.ReadLine();
                    Priorities priority = (Priorities)Enum.Parse(typeof(Priorities), str);
                    dalObject.AddParcial(id, senderID, targetID, weight, priority);
                    break;
                default:
                    break;

            }
        }



        public static void updateOption(DalObject.DalObject dalObject)
        {
            Console.WriteLine("to conect a parcial to a drone enter 1: " +
                "\nto collect a parcial by a drone enter 2: " +
                "\nto supply a parcial to a customer enter 3: " +
                "\nto send a drone to charge in a station enter 4: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            int id = 0;

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter id of Parcial to connect: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    dalObject.updateConectDroneToParcial(id);
                    break;
                case 2:
                    Console.WriteLine("Enter id of Parcial to collect: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    dalObject.updateCollectParcialByDrone(id);
                    break;
                case 3:
                    Console.WriteLine("Enter id of Parcial to supply: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    dalObject.updateSupplyParcialToCustomer(id);
                    break;
                case 4:
                    showStationWithEmptyChargers(dalObject);
                    Console.WriteLine("Enter id of drone to charge: ");
                    int droneId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter id of station to charge: ");
                    int stationId = Convert.ToInt32(Console.ReadLine());
                    dalObject.updateSendDroneToCharge(droneId, stationId);
                    break;
                case 5:
                    Console.WriteLine("Enter id of drone to uncharge: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    dalObject.updateUnChargeDrone(id);
                    break;
                default:
                    break;
            }
        }



        public static void showOneItem(DalObject.DalObject dalObject)
        {
            Console.WriteLine("to display a station enter 1: " +
                "\nto display a drone enter 2: " +
                "\nto display a customer enter 3: " +
                "\nto display a parcial enter 4: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            int id = 0;

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter an id of a station: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Station station = dalObject.GetSpecificStation(id);
                    Console.WriteLine(station);

                    break;
                case 2:
                    Console.WriteLine("Enter an id of the drone");
                    id = Convert.ToInt32(Console.ReadLine());
                    Drone drone = dalObject.GetSpecificDrone(id);
                    Console.WriteLine(drone);
                    break;
                case 3:
                    Console.WriteLine("Enter an id of a customer: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Customer customer = dalObject.GetSpecificCustomer(id);
                    Console.WriteLine(customer);
                    break;
                case 4:
                    Console.WriteLine("Enter an id of a parcial: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Parcial parcial = dalObject.GetSpecificParcial(id);
                    Console.WriteLine(parcial);
                    break;
                default:
                    break;
            }
        }



        public static void showAllItems(DalObject.DalObject dalObject)
        {
            Console.WriteLine("to display the station list enter 1: " +
                "\nto display the drone list enter 2: " +
                "\nto display the customer list enter 3: " +
                "\nto display the parcial list enter 4: " +
                "\nto display the list of parcials that are free enter 5: " +
                "\nto display the list of station that have free chargers enter 6: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    IEnumerable<Station> stations = dalObject.GetStation();
                    print(stations);
                    break;
                case 2:
                    IEnumerable<Drone> drones = dalObject.GetDrone();
                    print(drones);
                    break;
                case 3:
                    IEnumerable<Customer > customers = dalObject.GetCustomer();
                    print(customers);
                    break;
                case 4:
                    IEnumerable<Parcial > parcials = dalObject.GetParcial();
                    print(parcials);
                    break;
                case 5:
                    showParcelsWithoutoutDrone(dalObject);
                    break;
                case 6:
                    showStationWithEmptyChargers(dalObject);
                    break;
                default:
                    break;
            }
        }
        public static void print<T>(IEnumerable< T> array)
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }
    }
}
