using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;
namespace IBL
{
    namespace BO
    {
        public class LocationBL
        {
            public LocationBL(int longitude1, int latitude1)
            {
                longitude = longitude1;
                latitude = latitude1;
            }

            private int latitude { get; set; }

            public int Latitude
            {
                get
                {
                    return latitude;
                }
                set
                {
                    if (value > 36 || value < 0)
                        throw new OutOfRange("longitude");
                    latitude = value;
                }
            }
            private int longitude { get; set; }

            public int Longitude
            {
                get
                {
                    return longitude;
                }
                set
                {
                    if (value > 36 || value < 0)
                        throw new OutOfRange("longitude");
                    longitude = value;
                }
            }

            public LocationBL()
            {
                longitude = 0;
                Latitude = 0;
            }
            public override string ToString()
            {
                return $"longitude  : {longitude}, " +
                    $" latitude: {latitude}"

                ;
            }

        }
    }
}