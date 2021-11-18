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
            public IDAL.DO.Priorities Priority { get; set; }
            public CustomerAtParcel CustomerInDeliverySender { get; set; }
            public CustomerAtParcel CustomerInDeliveryReciever { get; set; }

            public override string ToString()
            {
                return $"ParcelInTransform: ID: {ID}, Priority: {Priority}, CustomerInDeliverySender: {CustomerInDeliverySender}" +
                    $"CustomerInDeliveryReciever: {CustomerInDeliveryReciever}";
            }
        }
    }
}