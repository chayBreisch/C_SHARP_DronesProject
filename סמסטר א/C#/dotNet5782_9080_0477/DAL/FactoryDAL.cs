using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDAL
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