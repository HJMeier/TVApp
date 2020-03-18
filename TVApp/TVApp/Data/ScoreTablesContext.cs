using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TVApp.Models;

namespace TVApp.Data
{
    public class ScoreTablesContext : DbContext
    {
        public ScoreTablesContext (DbContextOptions<ScoreTablesContext> options)
            : base(options)
        {
        }

        public DbSet<TVApp.Models.ScoreTable> ScoreTable { get; set; }
    }
}
