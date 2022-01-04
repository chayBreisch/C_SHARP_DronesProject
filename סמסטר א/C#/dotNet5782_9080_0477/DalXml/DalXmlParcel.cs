using DAL;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalXml
{
    public partial class DalXml
    {
        private void checkUniqeParcel(int id)
        {
            if (GetParcels().Any(parcel => parcel.ID == id))
                throw new NotUniqeID(id, typeof(Parcel));
        }

        public void AddParcel(Parcel parcel)
        {
            IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            checkUniqeParcel(parcel.ID);
            parcelList.ToList().Add(parcel);
            XMLTools.SaveListToXMLSerializer<Parcel>(parcelList, dir + parcelFilePath);
        }

        public void RemoveParcel(int id)
        {
            IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            if (!parcelList.Any(d => d.ID == id))
            {
                throw new NotExistObjWithID(id, typeof(Parcel));
            }
            Parcel parcel = GetParcelBy(id);
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }
        public void UpdateParcel(Parcel parcel)
        {
            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath).ToList();

            int index = parcelList.FindIndex(d => d.ID == parcel.ID);

            if (index == -1)
                throw new NotExistObjWithID(parcel.ID, typeof(Parcel));
            parcelList[index] = parcel;
            XMLTools.SaveListToXMLSerializer<Parcel>(parcelList, dir + parcelFilePath);
        }
        public Parcel GetParcelBy(int id)
        {
            try
            {
                IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
                return (from parcel in parcelList
                        where parcel.ID == id
                        select parcel).First();
            }
            catch (Exception e)
            {
                throw new NotExistObjWithID(id, typeof(Parcel), e);
            }

        }

        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicat = null)
        {
            IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            predicat ??= ((st) => true);
            return from parcel in parcelList
                   where predicat(parcel) && parcel.IsActive == true
                   orderby parcel.ID
                   select parcel;
        }

        public IEnumerable<Parcel> GetDeletedParcels()
        {
            IEnumerable<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            return from parcel in parcelList
                   where parcel.IsActive == false
                   orderby parcel.ID
                   select parcel;
        }

        public int LengthParcel()
        {
            return XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath).ToList().Count;
        }
    }
}
