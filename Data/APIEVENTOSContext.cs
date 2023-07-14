using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIEVENTOS.Models;

namespace APIEVENTOS.Data
{
    public class APIEVENTOSContext : DbContext
    {
        public APIEVENTOSContext (DbContextOptions<APIEVENTOSContext> options)
            : base(options)
        {
        }

        public DbSet<APIEVENTOS.Models.Usuario> Usuario { get; set; }

        public DbSet<APIEVENTOS.Models.Evento>? Evento { get; set; }

        public DbSet<APIEVENTOS.Models.Venta>? Venta { get; set; }
    }
}
