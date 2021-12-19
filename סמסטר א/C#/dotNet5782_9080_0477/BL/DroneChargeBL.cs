using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BO
    {
        public class DroneChargeBL
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
            public override string ToString()
            {
                return $"droneCharge: DroneId: {DroneId}, StationId: {StationId}";
            }
        }
    }
}
