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

        public static void checkUniqeIdCustomer(ulong id, IDAL.IDal dalObject)
        {
            List<Customer> customers = dalObject.GetCustomer().ToList();
            customers.ForEach(c =>
            {
                if (c.ID == id)
                    throw new NotUniqeID(id, typeof(Customer));
            });
        }

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

        public CustomerBL GetSpecificCustomerBL(ulong id)
        {
            try
            {
                return convertDalCustomerToBl(dalObject.GetSpecificCustomer(id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Customer));

            }
        }

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

        public void updateDataCustomer(ulong id, string name = null, string phone = null)
        {
            Customer customer = dalObject.GetSpecificCustomer(id);
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
