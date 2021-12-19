using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using static BL.ExceptionsBL;
using BlApi;

namespace BL
{
    public class FactoryBL
    {
        public static BlApi.Bl factory(string obj)
        {
            try
            {
                return BL.GetInstance();
            }
            catch (Exception e)
            {
                throw new CantReturnBLObject();
            }
        }
    }
}