using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KushaFlow.Models;

namespace KushaFlow.Models
{
    public class KushaFlowContext : DbContext
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<KushaFlow.Models.StudentIsWork> StudentIsWork { get; set; }
        public DbSet<WorkDownload> WorkDownloads { get; set; }

        public KushaFlowContext(DbContextOptions<KushaFlowContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        

    }
}
