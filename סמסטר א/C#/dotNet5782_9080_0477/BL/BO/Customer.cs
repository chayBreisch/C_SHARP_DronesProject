using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;


namespace BO
{
    public class Customer
    {
        public ulong ID { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public List<ParcelAtCustomer> parcelSendedByCustomer { get; set; }
        public List<ParcelAtCustomer> parcelSendedToCustomer { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            string parcelSentedByCustomer = " ";
            string parcelSentedToCustomer = " ";

            foreach (var p in parcelSendedByCustomer)
            {
                parcelSentedByCustomer += p;
                parcelSentedByCustomer += " ";
            }

            foreach (var p in parcelSendedToCustomer)
            {
                parcelSentedToCustomer += p;
                parcelSentedToCustomer += " ";
            }


            return $"customer ID: {ID}, Name : {Name}, Phone: {Phone}, Location : {Location}," +
                $"parcelsSentedByCustomer: {parcelSentedByCustomer}, parcelsSentedToCustomer: {parcelSentedToCustomer} ";
        }
    }
}


