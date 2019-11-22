using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KushaFlow.Models
{
    public class KushaFlowContext : DbContext
    {

        public DbSet<Student> Students { get; set; }

        public KushaFlowContext(DbContextOptions<KushaFlowContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
