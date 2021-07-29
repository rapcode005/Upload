using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upload.Models
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> options): base(options)
        {

        }

        public DbSet<Files> FilesTB { get; set; }
    }
}
