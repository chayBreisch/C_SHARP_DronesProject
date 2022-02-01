using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
namespace Dal
{
    internal partial class DalObject : IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        internal static DalObject Instance;
        public static DalObject getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new DalObject();
                return Instance;
            }
        }
        //################################################################
        //functions that returns the basic data arrays from dataSource
        //################################################################
        /*public  IEnumerable <Drone> GetDrone()
        {
            List<Drone> drones = new List<Drone>();
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                drones.Add(DataSource.drones[i]);
            }
            return drones;
        }*/
        /*public  IEnumerable <Station> GetStation()
        {
            List<Station> stations = new List<Station>();
            for (int i = 0; i < DataSource.stations.Count; i++)
            {
                stations.Add(DataSource.stations[i]);
            }
            return stations;
        }*/
        /*public  IEnumerable <Customer> GetCustomer()
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < DataSource.customers.Count; i++)
            {
                customers.Add(DataSource.customers[i]);
            }
            return customers;
        }*/
        /*     public  IEnumerable <Parcel> GetParcial()
             {
                 List<Parcel> parcels = new List<Parcel>();
                 for (int i = 0; i < DataSource.parcels.Count; i++)
                 {
                     parcels.Add(DataSource.parcels[i]);
                 }
                 return parcels;
             }*/

        /* public  IEnumerable <DroneCharge> GetDroneCharge()
         {
             List<DroneCharge> droneChargers = new List<DroneCharge>();
             for (int i = 0; i < DataSource.droneChargers.Count; i++)
             {
                 droneChargers.Add(DataSource.droneChargers[i]);
             }
             return droneChargers;
         }*/
        //#######################################################################
        //functions that returns the specific item from the dataSource by ID
        //#######################################################################
        /*public  Drone GetSpecificDrone(int id)
        {
            try
            {
                return DataSource.drones.First(drone => drone.ID == id);
            }
            catch(ArgumentNullException e)
            {
                throw new Exeptions(id);
            }
        }*/

        /*public  Station GetSpecificStation(int id)
        {
            try
            {
                return DataSource.stations.First(station => station.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exeptions(id);
            }
        }*/
        /*public  Customer GetSpecificCustomer(int id)
        {
            try
            {
                return DataSource.customers.First(customer => customer.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exeptions(id);
            }
        }*/
        /*public  Parcel GetSpecificParcial(int id)
        {
            try
            {
                return DataSource.parcels.First(parcel => parcel.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exeptions(id);
            }
        }*/
        //###############################################################
        //functions that are adding an item to the dataSources array
        //###############################################################
        /*public  void AddDrone(int id, string model, WeightCatagories weight)
        //public void AddDrone(int id, string model, WeightCatagories weight, DroneStatus status, double battery)
        {
            Drone newDrone = new Drone();
            newDrone.ID = id;
            newDrone.Model = model;
            newDrone.MaxWeight = weight;
           *//* newDrone.Status = status;
            newDrone.Battery = battery;*//*
            DataSource.drones[DataSource.drones.Count - 1] = newDrone;
        }*/
        /*public  void AddStation(int id, int name, int longitude, int latitude, int chargeSlots)
        {
            Station newStation = new Station();
            newStation.ID = id;
            newStation.Name = name;
            newStation.Longitude = longitude;
            newStation.Latitude = latitude;
            newStation.ChargeSlots = chargeSlots;
            DataSource.stations[DataSource.stations.Count - 1] = newStation;
        }*/
        /*public  void AddCustomer(int id, string name, string phone, double latitude, double longitude)
        {
            Customer newCustomer = new Customer();
            newCustomer.ID = id;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = latitude;
            newCustomer.Longitude = longitude;
            DataSource.customers[DataSource.customers.Count - 1] = newCustomer;
        }*/
        /*public  void AddParcial(int id, int senderId, int targetId, WeightCatagories weight, Priorities priority)
        {
            Parcel newParcial = new Parcel();
            newParcial.ID = id;
            newParcial.SenderID = senderId;
            newParcial.TargetID = targetId;
            newParcial.Weight = weight;
            newParcial.Priority = priority;
            DataSource.parcels[DataSource.parcels.Count - 1] = newParcial;
        }*/

        //################################################
        //functions that update the dataSource array 
        //################################################
        /*public void updateConectDroneToParcial(int id)
        {
            Parcel newParcial = getParcelById(p => p.ID == id);
            Drone drone = new Drone();
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                *//* if (DataSource.drones[i].Status == DroneStatus.Available)
                 {
                     drone = DataSource.drones[i];
                     drone.Status = DroneStatus.Delivery;
                     break;
                 }*//*
            }
            newParcial.DroneID = drone.ID;
            int index1 = DataSource.drones.FindIndex(p => p.ID == newParcial.ID);
            int index = DataSource.drones.FindIndex(d => d.ID == drone.ID);
            DataSource.drones[index] = drone;
            DataSource.parcels[index1] = newParcial;
        }*/


        /* public void updateCollectParcialByDrone(int id)
         {

             Parcel newParcial = getParcelById(p => p.ID == id);
             if (newParcial.DroneID == 0)
             {
                 Console.WriteLine("you didnt conect a drone");
             }
             newParcial.PickedUp = DateTime.Now;

         }*/

        /*public void updateSupplyParcialToCustomer(int id)
        {
            Parcel newParcial = getParcelById(p => p.ID == id);

            //if (newParcial.PickedUp)////////////////////////////////////////////////////
            {
                Console.WriteLine("you didnt collect a drone");
            }
            newParcial.Delivered = DateTime.Now;
        }*/
        /*public void updateSendDroneToCharge(int droneId, int statoinId)
        {
            DroneCharge droneCharge = new DroneCharge();
            int numOfChargers = 0;
            Station station = getStationById(s => s.ID == statoinId);
            Drone newDrone = getDroneById(d => d.ID == droneId);
            numOfChargers = 0;
            for (int j = 0; j < DataSource.droneChargers.Count; j++)
            {
                if (station.ID == DataSource.droneChargers[j].StationID)
                    numOfChargers++;
            }
            if (numOfChargers < station.ChargeSlots)
            {
                droneCharge.DroneID = newDrone.ID;
                droneCharge.StationID = station.ID;

            }

            //newDrone.Status = DroneStatus.Maintenance;
            DataSource.droneChargers[DataSource.droneChargers.Count - 1] = droneCharge;
        }*/
        /*public void updateUnChargeDrone(int id)
        {
            Drone NewDrone = getDroneById(d => d.ID == id);
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
            *//*NewDrone.Status = DroneStatus.Available;
            NewDrone.Battery = 100;*//*
            int index1 = DataSource.drones.FindIndex(d => d.ID == NewDrone.ID);
            DataSource.drones[index1] = NewDrone;
        }*/



        //################################################
        //functions to show information
        //################################################


        /* public IEnumerable<Parcel> showParcelsWithoutoutDrone()
         {
             //IEnumerable<Parcel> parcels = GetParcel();
             return from parcel in GetParcel()
             where parcel.DroneID == 0
             select parcel;
         *//*    foreach (var parcel in parcels)
             {
                 if (parcel.DroneID == 0)
                 {
                     yield return parcel;
                 }
             }*//*
         }*/


        /*public IEnumerable<Station> showStationWithEmptyChargers()
        {
            int numOfChargers = 0;
            IEnumerable<Station> stations = GetStation();
            IEnumerable<DroneCharge> droneChargers = GetDroneCharge();

            foreach (var station in stations)
            {
                foreach (var droneCharge in droneChargers)
                {
                    if (station.ID == droneCharge.StationID)
                        numOfChargers++;
                }
                if (numOfChargers < station.ChargeSlots)
                {
                    yield return station;
                }
            }
        }*/




        public double[] RequestElectric()
        {
            double[] array = { DataSource.Config.Available, DataSource.Config.LightHeight, DataSource.Config.MidHeight, DataSource.Config.HeavyHeight, DataSource.Config.ChargingRate };
            return array;
        }

    }
}