using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Exceptions : Exception
    {
        public Exceptions(ulong id) : base($"not found {id}")
        {

        }
        public Exceptions(int id) : base($"not found {id}")
        {

        }
        public Exceptions(string id) : base($"dont have enough battery {id}")
        {

        }
    }
    public class NotUniqeID : Exception
    {

        public NotUniqeID(int id, Type type) : base($"{id} is exist in {type.GetType()}")
        {

        }

        public NotUniqeID(ulong id, Type type) : base($"{id} is exist in {type.GetType()}")
        {

        }
        public NotUniqeID(int droneId, int stationId, Type type) : base($"there is a {type.GetType()} that is charging {droneId} in {stationId} station")
        {

        }
    }
    public class NotExistObjWithID : Exception
    {
        public NotExistObjWithID(int id, Type type) : base($"there is no {type} with ")
        {

        }

        public NotExistObjWithID(ulong id, Type type) : base($"there is no {type} with ")
        {

        }

    }
    public class NotEmptyChargeSlots : Exception
    {
        public NotEmptyChargeSlots(int id) : base($"you can't charge your drone in {id} station")
        {

        }

    }


}
