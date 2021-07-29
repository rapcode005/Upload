using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upload.Models;

namespace Upload.Factory
{
    public interface IFileExt
    {
        List<Files> getDataFromFile(string path);
    }
}
