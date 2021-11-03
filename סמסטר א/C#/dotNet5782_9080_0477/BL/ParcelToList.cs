using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelToList
        {
            public int ID { get; set; }
            public string NameCustomerSender { get; set; }
            public string NameCustomerReciver { get; set; }
            public IDAL.DO.WeightCatagories weightCatagories { get; set; }
            public IDAL.DO.Priorities priorities { get; set; }
            public ParcelStatus parcelStatus { get; set; }
        }
    }
}