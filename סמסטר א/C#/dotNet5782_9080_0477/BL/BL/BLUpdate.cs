using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;
using System.Runtime.CompilerServices;
namespace BL
{
    internal partial class BL
    {
        /// <summary>
        /// send a drone a charger in a station
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone UpdateSendDroneToCharge(int id)
        {
            lock (dalObject)
            {
                BO.Station station = new BO.Station();
                BO.Drone droneBL = GetSpecificDroneBL(id);
                //find a station to charge
                if (droneBL.DroneStatus == DroneStatus.Available)
                {
                    station = stationWithMinDisAndEmptySlots(droneBL.Location);
                    double electric = calcElectry(droneBL.Location, station.Location, 0);
                    //check if enough electricity to reach the station
                    if (electric > droneBL.BatteryStatus)
                        throw new CanNotUpdateDrone(droneBL.ID, "dont have enough battery");
                    droneBL.BatteryStatus -= electric;
                    droneBL.Location = station.Location;
                    droneBL.DroneStatus = DroneStatus.Maintenance;
                    updateDrone(droneBL);
                    station.ChargeSlots -= 1;
                    UpdateStation(station);
                    DroneCharge droneCharge = new DroneCharge { DroneID = id, StationID = station.ID };
                    addDroneCharge(station.ID, id);
                }
                return droneBL;
            }
        }

        /// <summary>
        /// uncharge a drone from the charger
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timeInCharge"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone UpdateUnchargeDrone(int id, double timeInCharge)
        {
            lock (dalObject)
            {
                BO.Drone droneBL = new BO.Drone();
                try
                {
                    droneBL = droneBLList.Find(d => d.ID == id && d.DroneStatus == DroneStatus.Maintenance);
                }
                catch (ArgumentNullException e)
                {
                    throw new CanNotUpdateDrone(id, "can not uncharge drone", e);
                }
                if (droneBL == null)
                    throw new CanNotUpdateDrone(id, "can not uncharge drone");
                double battery = droneBL.BatteryStatus + timeInCharge * dalObject.RequestElectric()[4];
                if (battery < 100)
                    droneBL.BatteryStatus = battery;
                else
                    droneBL.BatteryStatus = 100;
                droneBL.DroneStatus = DroneStatus.Available;
                updateDrone(droneBL);
                DroneCharge droneCharge = dalObject.GetDroneChargeById(d => d.DroneID == droneBL.ID);
                DO.Station station = dalObject.GetStationById(s => s.ID == droneCharge.StationID);
                station.ChargeSlots += 1;
                dalObject.UpdateStation(station);
                dalObject.RemoveDroneCharge(droneCharge);
                return droneBL;
            }
        }

        /// <summary>
        /// connect a parcel to the drone
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateConnectParcelToDrone(int id)
        {
            int count = 0;
            lock (dalObject)
            {
                BO.Drone droneBL = GetSpecificDroneBL(id);
                if (droneBL.DroneStatus != DroneStatus.Available)
                {
                    throw new CanNotUpdateDrone(id, "the drone is not free");
                }
                DO.Customer customerSender, customerCurrent, customerReciever;
                IEnumerable<DO.Parcel> parcels = dalObject.GetParcelesByCondition(p => p.Scheduled == null && p.Requested != null);
                DO.Parcel currentParcel = new DO.Parcel() { Weight = 0 };
                foreach (var parcel in parcels)
                {
                    if (parcel.Weight <= droneBL.Weight)
                        count++;
                    customerSender = dalObject.GetCustomerById(p => p.ID == parcel.SenderID);
                    customerCurrent = dalObject.GetCustomerById(c => c.ID == currentParcel.SenderID);
                    customerReciever = dalObject.GetCustomerById(c => c.ID == parcel.TargetID);
                    double disDroneToSenderParcel = distance(droneBL.Location, new LocationBL(customerSender.Longitude, customerSender.Latitude));
                    double electricity = dalObject.RequestElectric()[(int)parcel.Weight];
                    BO.Station station = stationWithMinDisAndEmptySlots(new LocationBL(customerReciever.Latitude, customerReciever.Longitude));
                    double electricSenderToReciever = calcElectry(new LocationBL(customerSender.Longitude, customerSender.Latitude), new LocationBL(customerReciever.Longitude, customerReciever.Latitude), (int)parcel.Weight);
                    double electricRecieverToCharger = calcElectry(new LocationBL(customerReciever.Longitude, customerReciever.Latitude), station.Location, 0);
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
                if (count == 0)
                    throw new NoParcelsToDeliver();
                if (currentParcel.Weight == 0)
                    throw new CanNotUpdateDrone(id, "didn't find a parcel for your drone");
                droneBL.DroneStatus = DroneStatus.Delivery;
                droneBL.parcelInDelivery = new ParcelInDelivery(GetSpecificParcelBL(currentParcel.ID), dalObject);
                updateDrone(droneBL);
                currentParcel.DroneID = id;
                currentParcel.Scheduled = DateTime.Now;
                dalObject.UpdateParcel(currentParcel);
            }
        }

        /// <summary>
        /// collect a parcel with the drone
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCollectParcelByDrone(int id)
        {
            lock (dalObject)
            {
                BO.Drone droneBL = GetSpecificDroneBLWithDeleted(id);
                DO.Parcel parcel = dalObject.GetParcelBy(p => p.DroneID == droneBL.ID);
                if (parcel.DroneID == 0)
                    throw new CanNotUpdateDrone(id, "drone is didn't connect to a parcel");
                if (droneBL.DroneStatus != DroneStatus.Delivery && parcel.PickedUp != null)
                    throw new CanNotUpdateDrone(id, "can't collect parcel because parcel is picked up");
                LocationBL customerSenderLocation = droneBL.parcelInDelivery.CollectLocation;
                droneBL.BatteryStatus -= calcElectry(droneBL.Location, customerSenderLocation, (int)parcel.Weight);
                droneBL.Location = customerSenderLocation;
                droneBL.DroneStatus = DroneStatus.Delivery;
                droneBL.parcelInDelivery.isWaiting = false;
                updateDrone(droneBL);
                parcel.PickedUp = DateTime.Now;
                dalObject.UpdateParcel(parcel);
            }
        }

        /// <summary>
        /// supply a parcel with the drone
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateSupplyParcelByDrone(int id)
        {
            lock (dalObject)
            {
                BO.Drone droneBL = GetSpecificDroneBLWithDeleted(id);
                DO.Parcel parcel = dalObject.GetParcelBy(p => p.DroneID == id);
                if (parcel.PickedUp == null && parcel.Delivered != null)
                    throw new CanNotUpdateDrone(id, "can't supply parcel because didn't picked up or delieverd");
                LocationBL customerSenderLocation = droneBL.parcelInDelivery.CollectLocation;
                LocationBL customerRecieverReciever = droneBL.parcelInDelivery.TargetLocation;
                double electricSenderToReciever = calcElectry(customerSenderLocation, customerRecieverReciever, (int)parcel.Weight);
                droneBL.BatteryStatus -= electricSenderToReciever;
                droneBL.Location = customerRecieverReciever;
                droneBL.DroneStatus = DroneStatus.Available;
                droneBL.parcelInDelivery = null;
                updateDrone(droneBL);
                parcel.Delivered = DateTime.Now;
                dalObject.UpdateParcel(parcel);
            }
        }
    }
}
