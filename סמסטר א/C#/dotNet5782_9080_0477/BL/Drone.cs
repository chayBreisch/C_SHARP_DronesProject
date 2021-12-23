using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;


namespace BO
{
    public class Drone
    {
        public Drone()
        {
            battery = 100;
        }
        private double battery { get; set; }
        private int Id { get; set; }

        public int ID
        {
            get
            {
                return Id;
            }
            set
            {
                if (value < 0)
                    throw new OutOfRange("drone id");
                Id = value;
            }
        }
        public string Model { get; set; }
        private DO.WeightCatagories weight { get; set; }

        public DroneStatus DroneStatus { get; set; }
        public ParcelInDelivery parcelInDelivery { get; set; }
        public LocationBL Location { get; set; }
        public DO.WeightCatagories Weight
        {
            get { return weight; }
            set
            {
                if (value < 0)
                    throw new OutOfRange("weight");
                weight = value;
            }
        }

        public double BatteryStatus
        {
            get
            {
                return battery;
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new OutOfRange("battry");
                battery = value;
            }
        }
        public override string ToString()
        {
            return $"drone  : {ID}, \n" +
                $" battery: {BatteryStatus}%,\n Model: {Model}, \nMaxWeight: {Weight}, \n" +
                $"DroneStatus : {DroneStatus}, \nParcelAtTransfor: {parcelInDelivery},\n" +
                $"Location: {Location}\n";
        }
    }
}
