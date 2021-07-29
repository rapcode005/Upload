using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upload.Models;

namespace Upload.FilesData
{
    public interface IFilesData
    {
        Task AddData(List<Files> file);

        Task<List<Files>> GetFilesByCurrency(string curr);

        Task<List<Files>> GetFilesByDataRange(DateTime d1, DateTime d2);

        Task<List<Files>> GetFilesByStatus(string s);
    }
}
