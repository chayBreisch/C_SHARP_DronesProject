using System;
using System.Collections.Generic;
using System.Linq;
using IDAL.DO;
using DAL;


namespace DalObject
{
    public partial class DalObject
    {
        public List<Customer> GetCustomersByList()
        {
            return DataSource.customers;
        }
        public IEnumerable<Customer> GetCustomer()
        {

            foreach (var customer in DataSource.customers)
            {
                yield return customer;
            }  
        }

        public Customer GetSpecificCustomer(ulong id)
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
            DataSource.customers.Add(newCustomer);
        }
    }
}
