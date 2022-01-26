﻿using System;
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
            public CanNotUpdateDrone(int id, string str, Exception e) : base($"{id}: {str}")
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

        public class CantReturnBLObject : Exception
        {
            public CantReturnBLObject(Exception e) : base($"can't return BL object")
            {

            }
        }

        public class CantRemoveItem : Exception
        {
            public CantRemoveItem(Type type) : base($"can't remove {type}")
            {

            }
        }
        public class NoParcelsToDeliver : Exception
        {
            public NoParcelsToDeliver() : base($"no parcel matched")
            {

            }
        }
    }
}
