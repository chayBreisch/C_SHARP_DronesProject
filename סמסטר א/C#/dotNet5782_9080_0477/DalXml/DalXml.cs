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

        


        /*public IEnumerable<DO.Test> getAllTests()
        {

            XElement testRoot = DL.XMLTools.LoadData(dir + testFilePath);
            IEnumerable<DO.Test> allTests;
            try
            {
                allTests = (from p in testRoot.Elements()
                            select new DO.Test()
                            {
                                IdTest = Convert.ToInt32(p.Element("IdTest").Value),
                                CourseName = p.Element("CourseName").Value,
                                TestDate = Convert.ToDateTime(p.Element("TestDate").Value)
                            });
            }
            catch
            {
                // allTests = null;
                throw new Exception();
            }
            return allTests;
        }

        public DO.Test GetTestById(int id)
        {
            XElement testRoot = DL.XMLTools.LoadData(dir + testFilePath);
            DO.Test t = new DO.Test();
            try
            {
                t = (from p in testRoot.Elements()
                     where Convert.ToInt32(p.Element("IdTest").Value) == id
                     select new DO.Test()
                     {
                         IdTest = Convert.ToInt32(p.Element("IdTest").Value),
                         CourseName = p.Element("CourseName").Value,
                         TestDate = Convert.ToDateTime(p.Element("TestDate").Value)
                     }).First();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("no element");
                throw new Exception("no test", e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return t;
        }

        public void AddTest(DO.Test test)
        {
            XElement testRoot = DL.XMLTools.LoadData(dir + testFilePath);
            XElement testElement;
            testElement = (from p in testRoot.Elements()
                           where Convert.ToInt32(p.Element("IdTest").Value) == test.IdTest
                           select p).FirstOrDefault();
            if (testElement == null)
            {
                XElement IdTest = new XElement("IdTest", test.IdTest);
                XElement CourseName = new XElement("CourseName", test.CourseName);
                XElement TestDate = new XElement("TestDate", test.TestDate.ToString("O"));
                testRoot.Add(new XElement("test", IdTest, CourseName, TestDate));
                testRoot.Save(dir + testFilePath);
            }
            else
            {
                Console.WriteLine("cannot adding test with id " + test.IdTest + "...");
            }
        }

        public bool RemoveTest(int id)
        {
            XElement testRoot = DL.XMLTools.LoadData(dir + testFilePath);
            XElement testElement;
            try
            {
                testElement = (from p in testRoot.Elements()
                               where Convert.ToInt32(p.Element("IdTest").Value) == id
                               select p).FirstOrDefault();
                if (testElement != null)
                {
                    testElement.Remove();
                    testRoot.Save(dir + testFilePath);
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateTest(DO.Test test)
        {
            XElement testRoot = DL.XMLTools.LoadData(dir + testFilePath);
            XElement testElement = (from p in testRoot.Elements()
                                    where Convert.ToInt32(p.Element("IdTest").Value) == test.IdTest
                                    select p).FirstOrDefault();
            testElement.Element("CourseName").Value = test.CourseName;
            testElement.Element("TestDate").Value = test.TestDate.ToString();
            testRoot.Save(dir + testFilePath);
        }*/

    }
}