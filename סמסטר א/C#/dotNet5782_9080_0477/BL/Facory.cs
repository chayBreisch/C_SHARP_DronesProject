using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;

/*    public class Factory
    {
        public IBL.Bl factory(string obj)
        {
            switch (obj)
            {
                case "BL":
                    return new BL();
                default:
                    throw new Exception();
            }
            return new BL();
        }
    }
}*/


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