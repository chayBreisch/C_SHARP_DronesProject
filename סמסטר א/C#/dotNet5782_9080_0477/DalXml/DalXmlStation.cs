using DALException;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    public partial class DalXml
    {
        /// <summary>
        /// get stations
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Station> GetStations(Predicate<Station> predicate = null) //predicate = (p => p.IsActive == false); אם רוצים מחוקים לשלוח 
        {

            /*XElement testRoot = XMLTools.LoadData(dir + stationFilePath);
            IEnumerable<Station> stations;
            try
            {
                stations = (from p in testRoot.Elements()
                            select new Station()
                            {
                                ID = Convert.ToInt32(p.Element("ID").Value),
                                ChargeSlots = Convert.ToInt32(p.Element("ChargeSlots").Value),
                                Latitude = Convert.ToDouble(p.Element("Latitude").Value),
                                Longitude = Convert.ToDouble(p.Element("Longitude").Value),
                                Name = Convert.ToInt32(p.Element("Name").Value),
                                IsActive = Convert.ToBoolean(p.Element("IsActive").Value)
                            });
            }
            catch
            {
                // allTests = null;
                throw new EmptyList(typeof(Station));
            }
            if (predicate == null)
                predicate = (p => p.IsActive == true);
            return from station in stations
                   where station.IsActive == false
                   select station;*/


            IEnumerable<Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath);
            predicate ??= ((st) => true);
            return from station in stationList
                   where predicate(station) && station.IsActive == true
                   orderby station.ID
                   select station;
        }

        /// <summary>
        /// get station by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /*    public Station GetStationById(Predicate<Station> predicate)
            {
                XElement stationRoot = XMLTools.LoadData(dir + stationFilePath);
                Station t = new Station();
                try
                {
                    t = (from p in stationRoot.Elements()
                         where Convert.ToInt32(p.Element("ID").Value) == 1////////////////////////////////////////
                         select new Station()
                         {
                             ID = Convert.ToInt32(p.Element("ID").Value),
                             ChargeSlots = Convert.ToInt32(p.Element("ChargeSlots").Value),
                             Latitude = Convert.ToDouble(p.Element("Latitude").Value),
                             Longitude = Convert.ToDouble(p.Element("Longitude").Value),
                             Name = Convert.ToInt32(p.Element("Name").Value),
                             IsActive = Convert.ToBoolean(p.Element("IsActive").Value)
                         }).First();
                }
                catch (Exception e) { }
                return t;
            }*/
        public Station GetStationById(Predicate<Station> predicate)
        {
            Station station = new Station();
            try
            {
                IEnumerable<Station> stationist = XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath);
                station = (from statio in stationist
                           where predicate(statio)
                           select statio).First();
            }
            catch (Exception e) { }
            return station;
        }

        /// <summary>
        /// add station
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            try
            {
                XElement stationRoot = XMLTools.LoadData(dir + stationFilePath);
                XElement stationElement;
                stationElement = (from p in stationRoot.Elements()
                                  //where Convert.ToInt32(p.Element("ID").Value) == station.ID
                                  where stationRoot.LastNode == p
                                  select p).First();
                XElement ID = new XElement("ID", station.ID);
                XElement ChargeSlots = new XElement("ChargeSlots", station.ChargeSlots);
                XElement Latitude = new XElement("Latitude", station.Latitude);
                XElement Longitude = new XElement("Longitude", station.Longitude);
                XElement Name = new XElement("Name", station.Name);
                XElement IsActive = new XElement("IsActive", station.IsActive);
                stationRoot.Add(new XElement("station", ID, ChargeSlots, Latitude, Longitude, Name, IsActive));
            }

            catch (Exception)
            {
                throw new NotUniqeID(station.ID, typeof(Station));
            }
        }

        /// <summary>
        /// remove station
        /// </summary>
        /// <param name="id"></param>
        public void RemoveStation(int id)
        {
            Station station = GetStationById(s => s.ID == id);
            station.IsActive = false;
            UpdateStation(station);
        }

        /// <summary>
        /// update station details
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(Station station)
        {
            XElement stationRoot = XMLTools.LoadData(dir + stationFilePath);
            XElement stationElement = (from p in stationRoot.Elements()
                                       where Convert.ToInt32(p.Element("ID").Value) == station.ID
                                       select p).FirstOrDefault();
            stationElement.Element("ID").Value = station.ID.ToString();
            stationElement.Element("ChargeSlots").Value = station.ChargeSlots.ToString();
            stationElement.Element("Latitude").Value = station.Latitude.ToString();
            stationElement.Element("Longitude").Value = station.Longitude.ToString();
            stationElement.Element("Name").Value = station.Name.ToString();
            stationElement.Element("IsActive").Value = station.IsActive.ToString();
            stationRoot.Save(dir + stationFilePath);
        }

        /// <summary>
        /// return count of stations
        /// </summary>
        /// <returns></returns>
        public int LengthStation()
        {
            return XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath).ToList().Count;
        }

        /// <summary>
        /// get deleted stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetDeletedStations()
        {
            IEnumerable<Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath);
            return from station in stationList
                   where station.IsActive == false
                   orderby station.ID
                   select station;
        }

    }
}
