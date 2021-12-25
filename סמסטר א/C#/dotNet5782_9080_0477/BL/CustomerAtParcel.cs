using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class CustomerAtParcel
    {
        public ulong ID { get; set; }
        public string CustomerName { get; set; }

        public CustomerAtParcel(ulong id, string name)
        {
            ID = id;
            CustomerName = name;
        }
        public CustomerAtParcel()
        {
            ID = 10000000;
            CustomerName = "";
        }
        public override string ToString()
        {
            return $"ID: {ID}, CustomerName: {CustomerName}";
        }
    }
}
