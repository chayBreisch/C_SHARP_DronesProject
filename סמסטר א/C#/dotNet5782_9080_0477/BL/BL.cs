using System;
using DalObject;
using IBL.BO;
using DAL;
using IBL;
using System.Collections.Generic;
using IDAL.DO;
using System.Linq;
namespace BL
{
   //לשים לב מה עם GET SET IN CUSTOMERBL
    public partial class BL : Bl
    {
        Random rand = new Random();
        List<DroneBL> droneBLList;
        IDAL.IDal dalObject;
        //############################################################
        //constructor
        //############################################################
        /// <summary>
        /// constructor of class BL
        /// </summary>
        public BL()
        {
            droneBLList = new List<DroneBL>();
            //get all the electric rates
            dalObject = Factory.factory("DalObject");
            double[] arrayEletric = dalObject.requestElectric();
            double electricAvailable = arrayEletric[0];
            double electricLightHeight = arrayEletric[1];
            double electricMidHeight = arrayEletric[2];
            double electricHeavyHeight = arrayEletric[3];
            double electricChargingRate = arrayEletric[4];
            //for each drone we check what is the status and reboot in entries
            foreach (var drone in dalObject.GetDrone())
            {
                DroneBL droneBL = new DroneBL { ID = drone.ID, Model = drone.Model, Weight = drone.MaxWeight };
                Parcel parcel = dalObject.GetParcelByDroneID(drone.ID);
                Customer customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
                //check if the drone has a parcel
                if (parcel.SenderID != 0)
                    //if (!parcel.Equals(null))
                {
                    //check if the parcel of the drone is scheduled and not delivered
                    if (parcel.Scheduled != new DateTime() && parcel.Delivered == new DateTime())
                    {
                        droneBL.DroneStatus = DroneStatus.Delivery;

                        //check if the parcel of the drone is scheduled and not delivered and not picked up
                        if (parcel.PickedUp != new DateTime())
                        {
                            Station station = findClosestStation(new LocationBL(customerSender.Latitude, customerSender.Longitude));
                            droneBL.Location = new LocationBL(station.Latitude,station.Longitude);
                        }

                        //check if the parcel of the drone is scheduled and not delivered and picked up
                        else
                        {
                            droneBL.Location.Longitude = customerSender.Longitude;
                            droneBL.Location.Latitude = customerSender.Latitude;
                        }
                        Customer customerReciever = dalObject.GetSpecificCustomer(parcel.TargetID);
                        double electricitySenderToReciever = calcElectry(new LocationBL(customerSender.Longitude, customerSender.Latitude), new LocationBL
                            (customerReciever.Longitude, customerReciever.Latitude ), (int)parcel.Weight);
                        Station station1 = stationWithMinDisAndEmptySlots(new LocationBL(customerReciever.Latitude, customerReciever.Longitude ));
                        double electricityRecieverToCharger = calcElectry(new LocationBL(customerReciever.Longitude, customerReciever.Latitude), new LocationBL
                            (station1.Longitude, station1.Latitude ), 0);
                        int minEle = (int)Math.Round(electricitySenderToReciever + electricityRecieverToCharger);
                        droneBL.BatteryStatus = rand.Next(minEle, 100);
                    }
                }

                //check if drone is not in delivery
                if (droneBL.DroneStatus != DroneStatus.Delivery)
                {
                    droneBL.DroneStatus = (DroneStatus)rand.Next(0, 2);
                }

                //check if drone is in maintenance
                if (droneBL.DroneStatus == DroneStatus.Maintenance)
                {
                    int length = dalObject.lengthStation();
                    Station station = dalObject.GetStation().ToList()[rand.Next(0, length)];
                    droneBL.Location = new LocationBL(station.Latitude, station.Longitude);
                    droneBL.BatteryStatus = rand.Next(0, 21);
                }

                //check if drone is in available
                else if (droneBL.DroneStatus == DroneStatus.Available)
                {
                    List<Parcel> parcelBLsWithSuppliedParcel = dalObject.GetParcel().ToList().FindAll(p => p.Delivered != new DateTime());
                    ParcelBL parcelBL = convertDalToParcelBL(parcelBLsWithSuppliedParcel[rand.Next(0, parcelBLsWithSuppliedParcel.Count)]);
                    Customer customer = dalObject.GetSpecificCustomer(parcelBL.Sender.ID);
                    droneBL.Location = new LocationBL(customer.Latitude, customer.Longitude);
                    Station station1 = stationWithMinDisAndEmptySlots(droneBL.Location);
                    double electry = calcElectry(new LocationBL(station1.Longitude, station1.Latitude), droneBL.Location, 0);
                    int minElectric = (int)Math.Round(electry);
                    droneBL.BatteryStatus = rand.Next(minElectric, 100);
                }
                droneBLList.Add(droneBL);
            }
        }



        //#############################################################
        //check validaion id
        //#############################################################


        //#############################################################
        //Add functions
        //#############################################################


        //#############################################################
        //Get functions from BL
        //#############################################################


        //#############################################################
        //Get specific item functions
        //#############################################################


        //#############################################################
        //convert from IDAL.DO to IBL.BO functions
        //#############################################################


        //#############################################################
        //update functions
        //#############################################################


