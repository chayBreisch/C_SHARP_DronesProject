using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerAtParcel
        {
            public int ID { get; set; }
            public string CustomerName { get; set; }

            public CustomerAtParcel()
            {
                ID = 0;
                CustomerName = "";
            }
            public override string ToString()
            {
                return $"ID: {ID}, CustomerName: {CustomerName}";
            }
        }
    }
}