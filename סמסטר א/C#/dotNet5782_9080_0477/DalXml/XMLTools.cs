﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using static Dal.XmlExceptions;

namespace Dal
{
    public class XMLTools
    {
        #region SaveLoadWithXMLSerializer
        public static void SaveListToXMLSerializer<T>(IEnumerable<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw new XMLFileLoadCreateException(filePath, ex);
            }
        }

        public static IEnumerable<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    IEnumerable<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    list = (IEnumerable<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);  
                throw new XMLFileLoadCreateException(filePath, ex);
            }
            throw new Exception();
        }
        #endregion

        public static XElement LoadData(string filePath)
        {
            try
            {
                return XElement.Load(filePath);
            }
            catch
            {
                //Console.WriteLine("File upload problem");
                //return null;
                throw new XMLFileLoadFail(filePath);
            }
        }
    }
}
