using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace IBL
{
    namespace BO
    {
        public class CustomerBL
        {
            private ulong id;
            public ulong ID {
                get
                { return id; }
                set
                {
                    if(BL.BL.CheckLongIdIsValid(value) && BL.BL.CheckValidIdCustomer(value) /*&& BL.BL.checkUniqeIDCustomer(value)*/)
                    id = value;
                    else{
                    //throw new FormatException("not valid id");
                    }
                }
                    }
            public string Name { get; set; }
            public string Phone { get; set; }


            public LocationBL Location { get; set; }
            public List<ParcelAtCustomer> parcelSendedByCustomer { get; set; }
            public List<ParcelAtCustomer> parcelSendedToCustomer { get; set; }



            /* public override string ToString()
             {
                 return $"customer: {ID} : {Name} : {Phone}";
             }
 */
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


                return $"customer {Name} : {ID}, {Phone}, Location : {Location}," +
                    $"parcelsSentedByCustomer: {parcelSentedByCustomer}, parcelsSentedToCustomer: {parcelSentedToCustomer} ";
            }










            /*public Customer()
            {
                ID = 0;
                Name = "";
                Phone = "";
                Longitude = 0;
                Latitude = 0;
            }
            public Customer(int id, string name, string phone, double longitude, double latitude)
            {
                ID = id;
                Name = name;
                Phone = phone;
                Longitude = longitude;
                Latitude = latitude;
            }
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()//////////////////
            {
                return $"ID: {ID}, Name: {Name}, Phone: {Phone}, Longitude: {Longitude}, Latitude: {Latitude}";

            }*/
        }
    }
}


