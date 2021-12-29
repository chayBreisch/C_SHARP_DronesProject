using System;
using DalObject;
using BO;
using DAL;
using BlApi;
using System.Collections.Generic;
using DO;
using System.Linq;
namespace BL
{
    //לשים לב מה עם GET SET IN CUSTOMERBL
    internal sealed partial class BL : Bl
    {
        static BL Instance;
        public static BL GetInstance()
        {
            if (Instance == null)
                Instance = new BL();
            return Instance;

        }
        Random rand = new Random();
        List<BO.Drone> droneBLList = new List<BO.Drone>();
        IDAL.IDal dalObject;
        //############################################################
        //constructor
        //############################################################
        /// <summary>
        /// constructor of class BL
        /// </summary>
        public BL()
        {

            //get all the electric rates
            try
            {
                dalObject = FactoryDAL.factory("DalObject");
            }
            catch (DAL.CantReturnDalObject)
            {
                throw new CantReturnDalObject();
            }
            double[] arrayEletric = dalObject.RequestElectric();
            double electricAvailable = arrayEletric[0];
            double electricLightHeight = arrayEletric[1];
            double electricMidHeight = arrayEletric[2];
            double electricHeavyHeight = arrayEletric[3];
            double electricChargingRate = arrayEletric[4];
            //for each drone we check what is the status and reboot in entries
            foreach (var drone in dalObject.GetDrones())
            {
                BO.Drone droneBL = new BO.Drone { ID = drone.ID, Model = drone.Model, Weight = drone.MaxWeight, IsActive = drone.IsActive };
                DO.Parcel parcel = dalObject.GetParcelBy(p => p.DroneID == drone.ID);
                DO.Customer customerSender = dalObject.GetCustomerById(c => c.ID == parcel.SenderID);
                //check if the drone has a parcel
                if (parcel.SenderID != 10000000)
                //if (!parcel.Equals(null))
                {
                    //check if the parcel of the drone is scheduled and not delivered
                    if (parcel.Scheduled != null && parcel.Delivered == null)
                    {
                        droneBL.parcelInDelivery = new ParcelInDelivery(convertDalToParcelBL(parcel), dalObject);
                        droneBL.DroneStatus = DroneStatus.Delivery;

                        //check if the parcel of the drone is scheduled and not delivered and not picked up
                        if (parcel.PickedUp != null)
                        {
                            DO.Station station = findClosestStation(new LocationBL(customerSender.Latitude, customerSender.Longitude));
                            droneBL.Location = new LocationBL(station.Latitude, station.Longitude);
                        }

                        //check if the parcel of the drone is scheduled and not delivered and picked up
                        //לבדוק מה בנות עשו
                        else
                        {
                            droneBL.Location = new LocationBL(customerSender.Latitude, customerSender.Longitude);
                        }
                        DO.Customer customerReciever = dalObject.GetCustomerById(c => c.ID == parcel.TargetID);
                        double electricitySenderToReciever = calcElectry(new LocationBL(customerSender.Longitude, customerSender.Latitude), new LocationBL
                            (customerReciever.Longitude, customerReciever.Latitude), (int)parcel.Weight);
                        DO.Station station1 = stationWithMinDisAndEmptySlots(new LocationBL(customerReciever.Latitude, customerReciever.Longitude));
                        double electricityRecieverToCharger = calcElectry(new LocationBL(customerReciever.Longitude, customerReciever.Latitude), new LocationBL
                            (station1.Longitude, station1.Latitude), 0);
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
                    int length = dalObject.LengthStation();
                    DO.Station station = dalObject.GetStations().ToList()[rand.Next(0, length)];
                    droneBL.Location = new LocationBL(station.Latitude, station.Longitude);
                    droneBL.BatteryStatus = rand.Next(0, 21);
                    DroneCharge droneCharge = new DroneCharge();
                    droneCharge.DroneID = drone.ID;
                    droneCharge.StationID = station.ID;
                    dalObject.AddDroneCharge(droneCharge);
                }

                //check if drone is in available
                else if (droneBL.DroneStatus == DroneStatus.Available)
                {
                    List<DO.Parcel> parcelBLsWithSuppliedParcel = dalObject.GetParcels().ToList().FindAll(p => p.Delivered != null);
                    if (parcelBLsWithSuppliedParcel.Count > 0)
                    {
                        BO.Parcel parcelBL = convertDalToParcelBL(parcelBLsWithSuppliedParcel[rand.Next(0, parcelBLsWithSuppliedParcel.Count)]);
                        DO.Customer customer = dalObject.GetCustomerById(c => c.ID == parcelBL.Sender.ID);
                        droneBL.Location = new LocationBL(customer.Latitude, customer.Longitude);
                        DO.Station station1 = stationWithMinDisAndEmptySlots(droneBL.Location);
                        double electry = calcElectry(new LocationBL(station1.Longitude, station1.Latitude), droneBL.Location, 0);
                        int minElectric = (int)Math.Round(electry);
                        droneBL.BatteryStatus = rand.Next(minElectric, 100);
                    }
                }
                droneBLList.Add(droneBL);
            }
        }


        /// <summary>
        ///calsulate the electricity that a drone needs to get from one location to another
        /// <param name="locatin1"></param>
        /// <param name="location2"></param>
        /// <param name="weight"></param>
        /// <returns>double</returns>
        private double calcElectry(LocationBL locatin1, LocationBL location2, int weight)
        {
            double distance1 = distance(locatin1, location2);
            return distance1 * dalObject.RequestElectric()[weight];
        }

        /// <summary>
        /// return the stations with the minimum distance from the location and with empty slots
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Station</returns>
        //return the stations with the minimum distance from the location and with empty slots
        private DO.Station stationWithMinDisAndEmptySlots(LocationBL location)////////////////////////////
        {
            double minDis = -1;
            double dis2 = 0;
            IEnumerable<DO.Station> stations = dalObject.GetStations();
            DO.Station sendStation = new DO.Station();
            foreach (var station in stations)
            {
                dis2 = distance(location, new LocationBL(station.Latitude, station.Longitude));
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
        private DO.Station findClosestStation(LocationBL location)
        {
            double minDis = -1;
            double dis2 = 0;
            IEnumerable<DO.Station> stations = dalObject.GetStations();
            DO.Station sendStation = new DO.Station();
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

        /// <summary>
        /// calculate the distance between to locations
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns>double</returns>
        public static double distance(LocationBL location1, LocationBL location2)
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

        /// <summary>
        /// return parcels that are not connected to a drone
        /// </summary>
        /// <returns>List<ParcelBL></returns>
        /*public IEnumerable<BO.Parcel> GetParcelsWithoutoutDrone()
        {
            IEnumerable<BO.Parcel> parcels = GetParcelsBL();
            List<BO.Parcel> parcelsWithOutDrone = new List<BO.Parcel>();
            foreach (var parcel in parcels)
            {
                if (parcel.Drone == null)
                {
                    parcelsWithOutDrone.Add(parcel);
                }
            }
            return parcelsWithOutDrone;
        }*/

        /// <summary>
        /// return station with epty chargers
        /// </summary>
        /// <returns>List<StationBL></returns>
        /*public IEnumerable<BO.Station> GetStationWithEmptyChargers()
        {
            int numOfChargers = 0;
            IEnumerable<BO.Station> stations = GetStationsBL();
            List<BO.Station> stationsWithEmptyChargers = new List<BO.Station>();
            List<DroneCharge> droneChargers = dalObject.GetDroneCharges().ToList();
            foreach (var station in stations)
            {
                foreach (var droneCharger in droneChargers)
                {
                    if (station.ID == droneCharger.StationID)
                        numOfChargers++;
                }
                if (numOfChargers < station.ChargeSlots)
                    stationsWithEmptyChargers.Add(station);
            }
            return stationsWithEmptyChargers;
        }*/

        public Array GetweightCategoriesEnumItem()
        {
            return Enum.GetValues(typeof(WeightCatagories));
        }
        public Array GetPrioritiesEnumItem()
        {
            return Enum.GetValues(typeof(Priorities));
        }
    }
}
