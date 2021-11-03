using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelAtCustomer
        {
            public int ID { get; set; }
            public IDAL.DO.WeightCatagories weightCatagories { get; set; }
            public IDAL.DO.Priorities priorities { get; set; }
            public ParcelStatus parcelStatus { get; set; }
            public CustomerInDelivery customerInDelivery { get; set; }
        }
    }
}