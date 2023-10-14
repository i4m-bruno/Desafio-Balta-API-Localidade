using DesafioLocalidade.Data.Context;
using DesafioLocalidade.Domain.Interfaces.IBGE;
using DesafioLocalidade.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

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

            return await _context.IBGE.Skip(skip)
                                            .Take(take)
                                            .Select(ibge => new IBGEViewModel(ibge.Id,ibge.City, ibge.State))
                                            .ToListAsync();
        }

        public async Task<IBGEViewModel>? GetByCity(string city)
        {
            return await _context.IBGE.Where(ibge => ibge.City.ToUpper().Equals(city.ToUpper()))
                                      .Select(ibge => new IBGEViewModel(ibge.Id, ibge.City, ibge.State))
                                      .FirstOrDefaultAsync() ?? null;
        }

        public async Task<IBGEViewModel> GetByIBGE()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IBGEViewModel>> GetByUF()
        {
            throw new NotImplementedException();
        }
    }
}
