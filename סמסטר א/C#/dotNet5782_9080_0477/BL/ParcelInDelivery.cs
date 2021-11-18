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
            public IDAL.DO.WeightCatagories Weight { get; set; }
            public IDAL.DO.Priorities Priority { get; set; }
            public bool ParcelStatus { get; set; }
            public LocationBL CollectLocation { get; set; }
            public LocationBL DeliveryDestinationLocation { get; set; }
            public double TransportDistance { get; set; }

            public override string ToString()
            {
                return $"ParcelInDelivery: ID: {ID}, Weight: {Weight}, Priority: {Priority}, ParcelStatus: {ParcelStatus}" +
                    $", CollectLocation: {CollectLocation}, CollectLocation: {CollectLocation}, DeliveryDestinationLocation: {DeliveryDestinationLocation}" +
                    $"TransportDistance: {TransportDistance}";
            }
        }
    }
}