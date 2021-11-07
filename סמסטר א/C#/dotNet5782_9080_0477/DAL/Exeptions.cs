using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Exeptions : Exception
    {
        public Exeptions(ulong id): base($"not found {id}")
        {
           
        }
        public Exeptions(int id) : base($"not found {id}")
        {

        }
    }
}
