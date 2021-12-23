using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;
/// <summary>
/// //////////////////////////////////////////////add commands//////////////////////////////
/// </summary>
namespace BL
{
    internal partial class BL
    {
        /// <summary>
        /// send a drone a charger in a station
        /// </summary>
        /// <param name="id"></param>
        public BO.Drone updateSendDroneToCharge(int id)
        {
            //check this function////////////////////////////////
            DO.Station station = new DO.Station();
            BO.Drone droneBL = getSpecificDroneBLFromList(id);
            //find a station to charge
            if (droneBL.DroneStatus == DroneStatus.Available)
            {
                station = stationWithMinDisAndEmptySlots(droneBL.Location);
                double electric = calcElectry(droneBL.Location, new LocationBL(station.Latitude, station.Longitude), 0);
                //check if enough electricity to reach the station
                if (electric > droneBL.BatteryStatus)
                {
                    throw new CanNotUpdateDrone(droneBL.ID, "dont have enough battery");
                }
                /* if (droneBL.DroneStatus == DroneStatus.Available)//////////////////יש פה טעןת///////////////////////////////
                 {
                     throw new Exception("there is no a station with empty charge slots");
                 }*/
                droneBL.BatteryStatus -= electric;
                droneBL.Location.Latitude = station.Latitude;
                droneBL.Location.Longitude = station.Longitude;
                droneBL.DroneStatus = DroneStatus.Maintenance;
                updateDrone(droneBL);
                station.ChargeSlots -= 1;
                dalObject.updateStation(station);
                DroneCharge droneCharge = new DroneCharge();
                droneCharge.DroneID = id;
                droneCharge.StationID = station.ID;
                dalObject.AddDroneCharge(droneCharge);

            }
            return droneBL;
        }

        /// <summary>
        /// uncharge a drone from the charger
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timeInCharge"></param>
        public BO.Drone updateUnchargeDrone(int id, double timeInCharge)
        {
            BO.Drone droneBL = new BO.Drone();
            try
            {
                droneBL = droneBLList.Find(d => d.ID == id && d.DroneStatus == DroneStatus.Maintenance);
            }
            catch (ArgumentNullException e)
            {
                throw new CanNotUpdateDrone(id, "can not uncharge drone");
            }
            if (droneBL == null)
                throw new CanNotUpdateDrone(id, "can not uncharge drone");
            double battery = droneBL.BatteryStatus + timeInCharge * dalObject.requestElectric()[4];
            if (battery < 100)
                droneBL.BatteryStatus = battery;
            else
                droneBL.BatteryStatus = 100;
            droneBL.DroneStatus = DroneStatus.Available;
            updateDrone(droneBL);
            DroneCharge droneCharge = dalObject.getDroneChargeById(d => d.DroneID == droneBL.ID);
            /*try/////////////////////לכאורה מיותר כי כבר בדקתי שהרחפן בהטענה אז חייב להיות מטען ותחנה
            {*/
            DO.Station station = dalObject.getStationById(s => s.ID == droneCharge.StationID);
            station.ChargeSlots += 1;
            dalObject.updateStation(station);
            /*}
            catch (ArgumentNullException e)
            {
                throw new Exception($"{e} can not find station ");
            }*/
            dalObject.removeDroneCharge(droneCharge);
            return droneBL;
        }

