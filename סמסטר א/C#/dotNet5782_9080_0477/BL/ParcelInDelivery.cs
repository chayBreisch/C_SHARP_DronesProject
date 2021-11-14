using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelInDelivery
        {
            public int ID { get; set; }
            public IDAL.DO.WeightCatagories weightCatagory { get; set; }
            public IDAL.DO.Priorities priorities { get; set; }
            public bool parcelStatus { get; set; }
            public LocationBL CollectLocation { get; set; }
            public LocationBL DeliveryDestinationLocation { get; set; }
            public double TransportDistance { get; set; }
        }
    }
}