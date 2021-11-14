﻿using System;
using DalObject;
using IBL.BO;
using IBL;
using System.Collections.Generic;
using IDAL.DO;
using System.Linq;
namespace BL
{
    class BL : Bl
    {
        Random rand = new Random();
        List<DroneBL> droneBLList;
        IDAL.IDal dalObject;
        //############################################################
        //constructor
        //############################################################
        public BL()
        {
            droneBLList = new List<DroneBL>();
            //IDAL.IDal dalObject = new DalObject.DalObject();
            dalObject = Factory.factory("DalObject");
            double[] arrayEletric = dalObject.requestElectric();
            double electricAvailable = arrayEletric[0];
            double electricLightHeight = arrayEletric[1];
            double electricMidHeight = arrayEletric[2];
            double electricHeavyHeight = arrayEletric[3];
            double electricChargingRate = arrayEletric[4];

            //List<DalObject.DataSource.drones> drones =  dalObject.GetDronesByList();
        }
        //#############################################################
        //Add functions
        //#############################################################

        public void AddCustomer(ulong id, string name, int phone, LocationBL location)
        {
            CustomerBL customer = new CustomerBL();
            customer.ID = id;
            customer.Name = name;
            customer.Phone = phone;
            customer.location.Latitude = location.Latitude;
            customer.location.Longitude = location.Longitude;
            AddCustomerToDal(id, name, phone, location);
        }

        public void AddCustomerToDal(ulong id, string name, int phone, LocationBL location)
        {
            Customer customer = new Customer();
            customer.ID = id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Longitude = location.Longitude;
            customer.Latitude = location.Latitude;
            dalObject.AddCustomer(customer);
        }

        public void AddStation(int id, string name, LocationBL location, int ChargeSlots)
        {
            StationBL station = new StationBL();
            station.ID = id;
            station.Name = name;
            station.location.Latitude = location.Latitude;
            station.location.Longitude = location.Longitude;
            station.ChargeSlots = ChargeSlots;
            AddStationToDal(id, name, location, ChargeSlots);
        }

        public void AddStationToDal(int id, string name, LocationBL location, int ChargeSlots)
        {
            Station station = new Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = location.Longitude;
            station.Latitude = location.Latitude;
            station.ChargeSlots = ChargeSlots;
            dalObject.AddStation(station);
        }

        public void AddDrone(int id, string model, WeightCatagories maxWeight, int stationID)
        {

            DroneBL droneBL = new DroneBL();
            Station station = dalObject.GetSpecificStation(id);

            droneBL.Model = model;
            droneBL.ID = id;
            droneBL.weight = maxWeight;
            droneBL.BatteryStatus = rand.Next(20, 40);
            droneBL.droneStatus = DroneStatus.Maintenance;
            droneBL.location.Longitude = station.Longitude;
            droneBL.location.Latitude = station.Latitude;

            AddDroneToDal(id, model, maxWeight);
            addDroneCharge(stationID, id);
            droneBLList.Add(droneBL);
        }

        public void AddDroneToDal(int id, string model, WeightCatagories maxWeight)
        {
            Drone drone = new Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
            dalObject.AddDrone(drone);
        }

        public void AddParcel(ulong sender, ulong target, WeightCatagories Weight, Priorities priority)
        {
            ParcelBL parcel = new ParcelBL();
            //parcel.ID = id;
            parcel.Sender.ID = sender;
            parcel.Reciever.ID = target;
            parcel.weightCatagories = Weight;
            parcel.priorities = priority;
            parcel.Created = DateTime.Now;
            parcel.Conected = new DateTime();
            parcel.Collected = new DateTime();
            parcel.Delivered = new DateTime();
            parcel.drone = null;
            AddParcelToDal(sender, target, Weight, priority);

        }

        public void AddParcelToDal(/*ulong id,*/ ulong sender, ulong target, WeightCatagories Weight, Priorities priority)
        {
            Parcel parcel = new Parcel();
            //parcel.ID = id;
            parcel.SenderID = sender;
            parcel.TargetID = target;
            parcel.Weight = Weight;
            parcel.Priority = priority;
            dalObject.AddParcel(parcel);
        }

        public void addDroneCharge(int stationID, int droneID)
        {
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneID = droneID;
            droneCharge.StationID = stationID;
            dalObject.AddDroneCharge(droneCharge);
        }
        //####################################################################
        //Get functions from BL
        //####################################################################
        public List<CustomerBL> GetCustomerBL()
        {

            List<Customer> customers = dalObject.GetCustomer().Cast<Customer>().ToList();
            List<CustomerBL> customers1 = new List<CustomerBL>();
            customers.ForEach(s => customers1.Add(convertDalCustomerToBl(s)));
            return customers1;
        }

