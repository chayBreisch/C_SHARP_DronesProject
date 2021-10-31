using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;
using DAL;
namespace DalObject
{
    public partial class DalObject
    {
        public IEnumerable<Customer> GetCustomer()
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < DataSource.customers.Count; i++)
            {
                customers.Add(DataSource.customers[i]);
            }
            return customers;
        }

        public Customer GetSpecificCustomer(int id)
        {
            try
            {
                return DataSource.customers.First(customer => customer.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exeptions(id);
            }
        }

        public void AddCustomer(int id, string name, string phone, double latitude, double longitude)
        {
            Customer newCustomer = new Customer();
            newCustomer.ID = id;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = latitude;
            newCustomer.Longitude = longitude;
            DataSource.customers[DataSource.customers.Count - 1] = newCustomer;
        }
    }
}
