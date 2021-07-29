using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upload.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Upload.FilesData
{
    public class SqlFileData : IFilesData
    {
                private FileContext _fileContext;
        public SqlFileData(FileContext fileContext)
        {
            _fileContext = fileContext;
        }

        public async Task AddData(List<Files> file)
        {
            foreach (Files f in file)
            {
                await _fileContext.FilesTB.AddAsync(f);
            }
        }

        public async Task<List<Files>> GetFilesByCurrency(string curr)
        {
            return await _fileContext.FilesTB.Where(m => m.cc == curr).ToListAsync();
        }

        public async Task<List<Files>> GetFilesByDataRange(DateTime d1, DateTime d2)
        {
            return await _fileContext.FilesTB.Where(m => m.td >= d1 && m.td <= d2).ToListAsync();
        }

        public async Task<List<Files>> GetFilesByStatus(string s)
        {
            return await _fileContext.FilesTB.Where(m => m.s == s).ToListAsync();
        }
    }
}
