using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// //////////////////////////////////////////////add commands//////////////////////////////
/// </summary>
namespace BL
{
    public partial class BL
    {
        public void updateSendDroneToCharge(int id)
        {
            //check this function////////////////////////////////
            Station station = new Station();
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            if (droneBL.DroneStatus == DroneStatus.Available)
            {
                for (int i = 0; i < dalObject.lengthStation(); i++)
                {
                    station = stationWithMinDisAndEmptySlots(droneBL.Location);
                    if (station.ChargeSlots != 0)
                    {
                        break;
                    }
                }
                double electric = calcElectry(droneBL.Location, new LocationBL(station.Latitude, station.Longitude), 0);

                if (electric > droneBL.BatteryStatus)
                {
                    throw new DAL.Exceptions("dont have enough battery");
                }
                if (droneBL.DroneStatus == DroneStatus.Available)
                {
                    throw new Exception("there is no a station with empty charge slots");
                }
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

        public void updateUnchargeDrone(int id, double timeInCharge)
        {
            DroneBL droneBL = new DroneBL();
            try
            {
                droneBL = droneBLList.Find(d => d.ID == id && d.DroneStatus != DroneStatus.Maintenance);
            }
            catch (ArgumentNullException e)
            {
                throw new Exception($"{e} can not uncharge drone");
            }

            droneBL.BatteryStatus += timeInCharge * dalObject.requestElectric()[4];
            droneBL.DroneStatus = DroneStatus.Available;
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
            if (droneBL.DroneStatus != DroneStatus.Available)
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
                double disDroneToSenderParcel = distance(droneBL.Location, new LocationBL(customerSender.Longitude, customerSender.Latitude));
                double disSenderToReciever = distance(new LocationBL(customerSender.Longitude, customerSender.Latitude), new LocationBL(customerReciever.Longitude, customerReciever.Latitude));
                double electricity = dalObject.requestElectric()[(int)parcel.Weight];
                Station station = stationWithMinDisAndEmptySlots(new LocationBL(customerReciever.Latitude, customerReciever.Longitude));
                double disRecieverToCharger = distance(new LocationBL(customerReciever.Longitude, customerReciever.Latitude), new LocationBL(station.Longitude, station.Latitude));
                if ((droneBL.BatteryStatus - (electricity * disDroneToSenderParcel + electricity * disSenderToReciever + disRecieverToCharger * dalObject.requestElectric()[0]) > 0) || (parcel.Weight < droneBL.Weight))
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
                            if (disDroneToSenderParcel < distance(droneBL.Location, new LocationBL(customerCurrent.Longitude, customerCurrent.Latitude)))
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
            droneBL.DroneStatus = DroneStatus.Delivery;
            updateDrone(droneBL);
            currentParcel.DroneID = id;
            currentParcel.Scheduled = DateTime.Now;
            dalObject.updateParcel(currentParcel);
        }

        public void updateCollectParcelByDrone(int id)
        {
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            Parcel parcel = new Parcel();
            if (droneBL.DroneStatus == DroneStatus.Delivery)
            {
                parcel = dalObject.GetSpecificParcelByDroneID(droneBL.ID);
                if (!parcel.PickedUp.Equals(null) || parcel.Scheduled.Equals(null))
                {
                    throw new Exception("can't collect parcel");
                }

            }
            Customer customerSender = dalObject.GetSpecificCustomer(parcel.SenderID);
            droneBL.BatteryStatus -= calcElectry(droneBL.Location, new LocationBL(customerSender.Longitude, customerSender.Latitude), (int)parcel.Weight);
            droneBL.Location = new LocationBL(customerSender.Longitude, customerSender.Latitude);
            updateDrone(droneBL);
            parcel.PickedUp = DateTime.Now;
            dalObject.updateParcel(parcel);
        }

        public void updateSupplyParcelByDrone(int id)
        {
            DroneBL droneBL = getSpecificDroneBLFromList(id);
            Parcel parcel = dalObject.GetSpecificParcelByDroneID(id);
            if (parcel.PickedUp.Equals(null) && !parcel.Delivered.Equals(null))
            {
                throw new Exception("can't supply parcel");
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
