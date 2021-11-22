using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;
using DAL;
namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// returns the parcels by list from dal
        /// </summary>
        /// <returns>DataSource.parcels</returns>
        public List<Parcel> GetParcelByList()
        {
            return DataSource.parcels;
        }

        /// <summary>
        /// returns the parcels from dal
        /// </summary>
        /// <returns>DataSource.parcels</returns>
        public IEnumerable<Parcel> GetParcel()
        {
            foreach (var parcel in DataSource.parcels)
            {
                yield return parcel;
            }
        }

        /// <summary>
        /// return a specific parcel by parcel id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Parcel</returns>
        public Parcel GetSpecificParcel(int id)
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

        /// <summary>
        /// return a specific parcel by drone id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Parcel</returns>
        public Parcel GetSpecificParcelByDroneID(int id)
        {
            try
            {
                return DataSource.parcels.Find(parcel => parcel.DroneID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exceptions(id);
            }
        }

        /// <summary>
        /// return length of parcel list
        /// </summary>
        /// <returns>int</returns>
        public int lengthParcel()
        {
            return DataSource.parcels.Count;
        }

        /// <summary>
        /// add parcel to dal
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            CheckUniqeParcel(parcel.ID);
            DataSource.parcels.Add(parcel);
        }

        /// <summary>
        /// checl uniqe parcel
        /// </summary>
        /// <param name="id"></param>
        public void CheckUniqeParcel(int id)
        {
            if (DataSource.parcels.Any(parcel => parcel.ID == id))
                throw new NotUniqeID(id, typeof(Parcel));
        }

        /// <summary>
        /// update parcel in dal
        /// </summary>
        /// <param name="parcel"></param>
        public void updateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(d => d.ID == parcel.ID);
            DataSource.parcels[index] = parcel;
        }
    }
}
