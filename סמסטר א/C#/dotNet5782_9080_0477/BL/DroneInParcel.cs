using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneInParcel
        {
            public int ID { get; set; }
            public double BatteryStatus { get; set; }
            public LocationBL CurrentLocation { get; set; }
        }
    }
}