        public List<DroneBL> GetDroneBL()
        {

            List<Drone> drone = dalObject.GetDrone().Cast<Drone>().ToList();
            List<DroneBL> drone1 = new List<DroneBL>();
            drone.ForEach(s => drone1.Add(convertDalDroneToBl(s)));
            return drone1;
        }
        public List<ParcelBL> GetParcelBL()
        {

            List<Parcel> parcel = dalObject.GetParcel().Cast<Parcel>().ToList();
            List<ParcelBL> parcel1 = new List<ParcelBL>();
            parcel.ForEach(s => parcel1.Add(convertDalToParcelBL(s)));
            return parcel1;
        }
        public List<StationBL> GetStationBL()
        {

            List<Station> stations = dalObject.GetStation().Cast<Station>().ToList();
            List<StationBL> stations1 = new List<StationBL>();
            stations.ForEach(s => stations1.Add(convertDalStationToBl(s)));
            return stations1;
        }

        //#############################################################
        //Get specific item functions
        //#############################################################
        public DroneBL GetSpecificDroneBL(int id)
        {
            try
            {
                return droneBLList.First(drone => drone.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new DAL.Exeptions(id);
            }
        }
        public StationBL GetSpecificStationBL(int id)
        {
            try
            {
                return convertDalStationToBl(dalObject.GetSpecificStation(id));
            }
            catch (ArgumentNullException e)
            {
                throw new DAL.Exeptions(id);
            }
        }

        public ParcelBL GetSpecificParcelBL(int id)
        {
            try
            {
                return convertDalToParcelBL(dalObject.GetSpecificParcel(id));
            }
            catch (ArgumentNullException e)
            {
                throw new DAL.Exeptions(id);
            }
        }
        public CustomerBL GetSpecificCustomerBL(ulong id)
        {
            try
            {
                return convertDalCustomerToBl(dalObject.GetSpecificCustomer(id));
            }
            catch (ArgumentNullException e)
            {
                throw new DAL.Exeptions(id);
            }
        }
        public List<Parcel> getParcelsWithoutoutDrone()
        {
            IEnumerable<Parcel> parcels = dalObject.GetParcel();
            List<Parcel> parcels1 = new List<Parcel>();
            foreach (var parcel in parcels)
            {
                if (parcel.DroneID == 0)
                {
                    parcels1.Add(parcel);
                }
            }
            return parcels1;
        }
        public List<StationBL> getStationWithEmptyChargers()
        {
            int numOfChargers = 0;
            IEnumerable<Station> stations = dalObject.GetStation();
            IEnumerable<DroneCharge> droneChargers = dalObject.GetDroneCharge();
            List<StationBL> stations1 = new List<StationBL>();

            foreach (var station in stations)
            {
                foreach (var droneCharge in droneChargers)
                {
                    if (station.ID == droneCharge.StationID)
                        numOfChargers++;
                }
                if (numOfChargers < station.ChargeSlots)
                {
                    stations1.Add(convertDalStationToBl(station));
                }
            }
            return stations1;
        }

        //######################################################################
        //convert from IDAL.DO to IBL.BO functions
        //######################################################################
        private StationBL convertDalStationToBl(Station s)
        {
            return new StationBL
            {
                ID = s.ID,
                Name = s.Name,
                ChargeSlots = s.ChargeSlots,
                location = new LocationBL() { Longitude = s.Longitude, Latitude = s.Latitude }
            };
        }

        private CustomerBL convertDalCustomerToBl(Customer c)
        {
            return new CustomerBL
            {
                ID = c.ID,
                Name = c.Name,
                Phone = c.Phone,
                location = new LocationBL() { Longitude = c.Longitude, Latitude = c.Latitude }
            };
        }
        private DroneBL convertDalDroneToBl(Drone d)
        {
            //IBL.BO.Drone drone = dalObject.GetSpecificDrone(d.ID);
            return new DroneBL { ID = d.ID, Model = d.Model }; /*+++++++++++++++++*/
        }
        private ParcelBL convertDalToParcelBL(Parcel p)
        {
            CustomerBL sender = convertDalCustomerToBl(dalObject.GetSpecificCustomer(p.SenderID));
            CustomerBL target = convertDalCustomerToBl(dalObject.GetSpecificCustomer(p.TargetID));
            DroneBL drone = convertDalDroneToBl(dalObject.GetSpecificDrone(p.DroneID));
            return new ParcelBL
            {
                ID = p.ID,
                Sender = sender,
                Reciever = target,
                weightCatagories = p.Weight,
                Collected = p.PickedUp,
                drone = drone,
                Created = p.Requested,
                Conected = p.Scheduled,
                Delivered = p.Delivered,
                priorities = p.Priority
            };
        }


        //######################################################
        //update functions
        //######################################################

        public void updateDrone(DroneBL drone)
        {
            int index = droneBLList.FindIndex(d => d.ID == drone.ID);
            droneBLList[index] = drone;
        }




        public void updateDataDroneName(int id, string model)
        {
            DroneBL droneBl = droneBLList.First(d => d.ID == id);
            droneBl.Model = model;
            updateDrone(droneBl);

            Drone drone = dalObject.GetSpecificDrone(id);
            drone.Model = model;
            dalObject.updateDrone(drone);
        }
        public void updateDataStation(int id, string name = null, int chargeSlots = -1)
        {
            Station station = dalObject.GetSpecificStation(id);
            if (name != null)
            {
                station.Name = name;
            }
            if (chargeSlots != -1)
            {
                station.ChargeSlots = chargeSlots;
            }
            dalObject.updateStation(station);

        }
        public void updateDataCustomer(ulong id, string name = null, int phone = -1)
        {
            Customer customer = dalObject.GetSpecificCustomer(id);
            if (name != null)
            {
                customer.Name = name;
            }
            if (phone != -1)
            {
                customer.Phone = phone;
            }
            dalObject.updateCustomer(customer);
        }
        public void updateSendDroneToCharge(int id)
        {
            //check this function////////////////////////////////
            Station station = new Station();
            double dis2 = 0;
            DroneBL droneBL = droneBLList.First(d => d.ID == id);
            //Drone drone = dalObject.GetSpecificDrone(id);
            if (droneBL.droneStatus == DroneStatus.Available)
            {
                for (int i = 0; i < dalObject.lengthStation(); i++)
                {
                    station = stationWithMinDisAndEmptySlots(droneBL.location);
                    if (station.ChargeSlots != 0)
                    {
                        break;
                    }
                }
                dis2 = distance(droneBL.location.Latitude, droneBL.location.Longitude, station.Latitude, station.Longitude);

                if (dis2 * dalObject.requestElectric()[0] > droneBL.BatteryStatus)
                {
                    throw new DAL.Exeptions("dont have enough battery");
                }
                if (droneBL.droneStatus == DroneStatus.Available)
                {
                    throw new Exception("there is no a station with empty charge slots");
                }
                droneBL.BatteryStatus -= dis2 * dalObject.requestElectric()[0];
                droneBL.location.Latitude = station.Latitude;
                droneBL.location.Longitude = station.Longitude;
                droneBL.droneStatus = DroneStatus.Maintenance;
                station.ChargeSlots -= 1;
                dalObject.updateStation(station);
                DroneCharge droneCharge = new DroneCharge();
                droneCharge.DroneID = id;
                droneCharge.StationID = station.ID;
                dalObject.AddDroneCharge(droneCharge);
            }

        }
        public void updateUnchargeDrone(int id, double timeInCharge)
        {
            DroneBL droneBL = new DroneBL();
            try
            {
                droneBL = droneBLList.First(d => d.ID == id && d.droneStatus != DroneStatus.Maintenance);
            }
            catch (ArgumentNullException e)
            {
                throw new Exception($"{e} can not uncharge drone");
            }

            droneBL.BatteryStatus += timeInCharge * dalObject.requestElectric()[4];
            droneBL.droneStatus = DroneStatus.Available;
            DroneCharge droneCharge = dalObject.getSpecificDroneChargeByDroneID(droneBL.ID);
            Station station = dalObject.GetSpecificStation(droneCharge.StationID);
            station.ChargeSlots += 1;
            dalObject.updateStation(station);
            dalObject.removeDroneCharge(droneCharge);
        }

        public void updateConnectParcelToDrone(int id)
        {
            DroneBL droneBL = droneBLList.First(d => d.ID == id);
            if (droneBL.droneStatus != DroneStatus.Available)
            {
                throw new Exception("the drone is not free");
            }
            Customer customerSender, customerCurrent, customerReciever;
            Parcel currentParcel = new Parcel();
            List<Parcel> parcels = getParcelsWithoutoutDrone();
            currentParcel = new Parcel() { Weight = 0 };
            foreach (var parcel in parcels) { 
            {
                if (parcel.Requested.Equals(null))
                    break;
                customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
                customerCurrent = dalObject.GetSpecificCustomer(currentParcel.SenderID);
                customerReciever = dalObject.GetSpecificCustomer(parcel.TargetID);
                double disDroneToSenderParcel = distance(droneBL.location.Longitude, droneBL.location.Latitude, customerSender.Longitude, customerSender.Latitude);
                double disSenderToReciever = distance(customerSender.Longitude, customerSender.Latitude, customerReciever.Longitude, customerReciever.Latitude);
                double electricity = dalObject.requestElectric()[(int)parcel.Weight];
                Station station = stationWithMinDisAndEmptySlots(new LocationBL { Latitude = customerReciever.Latitude, Longitude = customerReciever.Longitude });
                double disRecieverToCharger = distance(customerReciever.Longitude, customerReciever.Latitude, station.Longitude, station.Latitude);
                

                if (droneBL.BatteryStatus - (electricity * disDroneToSenderParcel + electricity * disSenderToReciever + disRecieverToCharger * electricity) > 0)
                {
                    if (parcel.Weight < droneBL.weight)
                    {
                        if (currentParcel.Priority < parcel.Priority)
                        {
                            currentParcel = parcel;
                        }
                        else if (currentParcel.Priority == parcel.Priority)
                        {
                            if (parcel.Weight > currentParcel.Weight)
                            {
                                currentParcel = parcel;
                            }
                            else if (parcel.Weight == currentParcel.Weight)
                            {

                                if (disDroneToSenderParcel < distance(droneBL.location.Longitude, droneBL.location.Latitude, customerCurrent.Longitude, customerCurrent.Latitude))
                                {
                                    currentParcel = parcel;
                                }
                            }
                        }
                    }
                }
            }}
            if (currentParcel.Weight == 0)
            {
                throw new Exception("didn't find a drone for your parcel");
            }
            droneBL.droneStatus = DroneStatus.Delivery;
            updateDrone(droneBL);
            currentParcel.DroneID = id;
            currentParcel.Scheduled = DateTime.Now;
            dalObject.updateParcel(currentParcel);
        }

        public void updateCollectParcelByDrone(int id)//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            DroneBL droneBL = droneBLList.First(d => d.ID == id);
            Parcel parcel = new Parcel();
            if(droneBL.droneStatus == DroneStatus.Delivery)
            {
                parcel = dalObject.GetSpecificParcelByDroneID(droneBL.ID);
                if (!parcel.PickedUp.Equals(null) || parcel.Scheduled.Equals(null))
                {
                    throw new Exception("can't collect parcel");
                }

            }
            Customer customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
            double disDroneToSenderParcel = distance(droneBL.location.Longitude, droneBL.location.Latitude, customerSender.Longitude, customerSender.Latitude);

            //double dis2 = distance(droneBL.location.Latitude, droneBL.location.Longitude, station.Latitude, station.Longitude);
            //droneBL.BatteryStatus -= dis2 * dalObject.requestElectric()[0];
            droneBL.BatteryStatus -= disDroneToSenderParcel * dalObject.requestElectric()[(int)parcel.Weight];
            droneBL.location.Longitude = customerSender.Longitude;
            droneBL.location.Latitude = customerSender.Latitude;
            updateDrone(droneBL);
            parcel.PickedUp = DateTime.Now;
            dalObject.updateParcel(parcel);
        }
        //###################################################################
        //help functions
        //###################################################################

