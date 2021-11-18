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
            public IDAL.DO.WeightCatagories Weight { get; set; }
            public IDAL.DO.Priorities Priority { get; set; }
            public ParcelStatus ParcelStatus { get; set; }
            public CustomerAtParcel customerAtParcel { get; set; }
            public override string ToString()
            {
                return $"ParcelAtCustomer: ID: {ID}, Weight: {Weight}, Priority: {Priority}, ParcelStatus: {ParcelStatus}, customerAtParcel: {customerAtParcel}";
            }
            }
    }
}