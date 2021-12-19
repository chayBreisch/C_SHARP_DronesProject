using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using static BL.ExceptionsBL;

namespace BL
{
    public class FactoryBL
    {
        public static IBL.Bl factory()
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