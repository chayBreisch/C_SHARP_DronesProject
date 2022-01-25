using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BL
{
    class Simulation
    {
        IBL bl;
        public Simulation(IBL bl)
        {
            this.bl = bl;
        }
        public void start(BO.Drone drone, Action<BO.Drone, int> updateDrone, Func<bool> needToStop)
        {
            while (!needToStop())
            {
               
            }
        }
    }
}
