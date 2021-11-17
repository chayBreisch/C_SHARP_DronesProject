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
    public class BL : Bl
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
            foreach (var drone in dalObject.GetDrone())
            {
                DroneBL droneBL = new DroneBL { ID = drone.ID, Model = drone.Model, weight = drone.MaxWeight };
                Parcel parcel = dalObject.GetParcelByDroneID(drone.ID);
                Customer customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);

                if (!parcel.Equals(null))
                {
                    if (!parcel.Scheduled.Equals(null) && parcel.Delivered.Equals(null))
                    {
                        droneBL.droneStatus = DroneStatus.Delivery;
                        if (parcel.PickedUp.Equals(null))
                        {
                            Station station = findClosestStation(new LocationBL { Latitude = customerSender.Latitude, Longitude = customerSender.Longitude });
                            droneBL.location = new LocationBL { Latitude = station.Latitude, Longitude = station.Longitude };
                        }
                        else
                        {
                            droneBL.location.Longitude = customerSender.Longitude;
                            droneBL.location.Latitude = customerSender.Latitude;
                        }
                        Customer customerReciever = dalObject.GetSpecificCustomer(parcel.TargetID);
                        double electricitySenderToReciever = calcElectry(new LocationBL { Longitude = customerSender.Longitude, Latitude = customerSender.Latitude }, new LocationBL { Longitude = customerReciever.Longitude, Latitude = customerReciever.Latitude }, (int)parcel.Weight);
                        Station station1 = stationWithMinDisAndEmptySlots(new LocationBL { Latitude = customerReciever.Latitude, Longitude = customerReciever.Longitude });
                        double electricityRecieverToCharger = calcElectry(new LocationBL { Longitude = customerReciever.Longitude, Latitude = customerReciever.Latitude }, new LocationBL { Longitude = station1.Longitude, Latitude = station1.Latitude }, 0);
                        int minEle = (int)Math.Round(electricitySenderToReciever + electricityRecieverToCharger);
                        droneBL.BatteryStatus = rand.Next(minEle, 100);
                    }
                }
                if (droneBL.droneStatus != DroneStatus.Delivery)
                {
                    droneBL.droneStatus = (DroneStatus)rand.Next(0, 2);
                }
                if (droneBL.droneStatus == DroneStatus.Maintenance)
                {
                    int length = dalObject.lengthStation();
                    Station station = dalObject.GetStation().ToList()[rand.Next(0, length + 1)];
                    droneBL.location = new LocationBL { Latitude = station.Latitude, Longitude = station.Longitude };
                    droneBL.BatteryStatus = rand.Next(0, 21);
                }
                else if (droneBL.droneStatus == DroneStatus.Available)
                {
                    List<Parcel> parcelBLsWithSuppliedParcel = dalObject.GetParcel().ToList().FindAll(p => !p.Delivered.Equals(null));
                    ParcelBL parcelBL = convertDalToParcelBL(parcelBLsWithSuppliedParcel[rand.Next(0, parcelBLsWithSuppliedParcel.Count)]);
                    Customer customer = dalObject.GetSpecificCustomer(parcelBL.Sender.ID);
                    droneBL.location = new LocationBL { Latitude = customer.Latitude, Longitude = customer.Longitude };
                    Station station1 = stationWithMinDisAndEmptySlots(droneBL.location);
                    double electry = calcElectry(new LocationBL { Longitude = station1.Longitude, Latitude = station1.Latitude }, droneBL.location, 0);
                    int minElectric = (int)Math.Round(electry);
                    droneBL.BatteryStatus = rand.Next(minElectric, 100);
                }
                droneBLList.Add(droneBL);
            }
        }


        
        //#############################################################
        //check validaion id
        //#############################################################
        public static void checkUniqeIdDrone(int id)
        {
            List<Drone> drones = new List<Drone>();
            drones.ForEach(d =>
            {
                if (d.ID == id)
                    throw new NotUniqeID(id, typeof(Drone));
            });
        }

        public static void checkUniqeIdParcel(int id)
        {
            List<Parcel> parcels = new List<Parcel>();
            parcels.ForEach(p =>
            {
                if (p.ID == id)
                    throw new NotUniqeID(id, typeof(Parcel));
            });
        }

        public static void checkUniqeIdCustomer(ulong id)
        {
            List<Customer> customers = new List<Customer>();
            customers.ForEach(c =>
            {
                if (c.ID == id)
                    throw new NotUniqeID(id, typeof(Customer));
            });
        }

        public static void checkUniqeIdStation(int id)
        {
            List<Station> stations = new List<Station>();
            stations.ForEach(s =>
            {
                if (s.ID == id)
                    throw new NotUniqeID(id, typeof(Station));
            });
        }
        public static void checkUniqeIdDroneCharge(int droneId, int stationId)
        {
            List<DroneCharge> droneCharges = new List<DroneCharge>();
            droneCharges.ForEach(d =>
            {
                if (d.DroneID == droneId && d.StationID == stationId)
                    throw new NotUniqeID(droneId, stationId, typeof(Station));
            });
        }

        //#############################################################
        //Add functions
        //#############################################################

        public void AddCustomer(ulong id, string name, int phone, LocationBL location)
        {
            checkUniqeIdCustomer(id);
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
            checkUniqeIdStation(id);
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
            checkUniqeIdDrone(id);
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
            parcel.Requesed = DateTime.Now;
            parcel.Scheduled = new DateTime();
            parcel.PickedUp = new DateTime();
            parcel.Delivered = new DateTime();
            parcel.drone = null;
            AddParcelToDal(sender, target, Weight, priority);

        }

        public void AddParcelToDal(/*ulong id,*/ ulong sender, ulong target, WeightCatagories Weight, Priorities priority)
        {
            Parcel parcel = new Parcel();
            parcel.ID = dalObject.lengthParcel() + 1;
            checkUniqeIdParcel(parcel.ID);
            parcel.SenderID = sender;
            parcel.TargetID = target;
            parcel.Weight = Weight;
            parcel.Priority = priority;
            dalObject.AddParcel(parcel);
        }

        public void addDroneCharge(int stationID, int droneID)
        {
            checkUniqeIdDroneCharge(droneID, stationID);
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
            IEnumerable<Customer> customers = dalObject.GetCustomer();
            List<CustomerBL> customers1 = new List<CustomerBL>();
            foreach (var customer in customers)
            {
                customers1.Add(convertDalCustomerToBl(customer));
            }
            return customers1;
        }

        public List<DroneBL> GetDroneBL()
        {

            IEnumerable<Drone> drones = dalObject.GetDrone();
            List<DroneBL> drone1 = new List<DroneBL>();
            foreach (var drone in drones)
            {
                drone1.Add(convertDalDroneToBl(drone));
            }
            return drone1;
        }

        public List<ParcelBL> GetParcelBL()
        {

            IEnumerable<Parcel> parcels = dalObject.GetParcel();
            List<ParcelBL> parcel1 = new List<ParcelBL>();
            foreach (var parcel in parcels)
            {
                parcel1.Add(convertDalToParcelBL(parcel));
            }
            return parcel1;
        }

        public List<StationBL> GetStationBL()
        {

            IEnumerable<Station> stations = dalObject.GetStation();
            List<StationBL> stations1 = new List<StationBL>();
            foreach (var station in stations)
            {
                stations1.Add(convertDalStationToBl(station));
            }
            return stations1;
        }

        /*//###########################################################
        //return specific item from dal
        //#############################################################
        public static void GetSpecificDroneBL(int id)
        {

        }*/

        //#############################################################
        //Get specific item functions
        //#############################################################
        public DroneBL getSpecificDroneBLFromList(int id)
        {
            try
            {
                return droneBLList.Find(drone => drone.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Drone));
            }
        }

        public DroneBL GetSpecificDroneBL(int id)
        {
            return getSpecificDroneBLFromList(id);
        }

        public StationBL GetSpecificStationBL(int id)
        {
            try
            {
                return convertDalStationToBl(dalObject.GetSpecificStation(id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Station));

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
                throw new NotExistObjWithID(id, typeof(Parcel));
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
                throw new NotExistObjWithID(id, typeof(Customer));

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
            IEnumerable<Station> stations = dalObject.GetStation();
            List<StationBL> stations1 = new List<StationBL>();
            foreach (var station in stations)
            {
                if (getIfEmptyChargers(station))
                    stations1.Add(convertDalStationToBl(station));
            }
            return stations1;
        }

        public bool getIfEmptyChargers(Station station)
        {
            IEnumerable<DroneCharge> droneChargers = dalObject.GetDroneCharge();
            int numOfChargers = 0;
            foreach (var droneCharge in droneChargers)
            {
                if (station.ID == droneCharge.StationID)
                    numOfChargers++;
            }
            if (numOfChargers < station.ChargeSlots)
                return true;
            return false;
        }

        //######################################################################
        //convert from IDAL.DO to IBL.BO functions
        //######################################################################
        private StationBL convertDalStationToBl(Station s)
        {
            List<DroneCharge> droneChargers = dalObject.GetDroneCharge().Cast<DroneCharge>().ToList();
            droneChargers = droneChargers.FindAll(d => d.StationID == s.ID);
            List<DroneInCharger> dronesInCharges = new List<DroneInCharger>();
            droneChargers.ForEach(d => dronesInCharges.Add(new DroneInCharger
            { ID = d.DroneID, BatteryStatus = getSpecificDroneBLFromList(d.DroneID).BatteryStatus }));
            return new StationBL
            {
                ID = s.ID,
                Name = s.Name,
                ChargeSlots = s.ChargeSlots,
                location = new LocationBL() { Longitude = s.Longitude, Latitude = s.Latitude },
                dronesInCharge = dronesInCharges
            };
        }

        private CustomerBL convertDalCustomerToBl(Customer c)
        {
            List<ParcelAtCustomer> parcelSendedByCustomers = new List<ParcelAtCustomer>();
            List<ParcelAtCustomer> parcelSendedToCustomers = new List<ParcelAtCustomer>();
            List<Parcel> parcels = dalObject.GetParcel().Cast<Parcel>().ToList();
            parcels.ForEach(p =>
            {
                if (p.SenderID == c.ID)
                    parcelSendedByCustomers.Add(new ParcelAtCustomer { ID = p.ID, weightCatagories = p.Weight, priorities = p.Priority, parcelStatus = findParcelStatus(p) });
                if (p.TargetID == c.ID)
                    parcelSendedToCustomers.Add(new ParcelAtCustomer { ID = p.ID, weightCatagories = p.Weight, priorities = p.Priority, parcelStatus = findParcelStatus(p) });

            });

            return new CustomerBL
            {
                ID = c.ID,
                Name = c.Name,
                Phone = c.Phone,
                location = new LocationBL() { Longitude = c.Longitude, Latitude = c.Latitude },
                parcelSendedByCustomer = parcelSendedByCustomers,
                parcelSendedToCustomer = parcelSendedToCustomers
            };
        }

        private DroneBL convertDalDroneToBl(Drone d)
        {
            return GetSpecificDroneBL(d.ID);
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
                priorities = p.Priority,
                PickedUp = p.PickedUp,
                drone = drone,
                Requesed = p.Requested,
                Scheduled = p.Scheduled,
                Delivered = p.Delivered,
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
            DroneBL droneBl = getSpecificDroneBLFromList(id);
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
            DroneBL droneBL = getSpecificDroneBLFromList(id);
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
                double electric = calcElectry(droneBL.location, new LocationBL { Latitude = station.Latitude, Longitude = station.Longitude }, 0);

                if (electric > droneBL.BatteryStatus)
                {
                    throw new DAL.Exceptions("dont have enough battery");
                }
                if (droneBL.droneStatus == DroneStatus.Available)
                {
                    throw new Exception("there is no a station with empty charge slots");
                }
                droneBL.BatteryStatus -= electric;
                droneBL.location.Latitude = station.Latitude;
                droneBL.location.Longitude = station.Longitude;
                droneBL.droneStatus = DroneStatus.Maintenance;
                updateDrone(droneBL);
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
                droneBL = droneBLList.Find(d => d.ID == id && d.droneStatus != DroneStatus.Maintenance);
            }
            catch (ArgumentNullException e)
            {
                throw new Exception($"{e} can not uncharge drone");
            }

            droneBL.BatteryStatus += timeInCharge * dalObject.requestElectric()[4];
            droneBL.droneStatus = DroneStatus.Available;
            DroneCharge droneCharge = dalObject.getSpecificDroneChargeByDroneID(droneBL.ID);
            try
            {
                Station station = dalObject.GetSpecificStation(droneCharge.StationID);
                station.ChargeSlots += 1;
                dalObject.updateStation(station);
            }
            catch (ArgumentNullException e)
            {
                throw new Exception($"{e} can not find staion ");
            }
            dalObject.removeDroneCharge(droneCharge);
        }

        public void updateConnectParcelToDrone(int id)////////////////////////////////////לנסות לייעל
        {
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            if (droneBL.droneStatus != DroneStatus.Available)
            {
                throw new Exception("the drone is not free");
            }
            Customer customerSender, customerCurrent, customerReciever;
            Parcel currentParcel = new Parcel();
            List<Parcel> parcels = getParcelsWithoutoutDrone();
            currentParcel = new Parcel() { Weight = 0 };
            foreach (var parcel in parcels)
            {
                if (parcel.Requested.Equals(null))
                    break;
                customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
                customerCurrent = dalObject.GetSpecificCustomer(currentParcel.SenderID);
                customerReciever = dalObject.GetSpecificCustomer(parcel.TargetID);
                double disDroneToSenderParcel = distance(droneBL.location, new LocationBL { Longitude = customerSender.Longitude, Latitude = customerSender.Latitude });
                double disSenderToReciever = distance(new LocationBL { Longitude = customerSender.Longitude, Latitude = customerSender.Latitude }, new LocationBL { Longitude = customerReciever.Longitude, Latitude = customerReciever.Latitude });
                double electricity = dalObject.requestElectric()[(int)parcel.Weight];
                Station station = stationWithMinDisAndEmptySlots(new LocationBL { Latitude = customerReciever.Latitude, Longitude = customerReciever.Longitude });
                double disRecieverToCharger = distance(new LocationBL { Longitude = customerReciever.Longitude, Latitude = customerReciever.Latitude }, new LocationBL { Longitude = station.Longitude, Latitude = station.Latitude });
                if ((droneBL.BatteryStatus - (electricity * disDroneToSenderParcel + electricity * disSenderToReciever + disRecieverToCharger * dalObject.requestElectric()[0]) > 0) || (parcel.Weight < droneBL.weight))
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
                            if (disDroneToSenderParcel < distance(droneBL.location, new LocationBL { Longitude = customerCurrent.Longitude, Latitude = customerCurrent.Latitude }))
                            {
                                currentParcel = parcel;
                            }
                        }
                    }
                }
            }
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

        public void updateCollectParcelByDrone(int id)
        {
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            Parcel parcel = new Parcel();
            if (droneBL.droneStatus == DroneStatus.Delivery)
            {
                parcel = dalObject.GetSpecificParcelByDroneID(droneBL.ID);
                if (!parcel.PickedUp.Equals(null) || parcel.Scheduled.Equals(null))
                {
                    throw new Exception("can't collect parcel");
                }

            }
            Customer customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
            droneBL.BatteryStatus -= calcElectry(droneBL.location, new LocationBL { Longitude = customerSender.Longitude, Latitude = customerSender.Latitude }, (int)parcel.Weight);
            droneBL.location = new LocationBL { Longitude = customerSender.Longitude, Latitude = customerSender.Latitude };
            updateDrone(droneBL);
            parcel.PickedUp = DateTime.Now;
            dalObject.updateParcel(parcel);
        }

        public void supplyParcelByDrone(int id)
        {
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            Parcel parcel = dalObject.GetSpecificParcelByDroneID(id);
            if (parcel.PickedUp.Equals(null) && !parcel.Delivered.Equals(null))
            {
                throw new Exception("can't supply parcel");
            }
            Customer customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
            Customer customerReciever = dalObject.GetSpecificCustomer(parcel.TargetID);
            double electricSenderToReciever = calcElectry(new LocationBL { Longitude = customerSender.Longitude, Latitude = customerSender.Latitude }, new LocationBL { Longitude = customerReciever.Longitude, Latitude = customerReciever.Latitude }, (int)parcel.Weight);
            droneBL.BatteryStatus -= electricSenderToReciever;
            droneBL.location = new LocationBL { Longitude = customerReciever.Longitude, Latitude = customerReciever.Latitude };
            droneBL.droneStatus = DroneStatus.Available;
            updateDrone(droneBL);
            parcel.Delivered = DateTime.Now;
            dalObject.updateParcel(parcel);
        }

        //###################################################################
        //help functions
        //###################################################################

        public double calcElectry(LocationBL locatin1, LocationBL location2, int weight)
        {
            double distance1 = distance(locatin1, location2);
            return distance1 * dalObject.requestElectric()[weight];
        }

        private ParcelStatus findParcelStatus(Parcel parcel)
        {
            if (parcel.Requested.Equals(null))
                return (ParcelStatus)0;
            else if (parcel.Scheduled.Equals(null))
                return (ParcelStatus)1;
            else if (parcel.PickedUp.Equals(null))
                return (ParcelStatus)2;
            return (ParcelStatus)3;
        }

        public Station stationWithMinDisAndEmptySlots(LocationBL location)////////////////////////////
        {
            double minDis = -1;
            double dis2 = 0;
            IEnumerable<Station> stations = dalObject.GetStation();
            Station sendStation = new Station();
            foreach (var station in stations)
            {
                dis2 = distance(location, new LocationBL { Latitude = station.Latitude, Longitude = station.Longitude });
                if (dis2 < minDis || minDis == -1 && station.ChargeSlots != 0)
                {
                    minDis = dis2;
                    sendStation = station;
                }
            }
            return sendStation;
        }

        public Station findClosestStation(LocationBL location)
        {
            double minDis = -1;
            double dis2 = 0;
            IEnumerable<Station> stations = dalObject.GetStation();
            Station sendStation = new Station();
            foreach (var station in stations)
            {
                dis2 = distance(location, new LocationBL { Latitude = station.Latitude, Longitude = station.Longitude });
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

        static double distance(LocationBL location1, LocationBL location2)
        {
            return Math.Sqrt(Math.Pow(location2.Longitude - location1.Longitude, 2) +
            Math.Pow(location2.Latitude - location1.Latitude, 2) * 1.0);
        }

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
