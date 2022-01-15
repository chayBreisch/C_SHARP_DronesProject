using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DO;
namespace DalXml
{
    public partial class DalXml
    {
        //dir need to be up from bin
        static string dir = @"..\..\..\..\xmlData\";
        static DalXml()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        string parcelFilePath = @"parcelFile.xml";
        string customerFilePath = @"customerFile.xml";
        string stationFilePath = @"stationFile.xml";
        string droneFilePath = @"droneFile.xml";
        string droneChargeFilePath = @"droneChargeFile.xml";

        public DalXml()
        {
            if (!File.Exists(dir + parcelFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Parcel>(DS.DataSource.parcels, dir + parcelFilePath);

            if (!File.Exists(dir + customerFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Customer>(DS.DataSource.customers, dir + customerFilePath);

            if (!File.Exists(dir + stationFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Station>(DS.DataSource.stations, dir + stationFilePath);

            if (!File.Exists(dir + droneFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Drone>(DS.DataSource.drones, dir + droneFilePath);

            if (!File.Exists(dir + droneChargeFilePath))
                XMLTools.SaveListToXMLSerializer<DO.DroneCharge>(DS.DataSource.droneChargers, dir + droneChargeFilePath);
        }
    }
}