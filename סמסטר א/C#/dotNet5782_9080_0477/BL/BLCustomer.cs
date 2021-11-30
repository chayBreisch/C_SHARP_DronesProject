using DAL;
using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdCustomer(ulong id, IDAL.IDal dalObject)
        {
            List<Customer> customers = dalObject.GetCustomer().ToList();
            if (customers.Any(c => c.ID == id))
                throw new NotUniqeID(id, typeof(Customer));
        }

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public void checkUniqeIDCustomer(ulong id)
        {
            IEnumerable<Customer> customers = dalObject.GetCustomer();
            foreach (var customer in customers)
            {
                if (customer.ID == id)
                {
                    throw new NotUniqeID(id, typeof(Customer));
                }
            }
        }

        /// <summary>
        /// add a customer to the bl
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        public void AddCustomer(ulong id, string name, string phone, LocationBL location)
        {
            checkUniqeIdCustomer(id, dalObject);
            CustomerBL customer = new CustomerBL();
            customer.ID = id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Location = new LocationBL(location.Longitude, location.Latitude);
            AddCustomerToDal(id, name, phone, location);
        }

        /// <summary>
        /// add a customer to the dal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        public void AddCustomerToDal(ulong id, string name, string phone, LocationBL location)
        {
            Customer customer = new Customer();
            customer.ID = id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Longitude = location.Longitude;
            customer.Latitude = location.Latitude;
            dalObject.AddCustomer(customer);
        }

        /// <summary>
        /// return all the customers from the dal converted to bl
        /// </summary>
        /// <returns> List<CustomerBL> </returns>
        public List<CustomerBL> GetCustomersBL()
        {
            IEnumerable<Customer> customers = dalObject.GetCustomer();
            List<CustomerBL> customers1 = new List<CustomerBL>();
            foreach (var customer in customers)
            {
                customers1.Add(convertDalCustomerToBl(customer));
            }
            return customers1;
        }

        /// <summary>
        /// returns a specific customer by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>customerbl</returns>
        public CustomerBL GetSpecificCustomerBL(ulong id)
        {
            try
            {
                return convertDalCustomerToBl(dalObject.getCustomerById(c => c.ID == id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Customer));

            }
        }

        /// <summary>
        /// convert a customer from dal to bl
        /// </summary>
        /// <param name="c"></param>
        /// <returns>CustomerBL</returns>
        public CustomerBL convertDalCustomerToBl(Customer c)
        {
            List<ParcelAtCustomer> parcelSendedByCustomers = new List<ParcelAtCustomer>();
            List<ParcelAtCustomer> parcelSendedToCustomers = new List<ParcelAtCustomer>();
            List<Parcel> parcels = dalObject.GetParcel().Cast<Parcel>().ToList();
            parcels.ForEach(p =>
            {
                if (p.SenderID == c.ID)
                    parcelSendedByCustomers.Add(new ParcelAtCustomer { ID = p.ID, Weight = p.Weight, Priority = p.Priority, ParcelStatus = findParcelStatus(p) });
                if (p.TargetID == c.ID)
                    parcelSendedToCustomers.Add(new ParcelAtCustomer { ID = p.ID, Weight = p.Weight, Priority = p.Priority, ParcelStatus = findParcelStatus(p) });

            });

            return new CustomerBL
            {
                ID = c.ID,
                Name = c.Name,
                Phone = c.Phone,
                Location = new LocationBL() { Longitude = c.Longitude, Latitude = c.Latitude },
                parcelSendedByCustomer = parcelSendedByCustomers,
                parcelSendedToCustomer = parcelSendedToCustomers
            };
        }

        /// <summary>
        /// update the customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void updateDataCustomer(ulong id, string name = null, string phone = null)
        {
            Customer customer = dalObject.getCustomerById( c => c.ID == id);
            if (name != null)
            {
                customer.Name = name;
            }
            if (phone != null)
            {
                customer.Phone = phone;
            }
            dalObject.updateCustomer(customer);
        }

        /// <summary>
        /// check if there is a customer with the id
        /// </summary>
        /// <param name="id"></param>
        public void checkIfCustomerWithThisID(ulong id)
        {
            bool check = false;
            IEnumerable<Customer> customers = dalObject.GetCustomer();
            foreach (var customer in customers)
            {
                if (customer.ID == id)
                {
                    check = true;
                }
            }
            if (!check)
                throw new NotExistObjWithID(id, typeof(Customer));
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
    }
}