        /// <summary>
        /// connect a parcel to the drone
        /// </summary>
        /// <param name="id"></param>
        public void updateConnectParcelToDrone(int id)
        {
            BO.Drone droneBL = getSpecificDroneBLFromList(id);
            if (droneBL.DroneStatus != DroneStatus.Available)
            {
                throw new CanNotUpdateDrone(id, "the drone is not free");
            }
            DO.Customer customerSender, customerCurrent, customerReciever;
            DO.Parcel currentParcel = new DO.Parcel();
            IEnumerable<DO.Parcel> parcels = getParcelsWithoutoutDrone();
            currentParcel = new DO.Parcel() { Weight = 0 };
            foreach (var parcel in parcels)
            {
                if (parcel.Requested == null)
                    break;
                if (parcel.Scheduled != null)
                    break;
                customerSender = dalObject.getCustomerById(p => p.ID == parcel.SenderID);
                customerCurrent = dalObject.getCustomerById(c => c.ID == currentParcel.SenderID);
                customerReciever = dalObject.getCustomerById(c => c.ID == parcel.TargetID);
                double disDroneToSenderParcel = distance(droneBL.Location, new LocationBL(customerSender.Longitude, customerSender.Latitude));
                double electricity = dalObject.requestElectric()[(int)parcel.Weight];
                DO.Station station = stationWithMinDisAndEmptySlots(new LocationBL(customerReciever.Latitude, customerReciever.Longitude));
                double electricSenderToReciever = calcElectry(new LocationBL(customerSender.Longitude, customerSender.Latitude), new LocationBL(customerReciever.Longitude, customerReciever.Latitude), (int)parcel.Weight);
                double electricRecieverToCharger = calcElectry(new LocationBL(customerReciever.Longitude, customerReciever.Latitude), new LocationBL(station.Longitude, station.Latitude), 0);
                //check if the drone has enough battery to reach the reciever
                if (((droneBL.BatteryStatus - (electricity * disDroneToSenderParcel + electricSenderToReciever + electricRecieverToCharger)) > 0) || (parcel.Weight < droneBL.Weight))
                {
                    //check what is the best parcel to take
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
                            if (disDroneToSenderParcel < distance(droneBL.Location, new LocationBL(customerCurrent.Longitude, customerCurrent.Latitude)))
                            {
                                currentParcel = parcel;
                            }
                        }
                    }
                }
            }
            //if did not find a good parcel
            if (currentParcel.Weight == 0)
            {
                throw new CanNotUpdateDrone(id, "didn't find a parcel for your drone");
            }
            droneBL.DroneStatus = DroneStatus.Delivery;
            updateDrone(droneBL);
            currentParcel.DroneID = id;
            currentParcel.Scheduled = DateTime.Now;
            dalObject.updateParcel(currentParcel);
        }

        /// <summary>
        /// collect a parcel with the drone
        /// </summary>
        /// <param name="id"></param>
        public void updateCollectParcelByDrone(int id)
        {
            BO.Drone droneBL = getSpecificDroneBLFromList(id);
            DO.Parcel parcel = new DO.Parcel();
            parcel = dalObject.getParcelById(p => p.DroneID == droneBL.ID);
            if (parcel.DroneID == 0)
                throw new CanNotUpdateDrone(id, "drone is didn't connect to a parcel");
            if (droneBL.DroneStatus == DroneStatus.Delivery)
            {
                //check if can collect the parcel
                if (parcel.PickedUp != null)
                {
                    throw new CanNotUpdateDrone(id, "can't collect parcel because parcel is picked up");
                }

            }
            DO.Customer customerSender = dalObject.getCustomerById(c => c.ID == parcel.SenderID);
            droneBL.BatteryStatus -= calcElectry(droneBL.Location, new LocationBL(customerSender.Longitude, customerSender.Latitude), (int)parcel.Weight);
            droneBL.Location = new LocationBL(customerSender.Longitude, customerSender.Latitude);
            droneBL.DroneStatus = DroneStatus.Delivery;//////////////////////////////////////
            updateDrone(droneBL);
            parcel.PickedUp = DateTime.Now;
            dalObject.updateParcel(parcel);
        }

        /// <summary>
        /// supply a parcel with the drone
        /// </summary>
        /// <param name="id"></param>
        public void updateSupplyParcelByDrone(int id)
        {
           BO.Drone droneBL = getSpecificDroneBLFromList(id);
            DO.Parcel parcel = dalObject.getParcelById(p=> p.DroneID == id);
            //check if can supply the parcel
            if (parcel.Delivered != null)
                throw new CanNotUpdateDrone(id, "parcel is delivered already");
            if (parcel.PickedUp == null && parcel.Delivered != null)
            {
                throw new CanNotUpdateDrone(id, "can't supply parcel because didn't picked up or delieverd");
            }
            DO.Customer customerSender = dalObject.getCustomerById(c => c.ID == parcel.SenderID);
            DO.Customer customerReciever = dalObject.getCustomerById(c=> c.ID == parcel.TargetID);
            ParcelInDelivery parcelInDelivery = new ParcelInDelivery(convertDalToParcelBL(parcel), dalObject);
            droneBL.parcelInDelivery = parcelInDelivery;
            double electricSenderToReciever = calcElectry(new LocationBL(customerSender.Longitude, customerSender.Latitude), new LocationBL(customerReciever.Longitude, customerReciever.Latitude), (int)parcel.Weight);
            droneBL.BatteryStatus -= electricSenderToReciever;
            droneBL.Location = new LocationBL(customerReciever.Longitude, customerReciever.Latitude);
            droneBL.DroneStatus = DroneStatus.Available;
            updateDrone(droneBL);
            parcel.Delivered = DateTime.Now;
            dalObject.updateParcel(parcel);
        }
    }
}
