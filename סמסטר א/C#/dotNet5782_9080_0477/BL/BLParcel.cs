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
        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdParcel(int id, IDAL.IDal dalObject)
        {
            List<Parcel> parcels = dalObject.GetParcelByList();
            if (parcels.Any(p => p.ID == id))
                throw new NotUniqeID(id, typeof(Parcel));
        }

        /// <summary>
        /// add a parcel to the bl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="Weight"></param>
        /// <param name="priority"></param>
        public void AddParcel(ulong sender, ulong target, int Weight, int priority)
        {
            ParcelBL parcel = new ParcelBL();
            parcel.Sender = new CustomerBL();
            parcel.Reciever = new CustomerBL();
            checkIfCustomerWithThisID(sender);
            checkIfCustomerWithThisID(target);
            parcel.Sender = GetSpecificCustomerBL(sender);
            parcel.Reciever = GetSpecificCustomerBL(target);
            parcel.Weight = (WeightCatagories)Weight;
            parcel.Priorities = (Priorities)priority;
            parcel.Requesed = DateTime.Now;
            parcel.Scheduled = new DateTime();
            parcel.PickedUp = new DateTime();
            parcel.Delivered = new DateTime();
            parcel.Drone = null;
            AddParcelToDal(sender, target, Weight, priority);

        }

        /// <summary>
        /// add a parcel to the dal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="Weight"></param>
        /// <param name="priority"></param>
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

        /// <summary>
        /// return all the parcels from the dal converted to bl
        /// </summary>
        /// <returns>List<ParcelBL> </returns>
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

        /// <summary>
        /// returns a specific parcel by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Parcel</returns>
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

        /// <summary>
        /// return parcels that are not connected to a drone
        /// </summary>
        /// <returns> List<Parcel></returns>
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

        /// <summary>
        /// convert a parcel from dal to bl
        /// </summary>
        /// <param name="p"></param>
        /// <returns>ParcelBL</returns>
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

        /// <summary>
        /// returns the status of the parcel
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>ParcelStatus</returns>
        public ParcelStatus findParcelStatus(Parcel parcel)
        {
            if (parcel.Requested == new DateTime())
                return (ParcelStatus)0;
            else if (parcel.Scheduled == new DateTime())
                return (ParcelStatus)1;
            else if (parcel.PickedUp == new DateTime())
                return (ParcelStatus)2;
            return (ParcelStatus)3;
        }
    }

}
