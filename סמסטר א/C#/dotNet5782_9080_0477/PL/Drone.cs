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
    class Drone_ : DependencyObject
    {


        public int ID { get; set; }
        //public string Model { get; set; }
        public DO.WeightCatagories Weight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatus DroneStatus { get; set; }
        public LocationBL Location { get; set; }


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



        public Drone_(DroneToList drone)
        {
            ID = drone.ID;
            Model = drone.Model;
            Weight = drone.Weight;
            BatteryStatus = drone.BatteryStatus;
            DroneStatus = drone.DroneStatus;
            Location = drone.Location;

        }
    }
}
