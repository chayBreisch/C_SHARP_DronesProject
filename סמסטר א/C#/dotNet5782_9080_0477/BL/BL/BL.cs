using System;
using BO;
using BlApi;
using System.Collections.Generic;
using DO;
using System.Linq;
using DALException;
using DalApi;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BL
{
    //לשים לב מה עם GET SET IN CUSTOMERBL
    internal sealed partial class BL : IBL
    {
        private static BL Instance;
        public static BL GetInstance()
        {
            if (Instance == null)
                Instance = new BL();
            return Instance;

        }
        Random rand = new Random();
        List<BO.Drone> droneBLList = new List<BO.Drone>();
        internal IDAL.IDal dalObject;
        double electricAvailable;
        double electricLightHeight;
        double electricMidHeight;
        double electricHeavyHeight;
        double electricChargingRate;
        //############################################################
        //constructor
        //############################################################
        /// <summary>
        /// constructor of class BL
        /// </summary>
        private BL()
        {

            #region get all the electric rates
            dalObject = DalFactory.GetDal();
            double[] arrayEletric = dalObject.RequestElectric();
            electricAvailable = arrayEletric[0];
            electricLightHeight = arrayEletric[1];
            electricMidHeight = arrayEletric[2];
            electricHeavyHeight = arrayEletric[3];
            electricChargingRate = arrayEletric[4];
            #endregion

            //for each drone we check what is the status and reboot in entries
            foreach (var drone in dalObject.GetDrones())
            {
                BO.Drone droneBL = new BO.Drone { ID = drone.ID, Model = drone.Model, Weight = drone.MaxWeight, IsActive = drone.IsActive };
                DO.Parcel parcel = dalObject.GetParcelBy(p => p.DroneID == drone.ID);
                //check if the drone has a parcel
                if (parcel.SenderID != 10000000 && parcel.SenderID != 0)
                //if (!parcel.Equals(null))
                {
                    BO.Customer customerSender = GetSpecificCustomerBL(c => c.ID == parcel.SenderID);
                    //check if the parcel of the drone is scheduled and not delivered
                    if (parcel.Scheduled != null && parcel.Delivered == null)
                    {
                        droneBL.parcelInDelivery = new ParcelInDelivery(convertDalToParcelBL(parcel), dalObject);
                        droneBL.DroneStatus = DroneStatus.Delivery;

                        //check if the parcel of the drone is scheduled and not delivered and not picked up
                        if (parcel.PickedUp != null)
                        {
                            DO.Station station = findClosestStation(customerSender.Location);
                            droneBL.Location = new LocationBL(station.Latitude, station.Longitude);
                        }

                        //check if the parcel of the drone is scheduled and not delivered and picked up
                        //לבדוק מה בנות עשו
                        else
                        {
                            droneBL.Location = customerSender.Location;
                        }
                        BO.Customer customerReciever = GetSpecificCustomerBL(c => c.ID == parcel.TargetID);
                        double electricitySenderToReciever = calcElectry(customerSender.Location, customerReciever.Location, (int)parcel.Weight);
                        BO.Station station1 = stationWithMinDisAndEmptySlots(customerReciever.Location);
                        double electricityRecieverToCharger = calcElectry(customerReciever.Location, station1.Location, 0);
                        int minEle = (int)Math.Round(electricitySenderToReciever + electricityRecieverToCharger);
                        droneBL.BatteryStatus = rand.Next(minEle, 100);
                    }
                }

                #region check if drone is not in delivery
                if (droneBL.DroneStatus != DroneStatus.Delivery)
                {
                    droneBL.DroneStatus = (DroneStatus)rand.Next(0, 2);
                }
                #endregion

                #region check if drone is in maintenance

                if (droneBL.DroneStatus == DroneStatus.Maintenance)
                {
                    int length = dalObject.LengthStation();
                    List<DO.Station> s = dalObject.GetStations().ToList();
                    DO.Station station = s[rand.Next(0, length)];
                    droneBL.Location = new LocationBL(station.Latitude, station.Longitude);
                    droneBL.BatteryStatus = rand.Next(0, 21);
                    DroneCharge droneCharge = new DroneCharge();
                    droneCharge.DroneID = drone.ID;
                    droneCharge.StationID = station.ID;
                    dalObject.AddDroneCharge(droneCharge);
                }
                #endregion

                #region check if drone is in available

                else if (droneBL.DroneStatus == DroneStatus.Available)
                {
                    List<DO.Parcel> parcelBLsWithSuppliedParcel = dalObject.GetParcels().ToList().FindAll(p => p.Delivered != null);
                    if (parcelBLsWithSuppliedParcel.Count > 0)
                    {
                        BO.Parcel parcelBL = convertDalToParcelBL(parcelBLsWithSuppliedParcel[rand.Next(0, parcelBLsWithSuppliedParcel.Count)]);
                        BO.Customer customer = GetSpecificCustomerBL(c => c.ID == parcelBL.Sender.ID);
                        droneBL.Location = customer.Location;
                        BO.Station station1 = stationWithMinDisAndEmptySlots(droneBL.Location);
                        double electry = calcElectry(station1.Location, droneBL.Location, 0);
                        int minElectric = (int)Math.Round(electry);
                        droneBL.BatteryStatus = rand.Next(minElectric, 100);
                    }
                }
                #endregion

                droneBLList.Add(droneBL);
            }
        }

        #region help functions

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
        private BO.Station stationWithMinDisAndEmptySlots(LocationBL location)
        {
            double minDis = -1;
            double dis2 = 0;
            IEnumerable<BO.Station> stations = getStationsBL();
            BO.Station sendStation = new BO.Station();
            foreach (var station in stations)
            {
                dis2 = distance(location, station.Location);
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
        /// return values of weightCatagories enum
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Array GetweightCategoriesEnumItem()
        {
            return Enum.GetValues(typeof(WeightCatagories));
        }

        /// <summary>
        /// return values of priorities enum
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Array GetPrioritiesEnumItem()
        {
            return Enum.GetValues(typeof(Priorities));
        }

        /// <summary>
        /// get rate of charging
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double getRateOfCharging()
        {
            return this.electricChargingRate;
        }
        #endregion

        /// <summary>
        /// start simularion
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="worker"></param>
        /// <param name="updateDrone"></param>
        /// <param name="needToStop"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void StartSimulation(BO.Drone drone, BackgroundWorker worker, Action<BO.Drone, int> updateDrone, Func<bool> needToStop)
        {
            new Simulation(this, drone, worker, updateDrone, needToStop);
        }
    }
}
