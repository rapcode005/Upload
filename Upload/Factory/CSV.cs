using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Upload.Models;

namespace Upload.Factory
{
    public class CSV : IFileExt
    {
        private IFormFile _file;

        public CSV(IFormFile file)
        {
            _file = file;
        }

        public List<Files> getDataFromFile(string path)
        {
            List<Files> ListFiles = new List<Files>();
            using (FileStream fileStream = System.IO.File.Create(path + _file.FileName))
            {
                _file.CopyTo(fileStream);
                fileStream.Flush();
                string csvD = System.IO.File.ReadAllText(path);

                foreach (string row in csvD.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        Files newFile = new Files();
                        string[] data = row.Split(',');
                        newFile.ti = data[0];
                        newFile.a = decimal.Parse(data[1]);
                        newFile.cc = data[2];
                        newFile.td = Convert.ToDateTime(data[3]);
                        newFile.s = data[4];
                        ListFiles.Add(newFile);
                    }
                }

                return ListFiles;
            }
        }

    }
}
