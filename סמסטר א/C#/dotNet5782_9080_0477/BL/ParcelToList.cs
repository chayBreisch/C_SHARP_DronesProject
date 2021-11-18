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
            public IDAL.DO.WeightCatagories Weight { get; set; }
            public IDAL.DO.Priorities Priority { get; set; }
            public ParcelStatus ParcelStatus { get; set; }

            public override string ToString()
            {
                return $"ParcelToList: ID: {ID}, NameCustomerSender: {NameCustomerSender}, NameCustomerReciver: {NameCustomerReciver}" +
                    $"Weight: {Weight}, Priority: {Priority}, ParcelStatus: {ParcelStatus}";
            }
        }
    }
}