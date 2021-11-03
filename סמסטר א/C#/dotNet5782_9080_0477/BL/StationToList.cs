using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class StationToList
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int ChargeSlotsFree { get; set; }
            public int ChargeSlotsBusy { get; set; }
        }
    }
}