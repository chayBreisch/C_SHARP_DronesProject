using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{

    public class InValidInput : Exception
    {
        public InValidInput(string input) : base($"invalid {input} input")
        {

        }
    }
}
