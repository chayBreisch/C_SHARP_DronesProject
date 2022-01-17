﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Parcel
    {
        public int ID { get; set; }
        public ulong SenderID { get; set; }
        public ulong TargetID { get; set; }
        public WeightCatagories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime? Requested { get; set; }
        public int DroneID { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return $"ID: {ID}, Priority: {Priority}, SenderID: {SenderID}, TargetID: {TargetID}, Weight: {Weight}, droneId: {DroneID}"; ;
        }
    }
}

