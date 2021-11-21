using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;

namespace BL
{
    public class Factory
    {
        public static IDAL.IDal factory(string obj)
        {
            switch (obj)
            {
                case "DalObject":
                    return new DalObject.DalObject();
                    break;
                    //default:
                    //return ;
            }
            return new DalObject.DalObject();
        }
    }
}