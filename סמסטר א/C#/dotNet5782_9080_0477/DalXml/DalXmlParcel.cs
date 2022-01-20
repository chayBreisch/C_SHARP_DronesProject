using DALException;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public partial class DalXml
    {
        /// <summary>
        /// check uniqe parcel
        /// </summary>
        /// <param name="id"></param>
        private void checkUniqeParcel(int id)
        {
            if (GetParcels().Any(parcel => parcel.ID == id))
                throw new NotUniqeID(id, typeof(Parcel));
        }

        /// <summary>
        /// add parcel to xml
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath).ToList();
            checkUniqeParcel(parcel.ID);
            parcelList.Add(parcel);
            XMLTools.SaveListToXMLSerializer<Parcel>(parcelList, dir + parcelFilePath);
        }

        /// <summary>
        /// remove parcel from xml (isActive = false)
        /// </summary>
        /// <param name="id"></param>
        public void RemoveParcel(int id)
        {
            IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            if (!parcelList.Any(d => d.ID == id))
            {
                throw new NotExistObjWithID(id, typeof(Parcel));
            }
            Parcel parcel = GetParcelBy(p => p.ID == id);
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }

        /// <summary>
        /// update details parcel
        /// </summary>
        /// <param name="parcel"></param>
        public void UpdateParcel(Parcel parcel)
        {
            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath).ToList();

            int index = parcelList.FindIndex(d => d.ID == parcel.ID);

            if (index == -1)
                throw new NotExistObjWithID(parcel.ID, typeof(Parcel));
            parcelList[index] = parcel;
            XMLTools.SaveListToXMLSerializer<Parcel>(parcelList, dir + parcelFilePath);
        }

        /// <summary>
        /// get parcel by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Parcel GetParcelBy(Predicate<Parcel> predicate)
        {
            Parcel parcel1 = new Parcel();
            try
            {
                IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
                parcel1 = (from parcel in parcelList
                           where predicate(parcel)
                           select parcel).First();
            }
            catch (Exception e) { }
            return parcel1;

        }

        /// <summary>
        /// get all parcels from xml
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicat = null)
        {
            IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            predicat ??= ((st) => true);
            return from parcel in parcelList
                   where predicat(parcel) && parcel.IsActive == true
                   orderby parcel.ID
                   select parcel;
        }

        /// <summary>
        /// get deleted parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetDeletedParcels()
        {
            IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            return from parcel in parcelList
                   where parcel.IsActive == false
                   orderby parcel.ID
                   select parcel;
        }

        /// <summary>
        /// get count of parcels
        /// </summary>
        /// <returns></returns>
        public int LengthParcel()
        {
            return XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath).ToList().Count;
        }

        public IEnumerable<Parcel> GetParcelesByCondition(Predicate<Parcel> predicate)
        {
            //try todo

            IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            return from parcel in parcelList
                   where predicate(parcel)
                   orderby parcel.ID
                   select parcel;
        }
    }
}
