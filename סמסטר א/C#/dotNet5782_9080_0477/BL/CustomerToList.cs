using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerToList
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Phone { get; set; }
            public int SumOfParcelsSendedAndProvided { get; set; }
            public int SumOfParcelsSendedAndNotProvided { get; set; }
            public int SumOfParcelsRecieved { get; set; }
            public int SumOfParcelsOnTheWay { get; set; }
        }
    }
}