using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;


namespace PL
{
    class Station_ : DependencyObject
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public int ChargeSlotsFree { get; set; }
        public int ChargeSlotsBusy { get; set; }


        public Station_(BO.StationToList station)
        {

            this.ID = station.ID;
            this.Name = station.Name;
            this.ChargeSlotsFree = station.ChargeSlotsFree;
            this.ChargeSlotsBusy = station.ChargeSlotsBusy;
            

    }
    }
}
