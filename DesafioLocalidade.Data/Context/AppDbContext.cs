using DesafioLocalidade.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioLocalidade.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<IBGE> IBGE { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) { }
    }
}
