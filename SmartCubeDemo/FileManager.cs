using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace SmartCubeDemo
{
    class FileManager: IFileManager
    {
        private string path { get; set; }
        private string name { get; set; }
        public string currentFile { get; private set; }
        private List<IFileManager> components;

        public FileManager( string path, string name, List<IFileManager> components)
        {
            this.path = path;
            this.name = name;
            this.components = components;

        }

        public void Save(StreamWriter sw = null)
        {
            var dialog = new MyDialog();
            if (dialog.ShowDialog() == true)
            {
                name = dialog.ResponseText;
                currentFile = Path.Combine(path, name + ".smc");
                using (StreamWriter swr = new StreamWriter(currentFile, false))
                {
                    swr.WriteLine("<?xml version='1.0'?>");
                    swr.WriteLine("<Settings>");
                    foreach (IFileManager cmp in components)
                    {
                        cmp.Save(swr);
                    }
                    swr.WriteLine("</Settings>");
                }
            }
        }

        public void Load(XPathDocument doc = null, XPathNavigator nav = null)
        {

            string fileName = Path.Combine(path, name);
            currentFile = fileName;
            XPathDocument document = new XPathDocument(fileName);
            XPathNavigator navigator = document.CreateNavigator();

            foreach (IFileManager cmp in components)
            {
                cmp.Load(document, navigator);
            }

            navigator = null;
            document = null;
        }
    }
}
