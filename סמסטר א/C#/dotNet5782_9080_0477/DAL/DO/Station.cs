﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    public struct Station
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int ChargeSlots { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return $"id: {ID}, Name: {Name}, Longitude: {Longitude}, Latitude: {Latitude}, ChargeSlots: {ChargeSlots}";
        }
    }
}
