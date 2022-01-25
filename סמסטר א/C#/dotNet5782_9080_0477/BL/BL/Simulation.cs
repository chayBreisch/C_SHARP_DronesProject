using System;
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
        int DELAY = 1000;
        double SPEED = 60;
        IBL bl;
        public Simulation(IBL bl)
        {
            this.bl = bl;
        }
        public void start(BO.Drone drone, BackgroundWorker worker, Action<BO.Drone, int> updateDrone, Func<bool> needToStop)
        {
            BO.Drone dr = new Drone();
            while (!needToStop())
            {
                dr = bl.GetSpecificDroneBL(drone.ID);
                switch (dr.DroneStatus)
                {
                    case DroneStatus.Available:
                        try
                        {
                            bl.UpdateConnectParcelToDrone(drone.ID);
                        }
                        catch (NoParcelsToDeliver e)
                        {
                            //change to regular run
                            worker.CancelAsync();
                        }
                        catch (CanNotUpdateDrone e)
                        {
                            bl.UpdateSendDroneToCharge(drone.ID);
                        }
                        updateDrone(drone, 1);
                        Thread.Sleep(DELAY);
                        break;
                    case DroneStatus.Delivery:
                        if (drone.parcelInDelivery.isWaiting)
                            bl.UpdateCollectParcelByDrone(drone.ID);
                        else
                            bl.UpdateSupplyParcelByDrone(drone.ID);
                        updateDrone(drone, 1);
                        Thread.Sleep(DELAY);
                        break;
                    case DroneStatus.Maintenance:
                        bl.UpdateUnchargeDrone(drone.ID, (100 - drone.BatteryStatus) / bl.getRateOfCharging());
                        updateDrone(drone, 1);
                        Thread.Sleep(DELAY);
                        break;
                }
            }
        }
    }
}
