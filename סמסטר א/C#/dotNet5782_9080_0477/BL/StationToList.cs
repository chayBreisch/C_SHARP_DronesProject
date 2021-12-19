using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace BO
    {
        public class StationToList
        {
            public StationToList(Station station)
            {
                ID = station.ID;
                Name = station.Name;
                ChargeSlotsFree = station.ChargeSlots - station.DronesInCharge.Count;
                ChargeSlotsBusy = station.DronesInCharge.Count;
            }
            public int ID { get; set; }
            public int Name { get; set; }
            public int ChargeSlotsFree { get; set; }
            public int ChargeSlotsBusy { get; set; }
            public override string ToString()
            {
                return $"*****************************************\nid: {ID},\nname: {Name},\nchargeSlotsFree: {ChargeSlotsFree},\nchargeSlotsBusy: {ChargeSlotsBusy}\n*****************************************";
            }
        }
    }
