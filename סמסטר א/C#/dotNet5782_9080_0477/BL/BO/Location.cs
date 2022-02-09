﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;

namespace BO
{
    public class Location
    {
        public Location(double longitude1, double latitude1)
        {
            longitude = longitude1;
            latitude = latitude1;
        }

        private double latitude { get; set; }

        public Double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                if (value > 36 || value < 0)

                    new OutOfRange("longitude");
                latitude = value;
            }
        }
        private double longitude { get; set; }

        public double Longitude
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

        public Location()
        {
            longitude = 0;
            Latitude = 0;
        }
        public override string ToString()
        {
            return $"({longitude}, " +
                $"{latitude})"

            ;
        }

    }
}
