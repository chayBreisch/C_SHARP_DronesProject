using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
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
        public NotExistObjWithID(int id, Type type) : base($"there is no {type} with {id}")
        {

        }

        public NotExistObjWithID(ulong id, Type type) : base($"there is no {type} with {id}")
        {

        }

    }
}
