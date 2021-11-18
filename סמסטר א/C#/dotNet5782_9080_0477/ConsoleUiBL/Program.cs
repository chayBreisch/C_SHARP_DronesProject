using System;
using IBL.BO;
using BL;
using DalObject;
using System.Collections.Generic;


namespace ConsoleUI_BL
{
    class Program
    {


        static void Main(string[] args)
        {
            try
            {
                BL.BL bL = new BL.BL();
                int choices;
                do
                {
                    Console.WriteLine("to add enter 1");
                    Console.WriteLine("to update enter 2");
                    Console.WriteLine("to display enter 3");
                    Console.WriteLine("to display lists enter 4");
                    Console.WriteLine("to exit enter 5");
                    choices = Convert.ToInt32(Console.ReadLine());
                    switch (choices)
                    {
                        case 1:
                            Console.WriteLine("to add a station enter 1");
                            Console.WriteLine("to add a drone enter 2");
                            Console.WriteLine("to add a customer enter 3");
                            Console.WriteLine("to add a parcel enter 4");
                            int choice = Convert.ToInt32(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("enter station number");
                                    int Id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter station name");
                                    int Name = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter number of free charge slots");
                                    int ChargeSlots = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter the longitude");
                                    int longitude = Convert.ToInt32(Console.ReadLine());
                                    double Longitude = (double)longitude;
                                    Console.WriteLine("enter the latitude");
                                    int latitude = Convert.ToInt32(Console.ReadLine());
                                    double Latitude = (double)latitude;
                                    LocationBL location = new LocationBL(Longitude, Latitude);

                                    bL.addStation(Id, Name, location, ChargeSlots);

                                    break;
                                case 2:
                                    Console.WriteLine("enter id");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter the maximal weight : 1. Light,2. Medium, 3.Heavy");
                                    int weight = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter the model");
                                    string Model = Console.ReadLine();
                                    Console.WriteLine("number of station for start charging");
                                    int number = Convert.ToInt32(Console.ReadLine());

                                    bL.addDrone(id, Model, weight, number);

                                    break;

                                case 3:
                                    Console.WriteLine("enter id");
                                    ulong IdCustomer = ulong.Parse(Console.ReadLine());
                                    Console.WriteLine("enter name");
                                    string NameCustomer = Console.ReadLine();
                                    Console.WriteLine("enter cellPhone");
                                    string Phone = Console.ReadLine();
                                    Console.WriteLine("enter the Longitude");
                                    double longitude2 = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("enter the Latitude");
                                    double latitude2 = Convert.ToDouble(Console.ReadLine());
                                    LocationBL location1 = new LocationBL(longitude2, latitude2);

                                    bL.AddCustomer(IdCustomer, NameCustomer, Phone, location1);

                                    break;
                                case 4:
                                    Console.WriteLine("enter id of sender");
                                    ulong SenderId = ulong.Parse(Console.ReadLine());
                                    Console.WriteLine("enter id of reciver");
                                    ulong RecieverId = ulong.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the weight : 1. Light,2. Medium, 3.Heavy");
                                    weight = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter the prionity : 1. Reguler,2. Fast, 3.Emergency");
                                    int priority = Convert.ToInt32(Console.ReadLine());
                                    bL.AddParcel(SenderId, RecieverId, weight, priority);
                                    break;
                                default:
                                    Console.WriteLine("please enter a number btween 1-4");
                                    break;
                            }
                            break;
                        case 2:
                            Console.WriteLine("to udate Model of drone enter 1");
                            Console.WriteLine("to update data of station enter 2");
                            Console.WriteLine("to update data of customer enter 3");
                            Console.WriteLine("to send a drone to charge in a station enter 4");
                            //Console.WriteLine("to Release a skimmer from charging at a base station enter 5 ");
                            choice = Convert.ToInt32(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("enter id");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter new Model");
                                    string name = Console.ReadLine();
                                    bL.updateDataDroneModel(id, name);
                                    break;
                                case 2:
                                    Console.WriteLine("enter id");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter name");
                                    int nameOfStation = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("enter number of charge slots");
                                    int chargeSlots = Convert.ToInt32(Console.ReadLine());
                                    bL.updateDataStation(id, nameOfStation, chargeSlots);
                                    break;
                                case 3:
                                    Console.WriteLine("enter id");
                                    ulong IdCustomer = ulong.Parse(Console.ReadLine());
                                    Console.WriteLine("enter name");
                                    name = Console.ReadLine();
                                    Console.WriteLine("enter phone");
                                    string phone = Console.ReadLine();
                                    bL.updateDataCustomer(IdCustomer, name, phone);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        /*case 3:
                            Console.WriteLine("to display a station enter 1");
                            Console.WriteLine("to display a drone enter 2");
                            Console.WriteLine("to display a customer enter 3");
                            Console.WriteLine("to display a parcel enter 4");
                            choice = Convert.ToInt32(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    print<Station>(dalObject.findStation(getStaionId()));
                                    break;
                                case 2:
                                    print<Drone>(dalObject.findDrone(getDroeId()));
                                    break;
                                case 3:
                                    print<Customer>(dalObject.findCustomer(getCustomerId()));
                                    break;
                                case 4:
                                    DisplayObj<Parcel>(dalObject.findParcel(getParcleId()));
                                    break;
                                default:
                                    break;
                            }

                            break;*/
                        case 4:
                            Console.WriteLine("to display the stations list enter 1");
                            Console.WriteLine("to display the drones list enter 2");
                            Console.WriteLine("to display the customers list enter 3");
                            Console.WriteLine("to display the parcels list enter 4");
                            Console.WriteLine("to display the list of parcels that are free enter 5");
                            Console.WriteLine("to display the list of station that have  free chargers enter 6");
                            choice = Convert.ToInt32(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    print<StationBL>(bL.GetStationsBL());
                                    break;
                                case 2:
                                    print<DroneBL>(bL.GetDronesBL());
                                    break;
                                case 3:
                                    print<CustomerBL>(bL.GetCustomersBL());
                                    break;
                                case 4:
                                    print<ParcelBL>(bL.GetParcelsBL());
                                    break;
                                case 5:
                                    //DisplayNotBelongedParcels(dalObject.GetParcel());
                                    break;
                                case 6:
                                    //displayStationsWithEmptyChargingSlots(dalObject.GetStations(), dalObject.GetDroneCharges());
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case 5:
                            break;
                        case 6:
                            break;

                        default:
                            Console.WriteLine("input not valid");
                            break;
                    }




                } while (choices != 5);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public static void print<T>(List<T> array)
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }
  


















/*namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BL.BL bL = new BL.BL();
            DalObject.DataSource.Initialize();
            int choice = 0;
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
                        optionAdd();
                        break;
                    case 2:
                        updateOption();
                        break;
                    case 3:
                        showOneItem();
                        break;
                    case 4:
                        showAllItems();
                        break;
                    case 5:
                        Console.WriteLine("you wanted to exit.....\nBye Bye");
                        break;
                    default:
                        Console.WriteLine("not a good choice try again");
                        break;
                }
            }
        }*/
        public static void showParcelsWithoutoutDrone()
        {
            BL.BL bl = new BL.BL();
            List<ParcelBL> parcels = bl.GetParcelsBL();
            foreach (var parcel in parcels)
            {
                if (parcel.ID == 0)
                {
                    Console.WriteLine(parcel.ToString());
                    Console.WriteLine(parcel);
                }
            }
        }
       /* public static void showStationWithEmptyChargers()
        {
            BL.BL bl = new BL.BL();
            int numOfChargers = 0;
            List<StationBL> stations = bl.GetStationBL();
            //List< DroneCharge> droneChargers = bl.GetDroneCharge();
            for (int i = 0; i < stations.Length; i++)
            {
                for (int j = 0; j < droneChargers.Length; j++)
                {
                    if (stations[i].ID == droneChargers[j].StationID)
                        numOfChargers++;
                }
                if (numOfChargers < stations[i].ChargeSlots)
                {
                    Console.WriteLine(stations[i].ToString());
                    Console.WriteLine(stations[i]);
                }

            }
        }*/
    }
}/*
          
       
        public static void optionAdd()
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
                    Console.WriteLine("enter station number");
                    int Id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("enter station name");
                    int Name = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("enter number of free charge slots");
                    int ChargeSlots = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("enter the longitude");
                    int longitude = Convert.ToInt32(Console.ReadLine());
                    double Longitude = (double)longitude;
                    Console.WriteLine("enter the latitude");
                    int latitude = Convert.ToInt32(Console.ReadLine());
                    double Latitude = (double)latitude;
                    LocationBL location = new LocationBL(Longitude, Latitude);

                    bL.addStationToBL(Id, Name, location, ChargeSlots);

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
                    DalObject.DalObject.AddDrone(id, model, maxWeight, status, battery);
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
                    DalObject.DalObject.AddCustomer(id, Name, phone, Latitude, Longitude);
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
                    DalObject.DalObject.AddParcial(id, senderID, targetID, weight, priority);
                    break;
                default:
                    break;
            }
        }
        public static void updateOption()
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
                    DalObject.DalObject.updateConectDroneToParcial(id);
                    break;
                case 2:
                    Console.WriteLine("Enter id of Parcial to collect: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    DalObject.DalObject.updateCollectParcialByDrone(id);
                    break;
                case 3:
                    Console.WriteLine("Enter id of Parcial to supply: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    DalObject.DalObject.updateSupplyParcialToCustomer(id);
                    break;
                case 4:
                    showStationWithEmptyChargers();
                    Console.WriteLine("Enter id of drone to charge: ");
                    int droneId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter id of station to charge: ");
                    int stationId = Convert.ToInt32(Console.ReadLine());
                    DalObject.DalObject.updateSendDroneToCharge(droneId, stationId);
                    break;
                case 5:
                    Console.WriteLine("Enter id of drone to uncharge: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    DalObject.DalObject.updateUnChargeDrone(id);
                    break;
                default:
                    break;
            }
        }
        public static void showOneItem()
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
                    Station station = DalObject.DalObject.GetSpecificStation(id);
                    Console.WriteLine(station.ToString());
                    Console.WriteLine(station);

                    break;
                case 2:
                    Console.WriteLine("Enter an id of the drone");
                    id = Convert.ToInt32(Console.ReadLine());
                    Drone drone = DalObject.DalObject.GetSpecificDrone(id);
                    Console.WriteLine(drone.ToString());
                    Console.WriteLine(drone);
                    break;
                case 3:
                    Console.WriteLine("Enter an id of a customer: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Customer customer = DalObject.DalObject.GetSpecificCustomer(id);
                    Console.WriteLine(customer.ToString());
                    Console.WriteLine(customer);
                    break;
                case 4:
                    Console.WriteLine("Enter an id of a parcial: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Parcial parcial = DalObject.DalObject.GetSpecificParcial(id);
                    Console.WriteLine(parcial.ToString());
                    Console.WriteLine(parcial);
                    break;
                default:
                    break;
            }
        }
        public static void showAllItems()
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

                    Station[] stations = DalObject.DalObject.GetStation();
                    foreach (var station in stations)
                    {
                        Console.WriteLine(station.ToString());
                    }
                    print(stations);
                    break;
                case 2:
                    Drone[] drones = DalObject.DalObject.GetDrone();
                    foreach (var drone in drones)
                    {
                        Console.WriteLine(drone.ToString());
                    }
                    print(drones);
                    break;
                case 3:
                    Customer[] customers = DalObject.DalObject.GetCustomer();
                    foreach (var customer in customers)
                    {
                        Console.WriteLine(customer.ToString());
                    }
                    print(customers);
                    break;
                case 4:
                    Parcial[] parcials = DalObject.DalObject.GetParcial();
                    foreach (var parcial in parcials)
                    {
                        Console.WriteLine(parcial.ToString());
                    }
                    print(parcials);
                    break;
                case 5:
                    showParcelsWithoutoutDrone();
                    break;
                case 6:
                    showStationWithEmptyChargers();
                    break;
                default:
                    break;
            }
        }
        public static void print<T>(T[] array)
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }
    }
}*/