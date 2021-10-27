using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneCharge
        {
            public int DroneID { get; set; }
            public int StationID { get; set; }
            public override string ToString()
            {
                return $"{DroneID}";
            }
        }
    }
}
