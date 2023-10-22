using DesafioLocalidade.Data.Context;
using DesafioLocalidade.Domain.Interfaces.IBGEServices;
using DesafioLocalidade.Domain.Models;
using DesafioLocalidade.Domain.ViewModels;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public async Task<bool> Delete(string id)
        {
            try
            {
                IBGE ibge = await _context.IBGE.FirstOrDefaultAsync(ibge => ibge.Id.Equals(id));
                if (ibge == null)
                    return false;

                _context.IBGE.Remove(ibge);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<IBGEViewModel>> ExcelUpdload(IFormFile file)
        {
            const string id = "Codigo_Municipio";
            const string uf = "Codigo_UF";
            const string city = "Nome_Municipio";
            int linhasAlteradas = 0;
            using var stream = file.OpenReadStream();
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            var dataSet = reader.AsDataSet(conf);
            var dataTable = dataSet.Tables[1];

            var ibgeExist = await _context.IBGE.Select(x => x.Id).AsNoTracking().ToListAsync();
            var ibgeList = new List<IBGE>();

            foreach (DataRow row in dataTable.Rows)
            {
                if (row[id] != null &&
                    row[city] != null &&
                    row[uf] != null)
                {
                    if (!ibgeExist.Contains(row[id].ToString()) && !ibgeList.Any(x => x.Id == row[id].ToString()))
                    {
                        var model = new IBGE(row[id].ToString() ?? "",
                                             row[uf].ToString() ?? "",
                                             row[city].ToString() ?? "");
                        ibgeList.Add(model);
                    }
                }
            }

            if (ibgeList.Count > 0)
            {
                _context.IBGE.AddRange(ibgeList);
                linhasAlteradas = await _context.SaveChangesAsync();
            }

            if (linhasAlteradas > 0)
            {
                return ibgeList.Select(l => new IBGEViewModel()
                                {
                                    Id = l.Id,
                                    City = l.City,
                                    State = l.State,
                                })
                                .Take(500)
                                .ToList();
            }

            return new List<IBGEViewModel>();
        }

        public async Task<IBGEViewModel> Update(IBGEViewModel vm)
        {
            try
            {
                IBGE ibge = await _context.IBGE.FirstOrDefaultAsync(ibge => ibge.Id.Equals(vm.Id));
                if (ibge == null)
                    return null;

                ibge.State = vm.State;
                ibge.City = vm.City;

                await _context.SaveChangesAsync();
                return vm;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
