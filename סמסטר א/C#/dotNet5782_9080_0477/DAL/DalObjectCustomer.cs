using System;
using System.Collections.Generic;
using System.Linq;
using DO;
using DAL;


namespace DalObject
{
    internal partial class DalObject
    {
        public IEnumerable<Customer> GetCustomers()
        {
            return from customer in DataSource.customers
                   select customer;
        }
        private void checkUniqeCustomer(ulong id)
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

        public Customer GetCustomerById(Predicate<Customer> predicate)
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
        /*public IEnumerable<Customer> getCustomersByCondition(Predicate<Customer> predicate)
        {
            //try todo
            return (from customer in DataSource.customers
                    where predicate(customer)
                    select customer);
        }*/
        public void AddCustomer(Customer newCustomer)
        {
            checkUniqeCustomer(newCustomer.ID);
            DataSource.customers.Add(newCustomer);
        }
        /*public void updateCustomer(Customer customer)
        {
            int index = DataSource.customers.FindIndex(d => d.ID == customer.ID);
            DataSource.customers[index] = customer;
        }*/
        public void RemoveCustomer(ulong idRemove)
        {
            Customer customer = DataSource.customers[getIndexOfCustomer(idRemove)];
            customer.IsActive = false;
            UpdateCustomer(customer);
        }

        /// <summary>
        /// return index of parcel in dataSource list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int getIndexOfCustomer(ulong id)
        {
            try
            {
                return DataSource.customers.FindIndex(p => p.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Parcel));
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            int index = getIndexOfCustomer(customer.ID);
            DataSource.customers[index] = customer;
        }
    }
}
