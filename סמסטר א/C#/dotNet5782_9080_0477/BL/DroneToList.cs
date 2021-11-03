using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public IDAL.DO.WeightCatagories weightCatagories { get; set; }
            public double BatteryStatus { get; set; }
            public DroneStatus droneStatus { get; set; }
            public Location location { get; set; }
            public int NumOfParcelTrans { get; set; }
        }
    }
}