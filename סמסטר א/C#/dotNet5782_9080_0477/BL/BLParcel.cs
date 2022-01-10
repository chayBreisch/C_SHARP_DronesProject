using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAL;
using BO;
using static BL.ExceptionsBL;

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
            IEnumerable<DO.Parcel> parcels = dalObject.GetParcels();
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
            checkIfCustomerWithThisID(sender);
            checkIfCustomerWithThisID(target);
            //BO.Parcel parcel = new BO.Parcel();
            /*BO.Customer customer = GetSpecificCustomerBL(sender);
            parcel.Sender = new CustomerAtParcel(customer.ID, customer.Name);
            customer = GetSpecificCustomerBL(target);
            parcel.Reciever = new CustomerAtParcel(customer.ID, customer.Name);
            parcel.Weight = (DO.WeightCatagories)Weight;
            parcel.Priorities = (DO.Priorities)priority;
            parcel.Requesed = DateTime.Now;
            parcel.Scheduled = null;
            parcel.PickedUp = null;
            parcel.Delivered = null;
            parcel.Drone = null;*/
            addParcelToDal(sender, target, Weight, priority);

        }

        /// <summary>
        /// add a parcel to the dal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="Weight"></param>
        /// <param name="priority"></param>
        private void addParcelToDal(/*ulong id,*/ ulong sender, ulong target, int Weight, int priority)
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
        public BO.Parcel GetSpecificParcelBL(int id)
        {
            try
            {
                return convertDalToParcelBL(dalObject.GetParcelBy(p => p.ID == id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DO.Parcel), e);
            }
        }

        /// <summary>
        /// return parcels that are not connected to a drone
        /// </summary>
        /// <returns> List<Parcel></returns>
        private IEnumerable<DO.Parcel> getParcelsWithoutoutDrone()
        {
            IEnumerable<DO.Parcel> parcels = dalObject.GetParcels();
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
        /// return active ParcelToList
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelToList> GetParcelsToList()
        {
            IEnumerable<BO.Parcel> parcels = getParcelsBL();
            List<ParcelToList> parcels1 = new List<ParcelToList>();
            foreach (var parcel in parcels)
            {
                parcels1.Add(new ParcelToList(parcel, dalObject));
            }
            return parcels1;
        }

        /// <summary>
        /// return all ParcelToList
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelToList> GetDeletedParcelsToList()
        {
            IEnumerable<DO.Parcel> parcels = dalObject.GetDeletedParcels();
            List<ParcelToList> parcels1 = new List<ParcelToList>();
            foreach (var parcel in parcels)
            {
                parcels1.Add(new ParcelToList(convertDalToParcelBL(parcel), dalObject));
            }
            return parcels1;
        }

        /// <summary>
        /// convert ParcelToList to ParcelBL
        /// </summary>
        /// <param name="parcelToList"></param>
        /// <returns></returns>
        public BO.Parcel ConvertParcelToListToParcelBL(ParcelToList parcelToList)
        {
            return GetSpecificParcelBL(parcelToList.ID);
        }

        /// <summary>
        /// get parcels by priority
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
       /* public List<ParcelToList> getParcelsByPriority(int status)
        {
            List<ParcelToList> parcelToList = new List<ParcelToList>();
            IEnumerable<BO.Parcel> parcelQuery =
            from parcel in GetParcelsBL()
            where parcel.Priorities == (DO.Priorities)status
            select parcel;
            foreach (var parcel in parcelQuery)
            {
                parcelToList.Add(new ParcelToList(parcel, dalObject));
            }
            return parcelToList;
        }

        /// <summary>
        /// get parcels by parcel weight
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<ParcelToList> getParcelsByparcelWeight(int status)
        {
            List<ParcelToList> parcelToList = new List<ParcelToList>();
            IEnumerable<BO.Parcel> parcelQuery =
            from parcel in GetParcelsBL()
            where parcel.Weight == (DO.WeightCatagories)status
            select parcel;
            foreach (var parcel in parcelQuery)
            {
                parcelToList.Add(new ParcelToList(parcel, dalObject));
            }
            return parcelToList;
        }*/


        public IEnumerable<ParcelToList> GetParcelsToListByCondition(Predicate<ParcelToList> predicate)
        {
            //try todo
            return (from parcel in GetParcelsToList()
                    where predicate(parcel)
                    select parcel);
        }


        public IEnumerable<BO.Parcel> GetParcelsByCondition(Predicate<BO.Parcel> predicate)
        {
            return (from parcel in getParcelsBL()
                    where predicate(parcel)
                    select parcel);
        }


        /*public IEnumerable<ParcelToList> getParcelToListWithFilter(int weight, int prioritty)
        {

            if (prioritty == -1)
                return getParcelToListByCondition(parcel => parcel.Weight == (WeightCatagories)prioritty);
            else if (weight == -1)
                return getParcelToListByCondition(parcel => parcel.Priority == (Priorities)weight);
            return getParcelToListByCondition(parcel => parcel.Priority == (Priorities)prioritty && parcel.Weight == (WeightCatagories)weight);
        }*/


        /*public IEnumerable<ParcelToList> getPrcelToListByCondition(Predicate<ParcelToList> predicate)
        {
            //try todo
            return (from parcel in getParcelToList()
                    where predicate(parcel)
                    select parcel);
        }*/
        /// <summary>
        /// returns the status of the parcel
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>ParcelStatus</returns>
        public ParcelStatus findParcelStatus(BO.Parcel parcel)
        {
            if (parcel.Delivered != null)
                return (ParcelStatus)3;
            else if (parcel.PickedUp != null)
                return (ParcelStatus)2;
            else if (parcel.Scheduled != null)
                return (ParcelStatus)1;
            return (ParcelStatus)0;
        }

        /// <summary>
        /// remove a parcel from dataSource list
        /// </summary>
        /// <param name="parcel"></param>
        public void RemoveParcel(int id)
        {
            BO.Drone drone = GetSpecificDroneBLWithDeleted(dalObject.GetParcelBy(p => p.ID == id).DroneID);
            if (drone != null)
            {
                /*drone.parcelInDelivery = null;
                drone.DroneStatus = DroneStatus.Available;
                updateDrone(drone);*/
                throw new CantRemoveItem(typeof(BO.Parcel));
            }
            dalObject.RemoveParcel(id);
        }

        public void updateParecl(BO.Parcel parcel)
        {
            dalObject.UpdateParcel(convertParcelBlToDal(parcel));
        }

        public void updateParecl(BO.ParcelToList parcel)
        {
            dalObject.UpdateParcel(convertParcelBlToDal(ConvertParcelToListToParcelBL(parcel)));
        }

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
                Delivered = parcel.PickedUp,
                IsActive = parcel.IsActive
            };
        }
    }
}
