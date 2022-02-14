using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using IDAL;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DALException;

namespace Dal
{
    internal partial class DalObject : IDal
    {
        /// <summary>
        /// returns the parcels from dal
        /// </summary>
        /// <returns>DataSource.parcels</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate = null)
        {
            return from parcel in DataSource.parcels
                   where parcel.IsActive == true
                   select parcel;
        }

        /// <summary>
        /// get deleted parcels
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetDeletedParcels()
        {
            return from parcel in DataSource.parcels
                   where parcel.IsActive == false
                   select parcel;
        }

        /// <summary>
        /// get parcel by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcelBy(Predicate<Parcel> predicate)
        {
            Parcel parcel1 = new Parcel();
            try
            {
                parcel1 = (from parcel in DataSource.parcels
                           where predicate(parcel) && parcel.IsActive == true
                           select parcel).First();
            }
            catch (Exception e) { }
            return parcel1;

        }

        /// <summary>
        /// get the parcels with a specific condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelesByCondition(Predicate<Parcel> predicate)
        {
            //try todo
            return (from parcel in DataSource.parcels
                    where predicate(parcel) && parcel.IsActive == true
                    select parcel);
        }

        /// <summary>
        /// return length of parcel list
        /// </summary>
        /// <returns>int</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int LengthParcel()
        {
            return DataSource.parcels.Count;
        }

        /// <summary>
        /// add parcel to dal
        /// </summary>
        /// <param name="parcel"></param>
        public int AddParcel(Parcel parcel)
        {
            checkUniqeParcel(parcel.ID);
            DataSource.parcels.Add(parcel);
            return parcel.ID;
        }

        /// <summary>
        /// checl uniqe parcel
        /// </summary>
        /// <param name="id"></param>
        private void checkUniqeParcel(int id)
        {
            if (DataSource.parcels.Any(parcel => parcel.ID == id))
                throw new NotUniqeID(id, typeof(Parcel));
        }

        /// <summary>
        /// update parcel in dal
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel)
        {
            int index = getIndexOfParcel(parcel.ID);
            DataSource.parcels[index] = parcel;
        }


        /// <summary>
        /// remove parcel from dataSource
        /// </summary>
        /// <param name="idRemove"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveParcel(int idRemove)
        {
            Parcel parcel = DataSource.parcels[getIndexOfParcel(idRemove)];
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }

        /// <summary>
        /// return index of parcel in dataSource list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int getIndexOfParcel(int id)
        {
            try
            {
                return DataSource.parcels.FindIndex(p => p.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(Parcel), e);
            }
        }
    }
}
