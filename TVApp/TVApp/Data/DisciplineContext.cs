using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TVApp.Models;

namespace TVApp.Data
{
    public class DisciplineContext : DbContext
    {
        public DisciplineContext (DbContextOptions<DisciplineContext> options)
            : base(options)
        {
        }

        public DbSet<TVApp.Models.Discipline> Discipline { get; set; }
    }
}
