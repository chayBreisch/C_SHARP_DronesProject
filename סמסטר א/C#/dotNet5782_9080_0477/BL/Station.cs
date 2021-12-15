using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static BL.ExceptionsBL;

namespace IBL
{
    namespace BO
    {
        public class Station
        {


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
            public LocationBL Location { get; set; }
            public List<DroneInCharger> DronesInCharge { get; set; }
            public override string ToString()
            {
                return $"station: ID: {ID} Name: {Name} ChargeSlots: {ChargeSlots} Location: {Location},  ";
            }
        }
    }
}