        //#############################################################
        //help functions
        //#############################################################
        /// <summary>
        ///calsulate the electricity that a drone needs to get from one location to another
        /// <param name="locatin1"></param>
        /// <param name="location2"></param>
        /// <param name="weight"></param>
        /// <returns>double</returns>
        public double calcElectry(LocationBL locatin1, LocationBL location2, int weight)
        {
            double distance1 = distance(locatin1, location2);
            return distance1 * dalObject.requestElectric()[weight];
        }
        /// <summary>
        /// return the stations with the minimum distance from the location and with empty slots
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Station</returns>
        //return the stations with the minimum distance from the location and with empty slots
        public Station stationWithMinDisAndEmptySlots(LocationBL location)////////////////////////////
        {
            double minDis = -1;
            double dis2 = 0;
            IEnumerable<Station> stations = dalObject.GetStation();
            Station sendStation = new Station();
            foreach (var station in stations)
            {
                dis2 = distance(location, new LocationBL(station.Latitude,  station.Longitude));
                if (dis2 < minDis || minDis == -1 && station.ChargeSlots != 0)
                {
                    minDis = dis2;
                    sendStation = station;
                }
            }
            return sendStation;
        }

        /// <summary>
        /// return the closest station to the location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Station</returns>
        public Station findClosestStation(LocationBL location)
        {
            double minDis = -1;
            double dis2 = 0;
            IEnumerable<Station> stations = dalObject.GetStation();
            Station sendStation = new Station();
            foreach (var station in stations)
            {
                dis2 = distance(location, new LocationBL(station.Latitude, station.Longitude));
                if (dis2 < minDis || minDis == -1)
                {
                    minDis = dis2;
                    sendStation = station;
                }
            }
            return sendStation;
        }

        /* public static double distance(double x1, double y1, double x2, double y2)
         {
             return Math.Sqrt(Math.Pow(x2 - x1, 2) +
             Math.Pow(y2 - y1, 2) * 1.0);
         }*/

        /// <summary>
        /// calculate the distance between to locations
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns>double</returns>
        static double distance(LocationBL location1, LocationBL location2)
        {
            return Math.Sqrt(Math.Pow(location2.Longitude - location1.Longitude, 2) +
            Math.Pow(location2.Latitude - location1.Latitude, 2) * 1.0);
        }

        
        /// <summary>
        /// check the range of the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public static bool CheckLongIdIsValid(ulong id)
        {
            return id > 100000000 && id < 1000000000;
        }

        













        /* public void AddCustomer(int id, string name, string phone, double latitude, double longitude)
         {
             Customer newCustomer = new Customer(id, name, phone, latitude, longitude);
            *//* newCustomer.ID = id;
             newCustomer.Name = name;
             newCustomer.Phone = phone;
             newCustomer.Latitude = latitude;
             newCustomer.Longitude = longitude;*//*
             dalObject.AddCustomer(newCustomer);
         }

        */

        //################################################
        //functions that update the dataSource array 
        //################################################
        /*public void updateConectDroneToParcial(int id)
        {
            Parcel newParcial = dalObject.GetSpecificParcial(id);
            Drone drone = new Drone();
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].Status == DroneStatus.Available)
                {
                    drone = DataSource.drones[i];
                    drone.Status = DroneStatus.Delivery;
                    break;
                }
            }
            newParcial.DroneID = drone.ID;
            int index1 = DataSource.drones.FindIndex(p => p.ID == newParcial.ID);
            int index = DataSource.drones.FindIndex(d => d.ID == drone.ID);
            DataSource.drones[index] = drone;
            DataSource.parcels[index1] = newParcial;
        }


        public void updateCollectParcialByDrone(int id)
        {

            Parcel newParcial = dalObject.GetSpecificParcial(id);
            if (newParcial.DroneID == 0)
            {
                Console.WriteLine("you didnt conect a drone");
            }
            newParcial.PickedUp = DateTime.Now;

        }

        public void updateSupplyParcialToCustomer(int id)
        {
            Parcel newParcial = dalObject.GetSpecificParcial(id);

            //if (newParcial.PickedUp)////////////////////////////////////////////////////
            {
                Console.WriteLine("you didnt collect a drone");
            }
            newParcial.Delivered = DateTime.Now;
        }
        public void updateSendDroneToCharge(int droneId, int statoinId)
        {
            DroneCharge droneCharge = new DroneCharge();
            int numOfChargers = 0;
            Station station = dalObject.GetSpecificStation(statoinId);
            Drone newDrone = dalObject.GetSpecificDrone(droneId);
            numOfChargers = 0;
            for (int j = 0; j < dalObject.returnLengthDroneCharger(); j++)
            {
                if (station.ID == DataSource.droneChargers[j].StationID)
                    numOfChargers++;
            }
            if (numOfChargers < station.ChargeSlots)
            {
                droneCharge.DroneID = newDrone.ID;
                droneCharge.StationID = station.ID;

            }

            newDrone.Status = DroneStatus.Maintenance;
            DataSource.droneChargers[DataSource.droneChargers.Count - 1] = droneCharge;
        }
        public void updateUnChargeDrone(int id)
        {
            Drone NewDrone = dalObject.GetSpecificDrone(id);
            int index = 0;
            for (int i = 0; i < DataSource.droneChargers.Count; i++)
            {
                if (DataSource.droneChargers[i].DroneID == NewDrone.ID)
                {
                    DataSource.droneChargers.RemoveAt(i);
                    index = i;
                    break;
                }
            }
            NewDrone.Status = DroneStatus.Available;
            NewDrone.Battery = 100;
            int index1 = DataSource.drones.FindIndex(d => d.ID == NewDrone.ID);
            DataSource.drones[index1] = NewDrone;
        }*/
    }
}
