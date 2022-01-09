using DAL;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalXml
{
    public partial class DalXml
    {
        /// <summary>
        /// check uniqe customer
        /// </summary>
        /// <param name="id"></param>
        private void checkUniqeCustomer(ulong id)
        {
            if (GetCustomers().Any(customer => customer.ID == id))
                throw new NotUniqeID(id, typeof(Customer));
        }

        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            IEnumerable<Customer> customerList = XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath);
            checkUniqeCustomer(customer.ID);
            customerList.ToList().Add(customer);
            XMLTools.SaveListToXMLSerializer<Customer>(customerList, dir + customerFilePath);
        }

        /// <summary>
        /// remove customer
        /// </summary>
        /// <param name="id"></param>
        public void RemoveCustomer(ulong id)
        {
            IEnumerable<Customer> customerList = XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath);
            if (!customerList.Any(d => d.ID == id))
            {
                throw new NotExistObjWithID(id, typeof(Customer));
            }
            Customer customer = GetCustomerById(c => c.ID == id);
            customer.IsActive = false;
            UpdateCustomer(customer);
        }

        /// <summary>
        /// update customer
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateCustomer(Customer customer)
        {
            List<Customer> customerList = XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath).ToList();

            int index = customerList.FindIndex(d => d.ID == customer.ID);

            if (index == -1)
                throw new NotExistObjWithID(customer.ID, typeof(Drone));
            customerList[index] = customer;
            XMLTools.SaveListToXMLSerializer<Customer>(customerList, dir + customerFilePath);
        }

        /// <summary>
        /// get customer by id
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Customer GetCustomerById(Predicate<Customer> predicate)
        {
            Customer customer1 = new Customer();
            try
            {
                IEnumerable<Customer> customerList = XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath);
                customer1 = (from customer in customerList
                             where predicate(customer)
                             select customer).First();
            }
            catch (Exception e) { }
            return customer1;
        }

        /// <summary>
        /// get all customers
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicat = null)
        {
            IEnumerable<Customer> droneList = XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath);
            predicat ??= ((st) => true);
            return from customer in droneList
                   where predicat(customer)
                   orderby customer.ID
                   select customer;
        }
    }
}
