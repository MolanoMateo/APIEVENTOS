using Microsoft.EntityFrameworkCore;

namespace APIEVENTOS.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> clientes { get; set; }


    }
}
