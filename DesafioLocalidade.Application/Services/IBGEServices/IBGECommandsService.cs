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

        public async Task<bool> Delete(IBGEViewModel vm)
        {
            try
            {
                IBGE ibge = await _context.IBGE.FindAsync(vm.Id);
                if (ibge == null)
                    return false;

                _context.IBGE.Remove(ibge);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<IBGEViewModel>> ExcelUpdload()
        {
            throw new NotImplementedException();
        }

        public async Task<IBGEViewModel> Update(IBGEViewModel vm)
        {
            try
            {
                IBGE ibge = await _context.IBGE.FindAsync(vm.Id);
                if (ibge == null)
                    return null;

                ibge.State = vm.State;
                ibge.City = vm.City;

                await _context.SaveChangesAsync();

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
