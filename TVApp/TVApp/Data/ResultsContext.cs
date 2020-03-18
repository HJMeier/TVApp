using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TVApp.Models;

namespace TVApp.Data
{
    public class ResultsContext : DbContext
    {
        public ResultsContext (DbContextOptions<ResultsContext> options)
            : base(options)
        {
        }

        public DbSet<TVApp.Models.Result> Result { get; set; }
    }
}
