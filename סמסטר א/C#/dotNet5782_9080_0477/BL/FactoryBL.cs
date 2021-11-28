using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;

namespace BL
{
    public class FactoryBL
    {
        public static IBL.Bl factory(string obj)
        {
            switch (obj)
            {
                case "BL":
                    return new BL();
                    break;
                    //default:
                    //return ;
            }
            return new BL();
        }
    }
}