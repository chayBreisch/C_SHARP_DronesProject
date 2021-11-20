using System;
using IBL.BO;
using BL;
using DalObject;
using System.Collections.Generic;

namespace ConsoleUI_BL
{
    class Program
    {
        //האם צריך לבדוק משקל קטן מ 0
        //באיזה ערך לאתחל את המיקום?
        //אם אני רוצה לראות חבילה איך אני עושה את זה?
        //לבדוק אם זה בסדר שאני שולחת ArgumentNullException
        //אף פעם אין מספיק בטריה בשביל לשלוח להטענה
        //כשמוסיפים רחפן באיזה מצב הוא צריך להיות

        static void Main(string[] args)
        {
            try
            {
                IBL.Bl bL = new BL.BL();
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
                                    try
                                    {
                                        bL.addStation(Id, Name, location, ChargeSlots);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"{e}\n\n\n\n\n");
                                    }
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
                                    try
                                    {
                                        bL.addDrone(id, Model, weight, number);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"{e}\n\n\n\n\n");
                                    }

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
                                    try { 
                                    bL.AddCustomer(IdCustomer, NameCustomer, Phone, location1);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"{e}\n\n\n\n\n");
                                    }

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
                                    try { 
                                    bL.AddParcel(SenderId, RecieverId, weight, priority);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"{e}\n\n\n\n\n");
                                    }
                                    break;
                                default:
                                    Console.WriteLine("please enter a number between 1-4");
                                    break;
                            }
                            break;
                        case 2://////////////////לבדוק את הפונקציות וההרצה ולעשות את כל התפיסת שגיאות ולהוסיף מקרה 5
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
                                    try
                                    {
                                        bL.updateDataStation(id, nameOfStation, chargeSlots);
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine($"{e}\n\n\n\n\n");
                                    }
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
                                case 4:
                                    Console.WriteLine("enter id");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    try
                                    {
                                        bL.updateSendDroneToCharge(id);
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;
                        case 3:
                            Console.WriteLine("to display a station enter 1");
                            Console.WriteLine("to display a drone enter 2");
                            Console.WriteLine("to display a customer enter 3");
                            Console.WriteLine("to display a parcel enter 4");
                            choice = Convert.ToInt32(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("enter id station: ");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    try
                                    {
                                        Console.WriteLine(bL.GetSpecificStationBL(id));
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine($"{e}\n\n\n\n\n\n");
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("enter id drone: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    try { 
                                    Console.WriteLine(bL.GetSpecificDroneBL(id));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"{e}\n\n\n\n\n\n");
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("enter id customer: ");
                                    ulong customerId = ulong.Parse(Console.ReadLine());
                                    try
                                    {
                                        Console.WriteLine(bL.GetSpecificCustomerBL(customerId));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"{e}\n\n\n\n\n\n");
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("enter id parcel: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    try { 
                                    Console.WriteLine(bL.GetSpecificParcelBL(id));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"{e}\n\n\n\n\n\n");
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case 4://///////////////////////////////////////להוסיף את מקרה 5 ואת מקרה 6
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
}