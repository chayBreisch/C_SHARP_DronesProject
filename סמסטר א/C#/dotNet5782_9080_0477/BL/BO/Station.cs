﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;

namespace BO
{
    public class Station
    {
        public Station(int id, int name, int chargeslots, Location location, bool isActive, List<DroneInCharger> droneInCharger)
        {
            ID = id;
            Name = name;
            chargeSlots = chargeslots;
            Location = location;
            DronesInCharge = droneInCharger;
            IsActive = isActive;
        }
        public Station()
        {
            ID = 0;
            Name = 0;
            chargeSlots = 0;
            Location = new Location(-1, -1);
            DronesInCharge = new List<DroneInCharger>();
            IsActive = true;
        }
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
                    throw new OutOfRange("station id");
                Id = value;
            }
        }
        public int Name { get; set; }
        private int chargeSlots { get; set; }

        public int ChargeSlots
        {
            get
            {
                return chargeSlots;
            }
            set
            {
                if (value < 0)
                    throw new OutOfRange("station charge slots");
                chargeSlots = value;
            }
        }
        public Location Location { get; set; }
        public List<DroneInCharger> DronesInCharge { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return $"station: ID: {ID} Name: {Name} ChargeSlots: {ChargeSlots} Location: {Location},  ";
        }
    }
}

