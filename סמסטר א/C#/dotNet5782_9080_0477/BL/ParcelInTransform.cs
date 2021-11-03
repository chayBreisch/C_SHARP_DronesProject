using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelInTransform
        {
            public int ID { get; set; }
            public IDAL.DO.Priorities priorities { get; set; }
            public CustomerInDelivery customerInDeliverySender { get; set; }
            public CustomerInDelivery customerInDeliveryReciever { get; set; }
        }
    }
}