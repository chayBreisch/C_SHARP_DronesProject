using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DAL;
using IBL.BO;

namespace BL
{
    public partial class BL
    {
        public static void checkUniqeIdParcel(int id, IDAL.IDal dalObject)
        {
            List<Parcel> parcels = dalObject.GetParcelByList();
            parcels.ForEach(p =>
            {
                if (p.ID == id)
                    throw new NotUniqeID(id, typeof(Parcel));
            });
        }

        public void AddParcel(ulong sender, ulong target, int Weight, int priority)
        {
            ParcelBL parcel = new ParcelBL();
            parcel.Sender = new CustomerBL();
            parcel.Reciever = new CustomerBL();
            //parcel.ID = id;
            checkIfCustomerWithThisID(sender);
            checkIfCustomerWithThisID(target);
            parcel.Sender.ID = sender;
            parcel.Reciever.ID = target;
            parcel.Weight = (WeightCatagories)Weight;
            parcel.Priorities = (Priorities)priority;
            parcel.Requesed = DateTime.Now;
            parcel.Scheduled = new DateTime();
            parcel.PickedUp = new DateTime();
            parcel.Delivered = new DateTime();
            parcel.Drone = null;
            AddParcelToDal(sender, target, Weight, priority);

        }

        public void AddParcelToDal(/*ulong id,*/ ulong sender, ulong target, int Weight, int priority)
        {
            Parcel parcel = new Parcel();
            parcel.ID = dalObject.lengthParcel() + 1;
            checkUniqeIdParcel(parcel.ID, dalObject);
            parcel.SenderID = sender;
            parcel.TargetID = target;
            parcel.Weight = (WeightCatagories)Weight;
            parcel.Priority = (Priorities)priority;
            dalObject.AddParcel(parcel);
        }

        public List<ParcelBL> GetParcelsBL()
        {

            IEnumerable<Parcel> parcels = dalObject.GetParcel();
            List<ParcelBL> parcel1 = new List<ParcelBL>();
            foreach (var parcel in parcels)
            {
                parcel1.Add(convertDalToParcelBL(parcel));
            }
            return parcel1;
        }
        public ParcelBL GetSpecificParcelBL(int id)
        {
            try
            {
                return convertDalToParcelBL(dalObject.GetSpecificParcel(id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Parcel));
            }
        }

        public List<Parcel> getParcelsWithoutoutDrone()
        {
            IEnumerable<Parcel> parcels = dalObject.GetParcel();
            List<Parcel> parcels1 = new List<Parcel>();
            foreach (var parcel in parcels)
            {
                if (parcel.DroneID == 0)
                {
                    parcels1.Add(parcel);
                }
            }
            return parcels1;
        }


        public ParcelBL convertDalToParcelBL(Parcel p)
        {
            CustomerBL sender = convertDalCustomerToBl(dalObject.GetSpecificCustomer(p.SenderID));
            CustomerBL target = convertDalCustomerToBl(dalObject.GetSpecificCustomer(p.TargetID));
            DroneBL drone = convertDalDroneToBl(dalObject.GetSpecificDrone(p.DroneID));
            return new ParcelBL
            {
                ID = p.ID,
                Sender = sender,
                Reciever = target,
                Weight = p.Weight,
                Priorities = p.Priority,
                PickedUp = p.PickedUp,
                Drone = drone,
                Requesed = p.Requested,
                Scheduled = p.Scheduled,
                Delivered = p.Delivered,
            };
        }

        private ParcelStatus findParcelStatus(Parcel parcel)
        {
            if (parcel.Requested.Equals(null))
                return (ParcelStatus)0;
            else if (parcel.Scheduled.Equals(null))
                return (ParcelStatus)1;
            else if (parcel.PickedUp.Equals(null))
                return (ParcelStatus)2;
            return (ParcelStatus)3;
        }
    }

}
