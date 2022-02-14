using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
namespace Dal
{
    internal sealed partial class DalObject : IDal
    {
        private DalObject()
        {
            DataSource.Initialize();
        }
        private static DalObject Instance;
        public static DalObject getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new DalObject();
                return Instance;
            }
        }

        public double[] RequestElectric()
        {
            double[] array = { DataSource.Config.Available, DataSource.Config.LightHeight, DataSource.Config.MidHeight, DataSource.Config.HeavyHeight, DataSource.Config.ChargingRate };
            return array;
        }

    }
}