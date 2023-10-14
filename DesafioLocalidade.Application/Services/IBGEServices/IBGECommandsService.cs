using DesafioLocalidade.Data.Context;
using DesafioLocalidade.Domain.Interfaces.IBGEServices;
using DesafioLocalidade.Domain.Models;
using DesafioLocalidade.Domain.ViewModels;

namespace DesafioLocalidade.Application.Services.IBGEServices
{
    public class IBGECommandsService : IIBGECommandsService
    {
        private readonly AppDbContext _context;
        public IBGECommandsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IBGEViewModel> Create(IBGEViewModel vm)
        {
            try
            {
                IBGE ibge = new (vm.Id, vm.State, vm.City);
                await _context.IBGE.AddAsync(ibge);
                int changedRows = await _context.SaveChangesAsync();

                if (changedRows > 0) return vm;

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IBGEViewModel>> ExcelUpdload()
        {
            throw new NotImplementedException();
        }

        public Task<IBGEViewModel> Update()
        {
            throw new NotImplementedException();
        }
    }
}
