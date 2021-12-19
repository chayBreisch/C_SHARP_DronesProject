using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ExceptionsBL
    {
        public class NotEmptyChargeSlots : Exception
        {
            public NotEmptyChargeSlots(int id) : base($"you can't charge your drone in {id} station")
            {

            }

        }
        public class CanNotUpdateDrone : Exception
        {
            public CanNotUpdateDrone(int id, string str) : base($"{id}: {str}")
            {

            }

        }
        public class OutOfRange : Exception
        {
            public OutOfRange(string str) : base($"{str} out of range")
            {

            }
        }
        public class NoItemWithThisID : Exception
        {
            public NoItemWithThisID(int id ,Type type) : base($"no {type} with {id} id")
            {

            }
        }
        public class CantReturnDalObject : DAL.CantReturnDalObject
        {
            public CantReturnDalObject() : base()
            {

            }
        }
        public class CantReturnBLObject : Exception
        {
            public CantReturnBLObject() : base($"can't return BL object")
            {

            }
        }
    }
}
