using System;
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
        List<IBL.BO.Drone> droneBLList;
        IDAL.IDal dalObject;
        public BL()
        {
            droneBLList = new List<IBL.BO.Drone>();
            IDAL.IDal dalObject = new DalObject.DalObject();
            double[] arrayEletric = dalObject.requestElectric();
            double electricAvailable = arrayEletric[0];
            double electricLightHeight = arrayEletric[1];
            double electricMidHeight = arrayEletric[2];
            double electricHeavyHeight = arrayEletric[3];
            double electricChargingRate = arrayEletric[4];

            //List<DalObject.DataSource.drones> drones =  dalObject.GetDronesByList();
        }

        public void AddCustomer(ulong id, string name, string phone, Location location)
        {
            IBL.BO.Customer customer = new IBL.BO.Customer();
            customer.ID = id;
            customer.Name = name;
            customer.Phone = phone;
            customer.location.Latitude = location.Latitude;
            customer.location.Longitude = location.Longitude;
            AddCustomerToDal(id, name, phone, location);
        }

        public void AddCustomerToDal(ulong id, string name, string phone, Location location)
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer();
            customer.ID = id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Longitude = location.Longitude;
            customer.Latitude = location.Latitude;
            dalObject.AddCustomer(customer);
        }

        public void AddStation(int id, string name, Location location, int ChargeSlots)
        {
            IBL.BO.Station station = new IBL.BO.Station();
            station.ID = id;
            station.Name = name;
            station.location.Latitude = location.Latitude;
            station.location.Longitude = location.Longitude;
            station.ChargeSlots = ChargeSlots;
            AddStationToDal(id, name, location, ChargeSlots);
        }

        public void AddStationToDal(int id, string name, Location location, int ChargeSlots)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = location.Longitude;
            station.Latitude = location.Latitude;
            station.ChargeSlots = ChargeSlots;
            dalObject.AddStation(station);
        }

        public void AddDrone(int id, string model, WeightCatagories maxWeight, int stationID)
        {

            IBL.BO.Drone droneBL = new IBL.BO.Drone();
            IDAL.DO.Station station = dalObject.GetSpecificStation(id);
            
            droneBL.Model = model;
            droneBL.ID = id;
            droneBL.weightCatagories = maxWeight;
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
            IDAL.DO.Drone drone = new IDAL.DO.Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
            dalObject.AddDrone(drone);
        }

        public void AddParcel(ulong sender, ulong target, WeightCatagories Weight, Priorities priority)
        {
            IBL.BO.Parcel parcel = new IBL.BO.Parcel();
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
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
            //parcel.ID = id;
            parcel.SenderID = sender;
            parcel.TargetID = target;
            parcel.Weight = Weight;
            parcel.Priority = priority;
            dalObject.AddParcel(parcel);
        }

        public void addDroneCharge(int stationID, int droneID)
        {
            IDAL.DO.DroneCharge droneCharge = new IDAL.DO.DroneCharge();
            droneCharge.DroneID = droneID;
            droneCharge.StationID = stationID;
            dalObject.AddDroneCharge(droneCharge);
        }
        public List<IBL.BO.Customer> GetCustomerBL()
        {

            List<IDAL.DO.Customer> customers = dalObject.GetCustomer().Cast<IDAL.DO.Customer>().ToList();
            List<IBL.BO.Customer> customers1 = new List<IBL.BO.Customer>();
            customers.ForEach(s => customers1.Add(convertDalCustomerToBl(s)));
            return customers1;
        }
        
        public List<IBL.BO.Drone> GetDroneBL()
        {

            List<IDAL.DO.Drone> drone = dalObject.GetDrone().Cast<IDAL.DO.Drone>().ToList();
            List<IBL.BO.Drone> drone1 = new List<IBL.BO.Drone>();
            drone.ForEach(s => drone1.Add(convertDalDroneToBl(s)));
            return drone1;
        }
        public List<IBL.BO.Parcel> GetParcelBL()
        {

            List<IDAL.DO.Parcel> parcel = dalObject.GetParcel().Cast<IDAL.DO.Parcel>().ToList();
            List<IBL.BO.Parcel> parcel1 = new List<IBL.BO.Parcel>();
            parcel.ForEach(s => parcel1.Add(convertDalToParcelBL(s)));
            return parcel1;
        }
        public List<IBL.BO.Station> GetStationBL()
        {

            List<IDAL.DO.Station> stations = dalObject.GetStation().Cast<IDAL.DO.Station>().ToList();
            List<IBL.BO.Station> stations1 = new List<IBL.BO.Station>();
            stations.ForEach(s => stations1.Add(convertDalStationToBl(s)));
            return stations1;
        }
        

        public IBL.BO.Drone GetSpecificDroneBL(int id)
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
        public IBL.BO.Station GetSpecificStationBL(int id)
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

        public IBL.BO.Parcel GetSpecificParcelBL(int id)
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
        public IBL.BO.Customer GetSpecificCustomerBL(ulong id)
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
        public List<IBL.BO.Parcel> getParcelsWithoutoutDrone()
        {
            IEnumerable<IDAL.DO.Parcel> parcels = dalObject.GetParcel();
            List<IBL.BO.Parcel> parcels1 = new List<IBL.BO.Parcel>();
            foreach (var parcel in parcels)
            {
                if (parcel.DroneID == 0)
                {
                    parcels1.Add(convertDalToParcelBL(parcel));
                }
            }
            return parcels1;
        }
        public List<IBL.BO.Station> getStationWithEmptyChargers()
        {
            int numOfChargers = 0;
            IEnumerable<IDAL.DO.Station> stations = dalObject.GetStation();
            IEnumerable<IDAL.DO.DroneCharge> droneChargers = dalObject.GetDroneCharge();
            List<IBL.BO.Station> stations1 = new List<IBL.BO.Station>();

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


        public IBL.BO.Station convertDalStationToBl(IDAL.DO.Station s)
        {
            return new IBL.BO.Station
            {
                ID = s.ID,
                Name = s.Name,
                ChargeSlots = s.ChargeSlots,
                location = new Location() { Longitude = s.Longitude, Latitude = s.Latitude }
            };
        }

        public IBL.BO.Customer convertDalCustomerToBl(IDAL.DO.Customer c)
        {
            return new IBL.BO.Customer
            {
                ID = c.ID,
                Name = c.Name,
                Phone = c.Phone,
                location = new Location() { Longitude = c.Longitude, Latitude = c.Latitude }
            };
        }
        public IBL.BO.Drone convertDalDroneToBl(IDAL.DO.Drone d)
        {
            //IBL.BO.Drone drone = dalObject.GetSpecificDrone(d.ID);
            return new IBL.BO.Drone { ID = d.ID, Model = d.Model }; /*+++++++++++++++++*/
        }
        public IBL.BO.Parcel convertDalToParcelBL(IDAL.DO.Parcel p)
        {
            IBL.BO.Customer sender = convertDalCustomerToBl(dalObject.GetSpecificCustomer(p.SenderID));
            IBL.BO.Customer target = convertDalCustomerToBl(dalObject.GetSpecificCustomer(p.TargetID));
            IBL.BO.Drone drone = convertDalDroneToBl(dalObject.GetSpecificDrone(p.DroneID));
            return new IBL.BO.Parcel
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









        public void updateParcelName(int id, string model)
        {
            IDAL.DO.Drone drone = dalObject.GetSpecificDrone(id);
            drone.Model = model;
        }

        public static bool CheckLongIdIsValid(ulong id)
        {
            return id > 100000000 && id < 1000000000;
        }

        public static bool CheckValidIdCustomer(ulong id)
        {
            int sum = 0, digit;
            for (int i = 0; i < 9; i++)
            {
                digit = (int)(id % 10);
                if ((i % 2) == 0)
                {
                    sum += digit * 2;
                }
                else
                {
                    sum += digit;
                }
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
