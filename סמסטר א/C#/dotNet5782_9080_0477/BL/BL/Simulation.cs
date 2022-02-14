﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlApi;
using static BL.ExceptionsBL;
using BO;
using System.ComponentModel;

namespace BL
{
    class Simulation
    {
        const int DELAY = 1000;
        const int SPEED = 60;
        //IBL bl;
        public Simulation(IBL bl, Drone drone, BackgroundWorker worker, Action<Drone> updateDrone, Func<bool> needToStop)
        {
            while (!needToStop())
            {
                switch (drone.DroneStatus)
                {
                    case DroneStatus.Available:
                        try
                        {
                            bl.UpdateConnectParcelToDrone(drone.ID);
                        }
                        catch (NoParcelsToDeliver e)
                        {
                            if (drone.BatteryStatus != 100)
                                bl.UpdateSendDroneToCharge(drone.ID);
                            else
                            {
                                //Thread.Sleep(DELAY);
                                worker.CancelAsync();
                            }
                        }
                        catch (CanNotUpdateDrone e)
                        {
                            try
                            {
                                bl.UpdateSendDroneToCharge(drone.ID);
                            }
                            catch(CanNotUpdateDrone es)
                            {
                                Thread.Sleep(DELAY);
                            }
                        }
                        updateDrone(drone);
                        Thread.Sleep(DELAY);
                        break;
                    case DroneStatus.Delivery:
                        if (drone.parcelInDelivery.isWaiting)
                            bl.UpdateCollectParcelByDrone(drone.ID);
                        else
                            bl.UpdateSupplyParcelByDrone(drone.ID);
                        updateDrone(drone);
                        Thread.Sleep(DELAY);
                        break;
                    case DroneStatus.Maintenance:
                        //double needToFill = (100 - drone.BatteryStatus) / bl.getRateOfCharging();
                        while((drone.BatteryStatus) <= 98)
                        {
                            updateDrone(drone);
                            Thread.Sleep(SPEED);
                            drone.BatteryStatus += 2;
                        }
                        drone.BatteryStatus = 100;
                        bl.UpdateUnchargeDrone(drone.ID, (100 - drone.BatteryStatus) / bl.getRateOfCharging());
                        updateDrone(drone);
                        Thread.Sleep(DELAY);
                        break;
                }
            }
        }
    }
}