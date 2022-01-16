using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalFacade;
using System.Collections.Generic;
using DALException;

namespace DalObject
{
    internal partial class DalObject
    {
        /// <summary>
        /// returns the parcels by list from dal
        /// </summary>
        /// <returns>DataSource.parcels</returns>
        /*public List<Parcel> GetParcelByList()
        {
            return DataSource.parcels;
        }*/

        /// <summary>
        /// returns the parcels from dal
        /// </summary>
        /// <returns>DataSource.parcels</returns>
        public IEnumerable<Parcel> GetParcels()
        {
            return from parcel in DataSource.parcels
                   where parcel.IsActive == true
                   select parcel;
        }

        /// <summary>
        /// get deleted parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetDeletedParcels()
        {
            return from parcel in DataSource.parcels
                   where parcel.IsActive == false
                   select parcel;
        }
        ///
        /* public Parcel GetSpecificParcel(int id)
         {
             try
             {
                 return DataSource.parcels.Find(parcel => parcel.ID == id);
             }
             catch (ArgumentNullException e)
             {
                 throw new Exceptions(id);
             }
         }
 */

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
        public IEnumerable<Parcel> GetParcelesByCondition(Predicate<Parcel> predicate)
        {
            //try todo
            return (from parcel in DataSource.parcels
                    where predicate(parcel) && parcel.IsActive == true
                    select parcel);
        }
        /// <summary>
        /// return a specific parcel by drone id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Parcel</returns>
       /* public Parcel GetSpecificParcelByDroneID(int id)
        {
            try
            {
                return DataSource.parcels.Find(parcel => parcel.DroneID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }*/

        /// <summary>
        /// return length of parcel list
        /// </summary>
        /// <returns>int</returns>
        public int LengthParcel()
        {
            return DataSource.parcels.Count;
        }

        /// <summary>
        /// add parcel to dal
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            checkUniqeParcel(parcel.ID);
            DataSource.parcels.Add(parcel);
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
        public void UpdateParcel(Parcel parcel)
        {
            int index = getIndexOfParcel(parcel.ID);
            DataSource.parcels[index] = parcel;
        }


        /// <summary>
        /// remove parcel from dataSource
        /// </summary>
        /// <param name="idRemove"></param>
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
