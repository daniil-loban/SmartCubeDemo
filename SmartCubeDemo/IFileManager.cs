using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace SmartCubeDemo
{
    interface IFileManager
    {
        void Save(StreamWriter sw = null);
        void Load(XPathDocument doc = null, XPathNavigator nav = null);
    }
}
