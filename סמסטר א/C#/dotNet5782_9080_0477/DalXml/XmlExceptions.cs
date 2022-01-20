using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    class XmlExceptions
    {
        public class XMLFileLoadCreateException : Exception
        {
            public XMLFileLoadCreateException(string filePath, Exception ex) : base($"fail to create xml file: {filePath}")
            {

            }
        }
        public class XMLFileLoadFail : Exception
        {
            public XMLFileLoadFail(string filePath) : base($"fail to create xml file: {filePath}")
            {

            }
        }
    }
}
