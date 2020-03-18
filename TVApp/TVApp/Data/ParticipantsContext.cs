using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TVApp.Models;

namespace TVApp.Data
{
    public class ParticipantsContext : DbContext
    {
        public ParticipantsContext (DbContextOptions<ParticipantsContext> options)
            : base(options)
        {
        }

        public DbSet<TVApp.Models.Participant> Participant { get; set; }
    }
}
