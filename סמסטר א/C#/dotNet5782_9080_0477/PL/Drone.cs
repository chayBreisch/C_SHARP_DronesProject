using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;
using BL;

namespace PL
{
    public class Drone_ : DependencyObject
    {


        public static readonly DependencyProperty IDProperty =
       DependencyProperty.Register("ID",
                                  typeof(object),
                                  typeof(Drone_),
                                  new UIPropertyMetadata(0));

        public int ID
        {
            get
            {
                return (int)GetValue(IDProperty);
            }
            set
            {
                SetValue(IDProperty, value);
            }
        }

        public static readonly DependencyProperty WeightProperty =
       DependencyProperty.Register("Weight",
                                  typeof(object),
                                  typeof(Drone_),
                                  new UIPropertyMetadata(0));

        public DO.WeightCatagories Weight
        {
            get
            {
                return (DO.WeightCatagories)GetValue(WeightProperty);
            }
            set
            {
                SetValue(WeightProperty, value);
            }
        }


        public static readonly DependencyProperty BatteryStatusProperty =
      DependencyProperty.Register("BatteryStatus",
                                 typeof(object),
                                 typeof(Drone_),
                                 new UIPropertyMetadata(0));


        public double BatteryStatus
        {
            get
            {
                return (double)GetValue(BatteryStatusProperty);
            }
            set
            {
                SetValue(BatteryStatusProperty, value);
            }
        }



        public static readonly DependencyProperty LocationProperty =
      DependencyProperty.Register("Location",
                                 typeof(object),
                                 typeof(Drone_),
                                 new UIPropertyMetadata(0));

        public Location Location
        {
            get
            {
                return (Location)GetValue(LocationProperty);
            }
            set
            {
                SetValue(LocationProperty, value);
            }
        }




        public static readonly DependencyProperty ModelProperty =
        DependencyProperty.Register("Model",
                                   typeof(object),
                                   typeof(Drone_),
                                   new UIPropertyMetadata(0));

        public string Model
        {
            get
            {
                return (string)GetValue(ModelProperty);
            }
            set
            {
                SetValue(ModelProperty, value);
            }
        }


        public static readonly DependencyProperty DroneStatusProperty =
        DependencyProperty.Register("DroneStatus",
                                    typeof(object),
                                    typeof(Drone_),
                                    new UIPropertyMetadata(0));


        public DroneStatus DroneStatus
        {
            get
            {
                return (DroneStatus)GetValue(DroneStatusProperty);
            }
            set
            {
                SetValue(DroneStatusProperty, value);
            }
        }



        public static readonly DependencyProperty parcelInDeliveryProperty =
        DependencyProperty.Register("parcelInDelivery",
                                    typeof(object),
                                    typeof(Drone_),
                                    new UIPropertyMetadata(0));


        public ParcelInDelivery parcelInDelivery
        {
            get
            {
                return (ParcelInDelivery)GetValue(parcelInDeliveryProperty);
            }
            set
            {
                SetValue(parcelInDeliveryProperty, value);
            }
        }



        public static readonly DependencyProperty IsActivProperty =
       DependencyProperty.Register("IsActiv",
                                   typeof(object),
                                   typeof(Drone_),
                                   new UIPropertyMetadata(0));


        public bool IsActiv
        {
            get
            {
                return (bool)GetValue(IsActivProperty);
            }
            set
            {
                SetValue(IsActivProperty, value);
            }
        }



        public Drone_(BO.Drone drone)
        {
            ID = drone.ID;
            Model = drone.Model;
            Weight = drone.Weight;
            BatteryStatus = drone.BatteryStatus;
            DroneStatus = drone.DroneStatus;
            Location = new Location(drone.Location);
            parcelInDelivery = drone.parcelInDelivery;
            IsActiv = drone.IsActive;

        }


        public void updateDrone(BO.Drone drone)
        {
            this.ID = drone.ID;
            this.Model = drone.Model;
            this.Weight = drone.Weight;
            this.BatteryStatus = Math.Round(drone.BatteryStatus);
            this.DroneStatus = drone.DroneStatus;
            this.Location = new Location(drone.Location);
            this.parcelInDelivery = drone.parcelInDelivery;
            this.IsActiv = drone.IsActive;

        }


        /*
                 private double battery { get; set; }
       

        public ParcelInDelivery parcelInDelivery { get; set; }
        
        

        public bool IsActive { get; set; }
         
         */


    }
}
