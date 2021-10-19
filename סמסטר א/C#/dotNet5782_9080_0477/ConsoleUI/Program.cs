using System;
using IDAL.DO;
using DalObject;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DalObject.DataSource.Initialize();
            int choice = 0;
            
            while (choice != 5)
            {
                Console.WriteLine("to add enter 1: ");
                Console.WriteLine("to update enter 2: ");
                Console.WriteLine("to show a specific one enter 3: ");
                Console.WriteLine("to show list enter 4: ");
                Console.WriteLine("to exit enter 5: ");
                choice = Convert.ToInt32(Console.ReadLine());
                
                switch (choice)
                {

                    case 1:
                        
                        Console.WriteLine("to add a station enter 1: ");
                        Console.WriteLine("to add a drone enter 2: ");
                        Console.WriteLine("to add a customer enter 3: ");
                        Console.WriteLine("to add a parcial enter 4: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        int id = 0, name = 0, longitude= 0, latiude = 0;
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
                                DalObject.DalObject.AddStation(id, name, longitude, latiude, chargeslots); break;
                            case 2:
                                Console.WriteLine("Enter an id: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter name: ");
                                string model = Console.ReadLine();

                                //DroneStatus status =  DroneStatus.Available;
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
                        break;


                    case 2:
                        Console.WriteLine("to conect a parcial to a drone enter 1: ");
                        Console.WriteLine("to collect a parcial by a drone enter 2: ");
                        Console.WriteLine("to supply a parcial to a customer enter 3: ");
                        Console.WriteLine("to send a drone to charge in a station enter 4: ");
                        choice = Convert.ToInt32(Console.ReadLine());
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
                        break;


                    case 3:
                        Console.WriteLine("to display a station enter 1: ");
                        Console.WriteLine("to display a drone enter 2: ");
                        Console.WriteLine("to display a customer enter 3: ");
                        Console.WriteLine("to display a parcial enter 4: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("Enter an id of a station: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                Station station = DalObject.DalObject.GetSpecificStation(id);
                                Console.WriteLine($"id: {station.ID} Name: {station.Name} Longitude: {station.Longitude} Latitude: {station.Latitude} ChargeSlots: {station.ChargeSlots}");

                                break;
                            case 2:
                                Console.WriteLine("Enter an id of the drone");
                                id = Convert.ToInt32(Console.ReadLine());
                                Drone drone = DalObject.DalObject.GetSpecificDrone(id);
                                Console.WriteLine($"ID: {drone.ID} Model: {drone.Model} Status: {drone.Status } MaxWeight: {drone.MaxWeight} Battery: {drone.Battery}");

                                break;
                            case 3:
                                Console.WriteLine("Enter an id of a customer: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                Customer customer = DalObject.DalObject.GetSpecificCustomer(id);
                                Console.WriteLine($"ID: {customer.ID} Name: {customer.Name} Phone: {customer.Phone} Longitude: {customer.Longitude} Latitude: {customer.Latitude}");
                                break;
                            case 4:
                                Console.WriteLine("Enter an id of a parcial: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                Parcial parcial = DalObject.DalObject.GetSpecificParcial(id);
                                Console.WriteLine($"ID: {parcial.ID} Priority: {parcial.Priority} SenderID: {parcial.SenderID} TargetID: {parcial.TargetID} Weight: {parcial.Weight}");
                                break;
                            default:
                                break;
                        }
                        break;


                    case 4:
                        Console.WriteLine("to display the station list enter 1: ");
                        Console.WriteLine("to display the drone list enter 2: ");
                        Console.WriteLine("to display the customer list enter 3: ");
                        Console.WriteLine("to display the parcial list enter 4: ");
                        Console.WriteLine("to display the list of parcials that are free enter 5: ");
                        Console.WriteLine("to display the list of station that have free chargers enter 6: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:

                                Station[] stations = DalObject.DalObject.GetStation();
                                foreach (var station in stations)
                                {
                                    //Console.WriteLine($"id: {station.ID} Name: {station.Name} Longitude: {station.Longitude} Latitude: {station.Latitude} ChargeSlots: {station.ChargeSlots}" );
                                    Console.WriteLine(station.ToString());
                                }
                                break;
                            case 2:
                                Drone[] drones = DalObject.DalObject.GetDrone();
                                foreach (var drone in drones)
                                {
                                    //Console.WriteLine($"ID: {drone.ID} Model: {drone.Model} Status: {drone.Status } MaxWeight: {drone.MaxWeight} Battery: {drone.Battery}");
                                    Console.WriteLine(drone.ToString());
                                }
                                break;
                            case 3:
                                Customer[] customers = DalObject.DalObject.GetCustomer();
                                foreach (var customer in customers)
                                {
                                    //Console.WriteLine($"ID: {customer.ID} Name: {customer.Name} Phone: {customer.Phone} Longitude: {customer.Longitude} Latitude: {customer.Latitude}");
                                    Console.WriteLine(customer.ToString());
                                }
                                break;
                            case 4:
                                Parcial[] parcials = DalObject.DalObject.GetParcial();
                                foreach (var parcial in parcials)
                                {
                                    Console.WriteLine($"ID: {parcial.ID} Priority: {parcial.Priority} SenderID: {parcial.SenderID} TargetID: {parcial.TargetID} Weight: {parcial.Weight} droneId: {parcial.DroneID}");
                                }
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
        public static void showParcelsWithoutoutDrone()
        {
            Parcial[] parcels = DalObject.DalObject.GetParcial();
            foreach (var parcel in parcels)
            {
                if (parcel.DroneID != 0)
                {
                    Console.WriteLine($"ID: {parcel.ID} Priority: {parcel.Priority} SenderID: {parcel.SenderID} TargetID: {parcel.TargetID} Weight: {parcel.Weight} droneId: {parcel.DroneID}");

                }
            }
        }
        public static void showStationWithEmptyChargers()
        {
            int numOfChargers = 0;
            Station[] stations = DalObject.DalObject.GetStation();
            DroneCharge[] droneChargers = DalObject.DalObject.GetDroneCharge();
            for (int i = 0; i < stations.Length; i++)
            {
                for(int j = 0; j < droneChargers.Length; j++)
                {
                    if (stations[i].ID == droneChargers[j].StationID)
                        numOfChargers++;
                }
                if (numOfChargers < stations[i].ChargeSlots)
                {
                    Console.WriteLine($"id: {stations[i].ID} Name: {stations[i].Name} Longitude: {stations[i].Longitude} Latitude: {stations[i].Latitude} ChargeSlots: {stations[i].ChargeSlots}");
                }
            
            }
        }
    }
}
