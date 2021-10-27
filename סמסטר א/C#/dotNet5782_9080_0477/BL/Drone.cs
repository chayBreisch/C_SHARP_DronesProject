using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{
    namespace BO
    {
        public class Drone
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public WeightCatagories MaxWeight { get; set; }
            public DroneStatus Status { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                return $"ID: {ID}, Model: {Model}, Status: {Status }, MaxWeight: {MaxWeight}, Battery: {Battery}";
            }
        }
    }
}
