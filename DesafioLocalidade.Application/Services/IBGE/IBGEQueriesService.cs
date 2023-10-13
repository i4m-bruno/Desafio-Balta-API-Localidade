using DesafioLocalidade.Data.Context;
using DesafioLocalidade.Domain.Interfaces.IBGE;
using DesafioLocalidade.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DesafioLocalidade.Application.Services.IBGE
{
    public class IBGEQueriesService : IIBGEQueriesService
    {
        private readonly AppDbContext _context;
        public IBGEQueriesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IBGEViewModel>> GetAll(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            var take = pageSize;

            return await _context.IBGE.AsNoTracking()
                                            .Skip(skip)
                                            .Take(take)
                                            .Select(ibge => new IBGEViewModel(ibge.Id, ibge.City, ibge.State))
                                            .ToListAsync();
        }

        public async Task<IBGEViewModel> GetByIBGE(string id)
        {
            var ibge = await _context.IBGE
                .Where(i => i.Id == id)
                .Select(ibge => new IBGEViewModel(ibge.Id, ibge.City, ibge.State))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (ibge != null)
                return ibge;

            return null;
        }

        public async Task<IBGEViewModel> GetByCity(string city)
        {
            var ibge = await _context.IBGE
                .Where(i => i.City.ToUpper().Equals(city.ToUpper()))
                .Select(ibge => new IBGEViewModel(ibge.Id, ibge.City, ibge.State))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (ibge != null)
                return ibge;

            return null;
        }

        public async Task<IEnumerable<IBGEViewModel>> GetByUF(string uf)
        {
            var ibge = await _context.IBGE
                .Where(i => i.State.ToUpper().Equals(uf.ToUpper()))
                .Select(ibge => new IBGEViewModel(ibge.Id, ibge.City, ibge.State))
                .AsNoTracking()
                .ToListAsync();

            if (ibge != null)
                return ibge;

            return null;
        }
    }
}
