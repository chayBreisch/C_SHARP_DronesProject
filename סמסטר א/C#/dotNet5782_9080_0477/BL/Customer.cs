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
        private ulong Id;
        public ulong ID
        {
            get
            { return Id; }
            set
            {
                if (value >= 100000000 && value < 1000000000)
                    Id = value;
                else
                {
                    throw new OutOfRange("customer id");
                }
            }
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public LocationBL Location { get; set; }
        public List<ParcelAtCustomer> parcelSendedByCustomer { get; set; }
        public List<ParcelAtCustomer> parcelSendedToCustomer { get; set; }

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


