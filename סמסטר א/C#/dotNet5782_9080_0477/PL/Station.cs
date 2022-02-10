using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;


namespace PL
{
    public class Station_ : DependencyObject
    {

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID",
                typeof(object),
                typeof(Station_),
                new UIPropertyMetadata(0));


        public int ID
        {
            get
            {
                return (int)GetValue(IDProperty);
            }
            set
            {
                SetValue(IDProperty, value);
            }
        }



        public static readonly DependencyProperty NameProperty =
           DependencyProperty.Register("Name",
               typeof(object),
               typeof(Station_),
               new UIPropertyMetadata(0));

        public int Name {
            get
            {
                return (int)GetValue(NameProperty);
            }
            set
            {
                SetValue(NameProperty, value);
            }
        }


        public static readonly DependencyProperty ChargeSlotsFreeProperty =
          DependencyProperty.Register("ChargeSlotsFree",
              typeof(object),
              typeof(Station_),
              new UIPropertyMetadata(0));


        public int ChargeSlotsFree {
            get
            {
                return (int)GetValue(ChargeSlotsFreeProperty);
            }
            set
            {
                SetValue(ChargeSlotsFreeProperty, value);
            }
        }


        public static readonly DependencyProperty ChargeSlotsBusyProperty =
                  DependencyProperty.Register("ChargeSlotsBusy",
                      typeof(object),
                      typeof(Station_),
               
                      new UIPropertyMetadata(0));

        public int ChargeSlotsBusy {
            get
            {
                return (int)GetValue(ChargeSlotsBusyProperty);
            }
            set
            {
                SetValue(ChargeSlotsBusyProperty, value);
            }
        }

        public static readonly DependencyProperty chargeSlotsProperty =
                  DependencyProperty.Register("chargeSlots",
                      typeof(object),
                      typeof(Station_),
                      new UIPropertyMetadata(0));


        public int chargeSlots {
            get
            {
                return (int)GetValue(chargeSlotsProperty);
            }
            set
            {
                SetValue(chargeSlotsProperty, value);
            }
        }

        public static readonly DependencyProperty LocationProperty =
                  DependencyProperty.Register("Location",
                      typeof(object),
                      typeof(Station_),
                      new UIPropertyMetadata(0));


        public Location Location {
            get
            {
                return (Location)GetValue(LocationProperty);
            }
            set
            {
                SetValue(LocationProperty, value);
            }
        }

        public static readonly DependencyProperty DronesInChargeProperty =
                  DependencyProperty.Register("DronesInCharge",
                      typeof(object),
                      typeof(Station_),
                      new UIPropertyMetadata(0));


        public List<DroneInCharger> DronesInCharge {
            get
            {
                return (List<DroneInCharger>)GetValue(DronesInChargeProperty);
            }
            set
            {
                SetValue(DronesInChargeProperty, value);
            }
        }

        public static readonly DependencyProperty IsActiveProperty =
                  DependencyProperty.Register("IsActive",
                      typeof(object),
                      typeof(Station_),
                      new UIPropertyMetadata(0));


        public bool IsActive {
            get
            {
                return (bool)GetValue(IsActiveProperty);
            }
            set
            {
                SetValue(IsActiveProperty, value);
            }
        }




        public Station_(BO.Station station)
        {
            ID = station.ID;
            Name = station.Name;
            ChargeSlotsFree = station.ChargeSlots - station.DronesInCharge.Count;
            ChargeSlotsBusy = station.DronesInCharge.Count;
            chargeSlots = station.ChargeSlots;
            Location = new Location(station.Location);
            DronesInCharge = new List<DroneInCharger>(station.DronesInCharge);
            IsActive = station.IsActive;
        }
    }
}
