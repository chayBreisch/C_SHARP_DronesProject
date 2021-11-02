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



            public Customer(int id, string name, string phone, double latitude, double longitude)
            {
                IDAL.DO.Customer customer = new IDAL.DO.Customer();
                dalObject.AddCustomer(customer);

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


