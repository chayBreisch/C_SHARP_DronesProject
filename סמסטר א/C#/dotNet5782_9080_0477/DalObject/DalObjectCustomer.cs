﻿using System;
using System.Collections.Generic;
using System.Linq;
using DO;
using DALException;
using System.Runtime.CompilerServices;
using IDAL;

namespace Dal
{
    internal partial class DalObject : IDal
    {
        /// <summary>
        /// get customers
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate = null)
        {
            return from customer in DataSource.customers
                   select customer;
        }

        /// <summary>
        /// check uniqe customer
        /// </summary>
        /// <param name="id"></param>
        private void checkUniqeCustomer(ulong id)
        {
            foreach (var customer in DataSource.customers)
            {
                if (customer.ID == id)
                    throw new NotUniqeID(id, typeof(Customer));
            }
        }

        /// <summary>
        /// get a customer by the id
        /// </summary>
        /// <param name="newCustomer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        /// add customer
        /// </summary>
        /// <param name="newCustomer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer newCustomer)
        {
            checkUniqeCustomer(newCustomer.ID);
            DataSource.customers.Add(newCustomer);
        }

        /// <summary>
        /// remove customer
        /// </summary>
        /// <param name="idRemove"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                throw new NotExistObjWithID(id, typeof(Parcel), e);
            }
        }

        /// <summary>
        /// update customer
        /// </summary>
        /// <param name="customer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
        {
            int index = getIndexOfCustomer(customer.ID);
            DataSource.customers[index] = customer;
        }
    }
}
