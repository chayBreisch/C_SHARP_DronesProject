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

            foreach (var customer in DataSource.customers)
            {
                yield return customer;
            }
        }
        public void CheckUniqeCustomer(ulong id)
        {
            foreach (var customer in DataSource.customers)
            {
                if (customer.ID == id)
                    throw new NotUniqeID(id, typeof(Customer));
            }
        }

        /*public Customer GetSpecificCustomer(ulong id)
        {
            try
            {
                return DataSource.customers.Find(customer => customer.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }*/

        /// <summary>
        /// get a customer by the id
        /// </summary>
        /// <param name="newCustomer"></param>

        public Customer getCustomerById(Predicate<Customer> predicate)
        {
            Customer customer1 = new Customer();
            try
            {
                customer1 = (from customer in DataSource.customers
                             where predicate(customer)
                             select customer).First();
            }
            catch(Exception e)
            {

            }
            return customer1;
        }

        /// <summary>
        /// get the customers with a specific condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Customer> getCustomerByCondition(Predicate<Customer> predicate)
        {
            //try todo
            return (from customer in DataSource.customers
                    where predicate(customer)
                    select customer);
        }
        public void AddCustomer(Customer newCustomer)
        {
            CheckUniqeCustomer(newCustomer.ID);
            DataSource.customers.Add(newCustomer);
        }
        public void updateCustomer(Customer customer)
        {
            int index = DataSource.customers.FindIndex(d => d.ID == customer.ID);
            DataSource.customers[index] = customer;
        }
    }
}
