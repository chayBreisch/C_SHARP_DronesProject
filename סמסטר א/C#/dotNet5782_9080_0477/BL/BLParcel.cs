using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAL;
using BO;

namespace BL
{
    internal partial class BL
    {
        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdParcel(int id, IDAL.IDal dalObject)
        {
            List<DO.Parcel> parcels = dalObject.GetParcelByList();
            if (parcels.Any(p => p.ID == id))
                throw new NotUniqeID(id, typeof(DO.Parcel));
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
            BO.Parcel parcel = new BO.Parcel();
            checkIfCustomerWithThisID(sender);
            checkIfCustomerWithThisID(target);
            BO.Customer customer = GetSpecificCustomerBL(sender);
            parcel.Sender = new CustomerAtParcel(customer.ID, customer.Name);
            customer = GetSpecificCustomerBL(target);
            parcel.Reciever = new CustomerAtParcel(customer.ID, customer.Name);
            parcel.Weight = (WeightCatagories)Weight;
            parcel.Priorities = (Priorities)priority;
            parcel.Requesed = DateTime.Now;
            parcel.Scheduled = null;
            parcel.PickedUp = null;
            parcel.Delivered = null;
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
            DO.Parcel parcel = new DO.Parcel();
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
        public List<BO.Parcel> GetParcelsBL()
        {

            IEnumerable<DO.Parcel> parcels = dalObject.GetParcel();
            List<BO.Parcel> parcel1 = new List<BO.Parcel>();
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
        public BO.Parcel GetSpecificParcelBL(int id)
        {
            try
            {
                return convertDalToParcelBL(dalObject.getParcelById(p => p.ID == id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DO.Parcel));
            }
        }

        /// <summary>
        /// return parcels that are not connected to a drone
        /// </summary>
        /// <returns> List<Parcel></returns>
        public List<DO.Parcel> getParcelsWithoutoutDrone()
        {
            IEnumerable<DO.Parcel> parcels = dalObject.GetParcel();
            List<DO.Parcel> parcels1 = new List<DO.Parcel>();
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
        public BO.Parcel convertDalToParcelBL(DO.Parcel p)
        {
            DO.Customer sender = dalObject.getCustomerById(c => c.ID == p.SenderID);
            DO.Customer target = dalObject.getCustomerById(c => c.ID == p.TargetID);
            BO.Drone drone = new BO.Drone();

            if (p.DroneID != 0)
                drone = convertDalDroneToBl(dalObject.getDroneById(d => d.ID == p.DroneID));
            return new BO.Parcel(p.ID, p.Weight, p.Priority, p.Requested, p.Scheduled, p.Delivered, p.PickedUp, drone, sender.ID, target.ID, dalObject);

        }

        /// <summary>
        /// return all ParcelToList
        /// </summary>
        /// <returns></returns>
        public List<ParcelToList> getParcelToList()
        {
            List<BO.Parcel> parcels = GetParcelsBL();
            List<ParcelToList> parcels1 = new List<ParcelToList>();
            foreach (var parcel in parcels)
            {
                parcels1.Add(new ParcelToList(parcel, dalObject));
            }
            return parcels1;
        }

        /// <summary>
        /// convert ParcelToList to ParcelBL
        /// </summary>
        /// <param name="parcelToList"></param>
        /// <returns></returns>
        public BO.Parcel convertParcelToListToParcelBL(ParcelToList parcelToList)
        {
            return GetSpecificParcelBL(parcelToList.ID);
        }

        /// <summary>
        /// get parcels by priority
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<ParcelToList> getParcelsByPriority(int status)
        {
            List<ParcelToList> parcelToList = new List<ParcelToList>();
            IEnumerable<BO.Parcel> parcelQuery =
            from parcel in GetParcelsBL()
            where parcel.Priorities == (Priorities)status
            select parcel;
            foreach (var parcel in parcelQuery)
            {
                parcelToList.Add(new ParcelToList(parcel, dalObject));
            }
            return parcelToList;
        }

        public List<ParcelToList> getParcelsByparcelWeight(int status)
        {
            List<ParcelToList> parcelToList = new List<ParcelToList>();
            IEnumerable<BO.Parcel> parcelQuery =
            from parcel in GetParcelsBL()
            where parcel.Weight == (WeightCatagories)status
            select parcel;
            foreach (var parcel in parcelQuery)
            {
                parcelToList.Add(new ParcelToList(parcel, dalObject));
            }
            return parcelToList;
        }

        /*  /// <summary>
          /// returns the status of the parcel
          /// </summary>
          /// <param name="parcel"></param>
          /// <returns>ParcelStatus</returns>
          public ParcelStatus findParcelStatus(DO.Parcel parcel)
          {
              if (parcel.Requested == null)
                  return (ParcelStatus)0;
              else if (parcel.Scheduled == null)
                  return (ParcelStatus)1;
              else if (parcel.PickedUp == null)
                  return (ParcelStatus)2;
              return (ParcelStatus)3;
          }*/

    }

}
