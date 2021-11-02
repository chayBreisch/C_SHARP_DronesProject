using System;
using System.Collections.Generic;
using System.Linq;
using IDAL.DO;
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

        public void AddCustomer(Customer newCustomer)
        {     
            DataSource.customers[DataSource.customers.Count - 1] = newCustomer;
        }
    }
}
