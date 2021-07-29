using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Upload.Models;

namespace Upload.Factory
{
    public class XML : IFileExt
    {
        private IFormFile _file;

        public XML(IFormFile file)
        {
            _file = file;
        }

        public List<Files> getDataFromFile(string path)
        {
            List<Files> ListFiles = new List<Files>();
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode, xmlnode2;

            using (FileStream fileStream = System.IO.File.Create(path + _file.FileName))
            {
                _file.CopyTo(fileStream);
            }

            using (FileStream fs = new FileStream(path
                + _file.FileName, FileMode.Open, FileAccess.Read))
            {
                xmldoc.Load(fs);
                xmlnode = xmldoc.GetElementsByTagName("Transaction");
                for (int i = 0; i <= xmlnode.Count - 1; i++)
                {
                    Files newFile = new Files();
                    newFile.ti = xmlnode[i].ParentNode.Attributes["Id"].Value;
                    newFile.a = decimal.Parse(xmlnode[i].ChildNodes.Item(1).
                        ChildNodes.Item(0).InnerText);
                    newFile.cc = xmlnode[i].ChildNodes.Item(1).
                        ChildNodes.Item(1).InnerText;
                    newFile.td = Convert.ToDateTime(xmlnode[i].ChildNodes.Item(0).InnerText);
                    newFile.s = xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                    ListFiles.Add(newFile);
                }

                return ListFiles;
            }
        
        }
    }
}
