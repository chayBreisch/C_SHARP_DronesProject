using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerInDelivery
        {
            public int ID { get; set; }
            public string CustomerName { get; set; }

            public CustomerInDelivery()
            {
                ID = 0;
                CustomerName = "";
            }

        }
    }
}