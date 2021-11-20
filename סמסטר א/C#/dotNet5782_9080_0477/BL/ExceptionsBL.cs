using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class ExceptionsBL
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
    }
}
