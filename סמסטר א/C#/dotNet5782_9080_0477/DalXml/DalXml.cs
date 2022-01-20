using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using IDAL;
using DalApi;

namespace Dal
{
    public partial class DalXml : IDal
    {
        //dir need to be up from bin
        static string dir = @"..\..\..\..\xmlData\";
        static DalXml()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        internal static DalXml Instance;
        /// <summary>
        /// return object of DalXml
        /// </summary>
        public static DalXml getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new DalXml();
                return Instance;
            }
        }
        string parcelFilePath = @"parcelFile.xml";
        string customerFilePath = @"customerFile.xml";
        string stationFilePath = @"stationFile.xml";
        string droneFilePath = @"droneFile.xml";
        string droneChargeFilePath = @"droneChargeFile.xml";

        /// <summary>
        /// constructor
        /// </summary>
        public DalXml()
        {
            if (!File.Exists(dir + parcelFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Parcel>(Dal.DataSource.parcels, dir + parcelFilePath);

            if (!File.Exists(dir + customerFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Customer>(Dal.DataSource.customers, dir + customerFilePath);

            if (!File.Exists(dir + stationFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Station>(Dal.DataSource.stations, dir + stationFilePath);

            if (!File.Exists(dir + droneFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Drone>(Dal.DataSource.drones, dir + droneFilePath);

            if (!File.Exists(dir + droneChargeFilePath))
                XMLTools.SaveListToXMLSerializer<DO.DroneCharge>(Dal.DataSource.droneChargers, dir + droneChargeFilePath);
        }

        /// <summary>
        /// return electricity use
        /// </summary>
        /// <returns></returns>
        public double[] RequestElectric()
        {
            double[] array = { DataSource.Config.Available, DataSource.Config.LightHeight, DataSource.Config.MidHeight, DataSource.Config.HeavyHeight, DataSource.Config.ChargingRate };
            return array;
        }
    }
}