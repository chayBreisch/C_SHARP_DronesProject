using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;
using static BL.ExceptionsBL;
using DALException;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL
    {

        #region add parcel functions

        /// <summary>
        /// add a parcel to the bl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="Weight"></param>
        /// <param name="priority"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(ulong sender, ulong target, int Weight, int priority)
        {
            lock (dalObject)
            {
                checkIfCustomerWithThisID(sender);
                checkIfCustomerWithThisID(target);
                addParcelToDal(sender, target, Weight, priority);
            }
        }

        /// <summary>
        /// add a parcel to the dal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="Weight"></param>
        /// <param name="priority"></param>
        private void addParcelToDal(ulong sender, ulong target, int Weight, int priority)
        {
            DO.Parcel parcel = new DO.Parcel();
            parcel.ID = dalObject.LengthParcel() + 1;
            checkUniqeIdParcel(parcel.ID, dalObject);
            parcel.SenderID = sender;
            parcel.TargetID = target;
            parcel.Weight = (DO.WeightCatagories)Weight;
            parcel.Priority = (DO.Priorities)priority;
            parcel.IsActive = true;
            dalObject.AddParcel(parcel);
        }
        #endregion

        #region get parcel/s functions

        /// <summary>
        /// return all the parcels from the dal converted to bl
        /// </summary>
        /// <returns>List<ParcelBL> </returns>
        private IEnumerable<BO.Parcel> getParcelsBL()
        {
            IEnumerable<DO.Parcel> parcels = dalObject.GetParcels();
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel GetSpecificParcelBL(int id)
        {
            try
            {
                lock (dalObject)
                {
                    return convertDalToParcelBL(dalObject.GetParcelBy(p => p.ID == id));
                }
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DO.Parcel), e);
            }
        }

        /// <summary>
        /// return active ParcelToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelsToList()
        {
            lock (dalObject)
            {
                IEnumerable<BO.Parcel> parcels = getParcelsBL();
                List<ParcelToList> parcels1 = new List<ParcelToList>();
                foreach (var parcel in parcels)
                {
                    parcels1.Add(new ParcelToList(parcel, dalObject));
                }
                return parcels1;
            }
        }

        /// <summary>
        /// return all ParcelToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetDeletedParcelsToList()
        {
            lock (dalObject)
            {
                IEnumerable<DO.Parcel> parcels = dalObject.GetDeletedParcels();
                List<ParcelToList> parcels1 = new List<ParcelToList>();
                foreach (var parcel in parcels)
                {
                    parcels1.Add(new ParcelToList(convertDalToParcelBL(parcel), dalObject));
                }
                return parcels1;
            }
        }

        /// <summary>
        /// get parcelToList by condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelsToListByCondition(Predicate<ParcelToList> predicate)
        {
            //try todo
            lock (dalObject)
            {
                return (from parcel in GetParcelsToList()
                        where predicate(parcel)
                        select parcel);
            }
        }

        //public IEnumerable<ParcelToList> GetParcelsToListByCondition(Predicate<DO.Parcel> predicate)
        //{
        //    //try todo
        //    lock (dalObject)
        //    {
        //        return (from parcel in dalObject.GetParcels()
        //                where predicate(parcel)
        //                select new ParcelToList(parcel , dalObject));
        //    }
        //}


        /// <summary>
        /// get parcels by condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Parcel> GetParcelsByCondition(Predicate<BO.Parcel> predicate)
        {
            lock (dalObject)
            {
                return (from parcel in getParcelsBL()
                        where predicate(parcel)
                        select parcel);
            }
        }
        #endregion

        #region update parcel functions

        /// <summary>
        /// update parcel
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateParecl(BO.Parcel parcel)
        {
            lock (dalObject)
            {
                dalObject.UpdateParcel(convertParcelBlToDal(parcel));
            }
        }

        /// <summary>
        /// update parcel
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateParecl(ParcelToList parcel)
        {
            lock (dalObject)
            {
                dalObject.UpdateParcel(convertParcelBlToDal(ConvertParcelToListToParcelBL(parcel)));
            }
        }
        #endregion

        #region convert parcel functions

        /// <summary>
        /// convert a parcel from dal to bl
        /// </summary>
        /// <param name="p"></param>
        /// <returns>ParcelBL</returns>
        private BO.Parcel convertDalToParcelBL(DO.Parcel p)
        {
            DO.Customer sender = dalObject.GetCustomerById(c => c.ID == p.SenderID);
            DO.Customer target = dalObject.GetCustomerById(c => c.ID == p.TargetID);
            BO.Drone drone = new BO.Drone();

            if (p.DroneID != 0)
                drone = convertDalDroneToBl(dalObject.GetDroneById(d => d.ID == p.DroneID));
            return new BO.Parcel(p.ID, p.Weight, p.Priority, p.Requested, p.Scheduled, p.Delivered, p.PickedUp, drone, sender.ID, target.ID, p.IsActive, dalObject);
        }

        /// <summary>
        /// convert parcel bl to dal
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Parcel convertParcelBlToDal(BO.Parcel parcel)
        {
            return new DO.Parcel
            {
                ID = parcel.ID,
                Weight = parcel.Weight,
                SenderID = parcel.Sender.ID,
                TargetID = parcel.Reciever.ID,
                Priority = parcel.Priorities,
                DroneID = parcel.Drone.ID,
                Scheduled = parcel.Scheduled,
                Requested = parcel.Requesed,
                PickedUp = parcel.PickedUp,
                Delivered = parcel.Delivered,
                IsActive = parcel.IsActive
            };
        }

        /// <summary>
        /// convert ParcelToList to ParcelBL
        /// </summary>
        /// <param name="parcelToList"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel ConvertParcelToListToParcelBL(ParcelToList parcelToList)
        {
            return GetSpecificParcelBL(parcelToList.ID);
        }
        #endregion

        /// <summary>
        /// returns the status of the parcel
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>ParcelStatus</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ParcelStatus findParcelStatus(BO.Parcel parcel)
        {
            lock (dalObject)
            {
                if (parcel.Delivered != null)
                    return (ParcelStatus)3;
                else if (parcel.PickedUp != null)
                    return (ParcelStatus)2;
                else if (parcel.Scheduled != null)
                    return (ParcelStatus)1;
                return (ParcelStatus)0;
            }
        }

        /// <summary>
        /// remove a parcel from dataSource list
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveParcel(int id)
        {
            lock (dalObject)
            {
                BO.Drone drone = GetSpecificDroneBLWithDeleted(dalObject.GetParcelBy(p => p.ID == id).DroneID);
                if (drone != null)
                    throw new CantRemoveItem(typeof(BO.Parcel));
                dalObject.RemoveParcel(id);
            }
        }

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void checkUniqeIdParcel(int id, IDAL.IDal dalObject)
        {
            lock (dalObject)
            {
                IEnumerable<DO.Parcel> parcels = dalObject.GetParcels();
                if (parcels.Any(p => p.ID == id))
                    throw new NotUniqeID(id, typeof(DO.Parcel));
            }
        }
    }
}
