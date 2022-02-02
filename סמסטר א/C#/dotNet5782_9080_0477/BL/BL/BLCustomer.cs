using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.ExceptionsBL;
using DALException;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL
    {
        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void checkUniqeIdCustomer(ulong id, IDAL.IDal dalObject)
        {
            lock (dalObject)
            {
                IEnumerable<DO.Customer> customers = dalObject.GetCustomers();
                if (customers.Any(c => c.ID == id))
                    throw new NotUniqeID(id, typeof(DO.Customer));
            }
        }

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        /*public void checkUniqeIDCustomer(ulong id)
        {
            IEnumerable<DO.Customer> customers = dalObject.GetCustomers();
            foreach (var customer in customers)
            {
                if (customer.ID == id)
                {
                    throw new NotUniqeID(id, typeof(DO.Customer));
                }
            }
        }*/

        /// <summary>
        /// add a customer to the bl
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(ulong id, string name, string phone, LocationBL location)
        {
            lock (dalObject)
            {
                checkUniqeIdCustomer(id, dalObject);
                /*            BO.Customer customer = new BO.Customer();
                            customer.ID = id;
                            customer.Name = name;
                            customer.Phone = phone;
                            customer.Location = new LocationBL(location.Longitude, location.Latitude);*/
                addCustomerToDal(id, name, phone, location);
            }
        }

        /// <summary>
        /// add a customer to the dal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        private void addCustomerToDal(ulong id, string name, string phone, LocationBL location)
        {
            DO.Customer customer = new DO.Customer();
            customer.ID = id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Longitude = location.Longitude;
            customer.Latitude = location.Latitude;
            customer.IsActive = true;
            dalObject.AddCustomer(customer);
        }

        /// <summary>
        /// return all the customers from the dal converted to bl
        /// </summary>
        /// <returns> List<CustomerBL> </returns>
        private IEnumerable<BO.Customer> getCustomersBL()
        {
            lock (dalObject)
            {
                IEnumerable<DO.Customer> customers = dalObject.GetCustomers();
                List<BO.Customer> customers1 = new List<BO.Customer>();
                foreach (var customer in customers)
                {
                    customers1.Add(convertDalCustomerToBl(customer));
                }
                return customers1;
            }
        }

        /// <summary>
        /// returns a specific customer by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>customerbl</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Customer GetSpecificCustomerBL(Predicate<BO.Customer> predicate)
        {
            try
            {
                lock (dalObject)
                {
                    return (from customer in getCustomersBL()
                            where predicate(customer)
                            select customer).First();
                }
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(1, typeof(DO.Customer), e);

            }
        }

        public CustomerToList GetSpecificCustomerToList(ulong id)
        {
            return new CustomerToList(GetSpecificCustomerBL(C => C.ID == id), dalObject);
        }

        /// <summary>
        /// convert a customer from dal to bl
        /// </summary>
        /// <param name="c"></param>
        /// <returns>CustomerBL</returns>
        private BO.Customer convertDalCustomerToBl(DO.Customer c)
        {
            List<ParcelAtCustomer> parcelSendedByCustomers = new List<ParcelAtCustomer>();
            List<ParcelAtCustomer> parcelSendedToCustomers = new List<ParcelAtCustomer>();
            IEnumerable<DO.Parcel> parcels = dalObject.GetParcels();
            foreach (var p in parcels)
            {
                if (p.SenderID == c.ID)
                    parcelSendedByCustomers.Add(new ParcelAtCustomer(convertDalToParcelBL(p), c.ID, dalObject));
                if (p.TargetID == c.ID)
                    parcelSendedToCustomers.Add(new ParcelAtCustomer(convertDalToParcelBL(p), c.ID, dalObject));

            };

            return new BO.Customer
            {
                ID = c.ID,
                Name = c.Name,
                Phone = c.Phone,
                Location = new LocationBL() { Longitude = c.Longitude, Latitude = c.Latitude },
                parcelSendedByCustomer = parcelSendedByCustomers,
                parcelSendedToCustomer = parcelSendedToCustomers,
                IsActive = c.IsActive
            };
        }

        /// <summary>
        /// update the customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /*public void updateDataCustomer(ulong id, string name = null, string phone = null)
        {
            DO.Customer customer = dalObject.getCustomerById( c => c.ID == id);
            if (name != null)
            {
                customer.Name = name;
            }
            if (phone != null)
            {
                customer.Phone = phone;
            }
            dalObject.updateCustomer(customer);
        }*/

        /// <summary>
        /// check if there is a customer with the id
        /// </summary>
        /// <param name="id"></param>
        private void checkIfCustomerWithThisID(ulong id)
        {
            lock (dalObject)
            {
                bool check = false;
                IEnumerable<DO.Customer> customers = dalObject.GetCustomers();
                foreach (var customer in customers)
                {
                    if (customer.ID == id)
                    {
                        check = true;
                    }
                }
                if (!check)
                    throw new NotExistObjWithID(id, typeof(DO.Customer));
            }
        }

        /// <summary>
        /// return active customerToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetCustomersToList()
        {
            lock (dalObject)
            {
                IEnumerable<BO.Customer> customers = getCustomersBL();
                List<CustomerToList> customers1 = new List<CustomerToList>();
                foreach (var customer in customers)
                {
                    if (customer.IsActive)
                        customers1.Add(new CustomerToList(customer, dalObject));
                }
                return customers1;
            }
        }

        /// <summary>
        /// return not active customerToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetDeletedCustomersToList()
        {
            lock (dalObject)
            {
                IEnumerable<BO.Customer> customers = getCustomersBL();
                List<CustomerToList> customers1 = new List<CustomerToList>();
                foreach (var customer in customers)
                {
                    if (!customer.IsActive)
                        customers1.Add(new CustomerToList(customer, dalObject));
                }
                return customers1;
            }
        }

        /// <summary>
        /// return name of customers by condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<string> GetCustomersNamesByCondition(Predicate<BO.Customer> predicate)
        {
            lock (dalObject)
            {
                return (from customer in getCustomersBL()
                        where predicate(customer)
                        select customer.Name);
            }
        }

        /// <summary>
        /// get customer by condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Customer> GetCustomersByCondition(Predicate<BO.Customer> predicate)
        {
            lock (dalObject)
            {
                return (from customer in getCustomersBL()
                        where predicate(customer)
                        select customer);
            }
        }

        /// <summary>
        /// convert customerToList to customerBL
        /// </summary>
        /// <param name="customerToList"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Customer ConvertCustomerToListToCustomerlBL(CustomerToList customerToList)
        {
            return GetSpecificCustomerBL(c => c.ID == customerToList.ID);
        }

        /// <summary>
        /// check if the check digit is good
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckValidIdCustomer(ulong id)///////////////////לבדוק אם גדול מעשר אחרי הכפלה
        {
            int sum = 0, digit, digit2 = 0;
            for (int i = 0; i < 9; i++)
            {
                digit = (int)(id % 10);
                if ((i % 2) == 0)
                {
                    digit *= 2;
                    if (digit > 10)
                    {
                        digit2 = digit % 10;
                        digit /= 10;
                        digit2 += digit;
                    }
                    digit = digit2;
                }
                sum += digit;
                id /= 10;
            }
            if ((sum % 10) == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// remove customer
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCustomer(ulong id)
        {
            lock (dalObject)
            {
                dalObject.RemoveCustomer(id);
            }
        }
    }
}
