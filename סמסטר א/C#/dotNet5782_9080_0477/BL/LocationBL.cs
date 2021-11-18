using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class LocationBL
        {
            public LocationBL(double longitude, double latitude)
            {
                Longitude = longitude;
                Latitude = latitude;
            }
            public double Longitude { get; set; }
            public double Latitude { get; set; }


            public LocationBL()
            {
                Longitude = 0;
                Latitude = 0;
            }
            public override string ToString()
            {
                return $"longitude  : {Longitude}, " +
                    $" latitude: {Latitude}"

                ;
            }

        }
    }
}