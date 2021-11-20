using IBL.BO;
using IDAL.DO;
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
    public partial class BL
    {
        /// <summary>
        /// send a drone a charger in a station
        /// </summary>
        /// <param name="id"></param>
        public void updateSendDroneToCharge(int id)
        {
            //check this function////////////////////////////////
            Station station = new Station();
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            //find a station to charge
            if (droneBL.DroneStatus == DroneStatus.Available)
            {
                /*for (int i = 0; i < dalObject.lengthStation(); i++)
                {*/
                station = stationWithMinDisAndEmptySlots(droneBL.Location);
                /*if (station.ChargeSlots != 0)
                {
                    break;
                }*/
                /*}*/
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
        }

        /// <summary>
        /// uncharge a drone from the charger
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timeInCharge"></param>
        public void updateUnchargeDrone(int id, double timeInCharge)
        {
            DroneBL droneBL = new DroneBL();
            try
            {
                droneBL = droneBLList.Find(d => d.ID == id && d.DroneStatus != DroneStatus.Maintenance);
            }
            catch (ArgumentNullException e)
            {
                throw new CanNotUpdateDrone(id, "can not uncharge drone");
            }

            droneBL.BatteryStatus += timeInCharge * dalObject.requestElectric()[4];
            droneBL.DroneStatus = DroneStatus.Available;
            DroneCharge droneCharge = dalObject.getSpecificDroneChargeByDroneID(droneBL.ID);
            /*try/////////////////////לכאורה מיותר כי כבר בדקתי שהרחפן בהטענה אז חייב להיות מטען ותחנה
            {*/
                Station station = dalObject.GetSpecificStation(droneCharge.StationID);
                station.ChargeSlots += 1;
                dalObject.updateStation(station);
            /*}
            catch (ArgumentNullException e)
            {
                throw new Exception($"{e} can not find station ");
            }*/
            dalObject.removeDroneCharge(droneCharge);
        }

        /// <summary>
        /// connect a parcel to the drone
        /// </summary>
        /// <param name="id"></param>
        public void updateConnectParcelToDrone(int id)////////////////////////////////////לנסות לייעל
        {
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            if (droneBL.DroneStatus != DroneStatus.Available)
            {
                throw new CanNotUpdateDrone(id, "the drone is not free");
            }
            Customer customerSender, customerCurrent, customerReciever;
            Parcel currentParcel = new Parcel();
            List<Parcel> parcels = getParcelsWithoutoutDrone();
            currentParcel = new Parcel() { Weight = 0 };
            foreach (var parcel in parcels)
            {
                if (parcel.Requested == new DateTime())
                    break;
                customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
                customerCurrent = dalObject.GetSpecificCustomer(currentParcel.SenderID);
                customerReciever = dalObject.GetSpecificCustomer(parcel.TargetID);
                double disDroneToSenderParcel = distance(droneBL.Location, new LocationBL(customerSender.Longitude, customerSender.Latitude));
                double electricity = dalObject.requestElectric()[(int)parcel.Weight];
                Station station = stationWithMinDisAndEmptySlots(new LocationBL(customerReciever.Latitude, customerReciever.Longitude));
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
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            Parcel parcel = new Parcel();
            if (droneBL.DroneStatus == DroneStatus.Delivery)
            {
                parcel = dalObject.GetSpecificParcelByDroneID(droneBL.ID);
                //check if can collect the parcel
                if (parcel.PickedUp != new DateTime() || parcel.Scheduled == new DateTime())
                {
                    throw new CanNotUpdateDrone(id, "can't collect parcel because parcel is picked up or didn't scheduled");
                }

            }
            Customer customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
            droneBL.BatteryStatus -= calcElectry(droneBL.Location, new LocationBL(customerSender.Longitude, customerSender.Latitude), (int)parcel.Weight);
            droneBL.Location = new LocationBL(customerSender.Longitude, customerSender.Latitude);
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
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            Parcel parcel = dalObject.GetSpecificParcelByDroneID(id);
            //check if can supply the parcel
            if (parcel.PickedUp == new DateTime() && parcel.Delivered != new DateTime())
            {
                throw new CanNotUpdateDrone(id, "can't supply parcel because didn't pickrd up or delieverd");
            }
            Customer customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
            Customer customerReciever = dalObject.GetSpecificCustomer(parcel.TargetID);
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
