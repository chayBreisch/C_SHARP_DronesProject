using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALException
{
    public class NotUniqeID : Exception
    {

        public NotUniqeID(int id, Type type) : base($"{id} is exist in {type}")
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
        public NotExistObjWithID(ulong id, Type type) : base($"there is no {type} with {id}")
        {

        }
        public NotExistObjWithID(int id, Type type, Exception e) : base($"there is no {type} with {id}")
        {

        }
        public NotExistObjWithID(int id, Type type) : base($"there is no {type} with {id}")
        {

        }
        public NotExistObjWithID(Type type, Exception e) : base($"there is no {type} with this id")
        {

        }

        public NotExistObjWithID(ulong id, Type type, Exception e) : base($"there is no {type} with {id}")
        {

        }
    }

    public class CantReturnDalObject : Exception
    {
        public CantReturnDalObject() : base($"can't return dal object")
        {

        }
        public CantReturnDalObject(Exception e) : base($"can't return dal object")
        {

        }
    }
    public class EmptyList : Exception
    {
        public EmptyList(Type t) : base($"{t} empty list")
        {

        }
    }
}
