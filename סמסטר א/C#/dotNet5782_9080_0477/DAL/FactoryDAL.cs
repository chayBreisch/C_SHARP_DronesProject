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
            try
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
            catch (Exception e)
            {
                throw new CantReturnDalObject();
            }
        }
    }
}