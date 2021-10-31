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
        public IEnumerable<Parcel> GetParcel()
        {
            List<Parcel> parcels = new List<Parcel>();
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                parcels.Add(DataSource.parcels[i]);
            }
            return parcels;
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

        public void AddParcel(int id, int senderId, int targetId, WeightCatagories weight, Priorities priority)
        {
            Parcel newParcial = new Parcel();
            newParcial.ID = id;
            newParcial.SenderID = senderId;
            newParcial.TargetID = targetId;
            newParcial.Weight = weight;
            newParcial.Priority = priority;
            DataSource.parcels[DataSource.parcels.Count - 1] = newParcial;
        }
    }
}
