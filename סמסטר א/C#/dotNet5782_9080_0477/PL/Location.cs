using System;
using System.Windows;
//using Exception;

namespace PL
{
    public class Location : DependencyObject
    {
        public static readonly DependencyProperty LongitudeProperty =
        DependencyProperty.Register("Longitude",
                            typeof(object),
                            typeof(Location),
                            new UIPropertyMetadata(0));

        public double Longitude
        {
            get
            {
                return (double)GetValue(LongitudeProperty);
            }
            set => SetValue(LongitudeProperty, value);
        }

        public static readonly DependencyProperty LatitudeProperty =
        DependencyProperty.Register("Latitude",
                            typeof(object),
                            typeof(Location),
                            new UIPropertyMetadata(0));

        public Double Latitude
        {
            get
            {
                return (double)GetValue(LatitudeProperty);
            }
            set => SetValue(LatitudeProperty, value);
        }

        public Location()
        {
        }

        public Location(BO.Location location)
        {
            if(location == null)
            {
                location.Longitude = 2;
                location.Latitude = 17;
            }
            Longitude = location.Longitude;
            Latitude = location.Latitude;
        }

        public override string ToString()
        {
            return $"{Longitude}/{Latitude}";
        }

    }
}