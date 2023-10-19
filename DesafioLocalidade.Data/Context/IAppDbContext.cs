using DesafioLocalidade.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioLocalidade.Data.Context
{
    public interface IAppDbContext
    {
        DbSet<IBGE> IBGE { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
