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

        public List<Parcel> GetParcelByList()
        {
            return DataSource.parcels;
        }
        public IEnumerable<Parcel> GetParcel()
        {
            foreach (var parcel in DataSource.parcels)
            {
                yield return parcel;
            }
        }

        public Parcel GetSpecificParcel(int id)
        {
            try
            {
                return DataSource.parcels.First(parcel => parcel.ID == id);
            }
            catch (ArgumentNullException e)
            {
                throw new Exeptions(id);
            }
        }

        public void AddParcel(Parcel parcel)
        {
            DataSource.parcels.Add(parcel);
        }
    }
}
