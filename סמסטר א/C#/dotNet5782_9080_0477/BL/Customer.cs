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
        public class Customer
        {

            IDAL.DO.Customer customer;

            public Customer()
            {
                customer = new IDAL.DO.Customer();         
            }


            public Location location { get; set; }
            List<ParcelAtCustomer> parcelSendedByCustomer = new List<ParcelAtCustomer>();
            List<ParcelAtCustomer> parcelSendedToCustomer = new List<ParcelAtCustomer>();



            public override string ToString()
            {
                return $"customer: {customer.Name} : {customer.ID}";
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


