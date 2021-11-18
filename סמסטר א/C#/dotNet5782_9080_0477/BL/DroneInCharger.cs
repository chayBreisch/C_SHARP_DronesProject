using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneInCharger
        {
            public int ID { get; set; }
            public double BatteryStatus { get; set; }
            public override string ToString()
            {
                return $"DroneInCharger: ID: {ID}, BatteryStatus: {BatteryStatus}";
            }
        }
    }
}