        /*public IDAL.DO.Parcel retunParcelWithHighPriority()
        {
            
            return currentParcel;
        }*/
        public Station stationWithMinDisAndEmptySlots(LocationBL location)////////////////////////////
        {
            double minDis = -1;
            double dis2 = 0;
            IEnumerable<Station> stations = dalObject.GetStation();
            Station sendStation = new Station();
            foreach (var station in stations)
            {
                dis2 = distance(location.Latitude, location.Longitude, station.Latitude, station.Longitude);
                if (dis2 < minDis || minDis == -1 && station.ChargeSlots != 0)
                {
                    minDis = dis2;
                    sendStation = station;
                }
            }
            return sendStation;
        }


        public static double distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) +
            Math.Pow(y2 - y1, 2) * 1.0);
        }
        /*static double distance(Location location1, Location location2*//*double x1, double y1, double x2, double y2*//*)
        {
            return Math.Sqrt(Math.Pow(location2.Longitude - location1.Longitude, 2) +
            Math.Pow(location2.Latitude - location1.Latitude, 2) * 1.0);
        }*/
        public bool checkUniqeIDCustomer(ulong id)
        {
            int sum = 0;
            IEnumerable<Customer> customers = dalObject.GetCustomer();
            foreach (var customer in customers)
            {
                if (customer.ID == id)
                {
                    sum += 1;
                }
            }
            if (sum == 1)
                return false;
            return false;
        }
        public static bool CheckLongIdIsValid(ulong id)
        {
            return id > 100000000 && id < 1000000000;
        }

        public static bool CheckValidIdCustomer(ulong id)///////////////////לבדוק אם גדול מעשר אחרי הכפלה
        {
            int sum = 0, digit, digit2 = 0;
            for (int i = 0; i < 9; i++)
            {
                digit = (int)(id % 10);
                if ((i % 2) == 0)
                {
                    digit *= 2;
                    if (digit > 10)
                    {
                        digit2 = digit % 10;
                        digit /= 10;
                        digit2 += digit;
                    }
                    digit = digit2;
                }
                sum += digit;
                id /= 10;
            }
            if ((sum % 10) == 0)
            {
                return true;
            }
            return false;
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
