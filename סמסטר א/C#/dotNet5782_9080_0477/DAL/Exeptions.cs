using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Exeptions : Exception
    {
        public Exeptions(ulong id) : base($"not found {id}")
        {

        }
        public Exeptions(int id) : base($"not found {id}")
        {

        }
        public Exeptions(string id) : base($"dont have enough battery {id}")
        {

        }
    }
    public class NotUniqeID : Exception
    {

        public NotUniqeID(int id, string type) : base($"{id} is exist int {type}")
        {

        }

        public NotUniqeID(ulong id, string type) : base($"{id} is exist int {type}")
        {

        }
    }

}
