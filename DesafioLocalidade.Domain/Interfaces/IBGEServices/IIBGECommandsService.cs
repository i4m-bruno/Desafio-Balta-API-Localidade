using DesafioLocalidade.Domain.ViewModels;
using Microsoft.AspNetCore.Http;

namespace DesafioLocalidade.Domain.Interfaces.IBGEServices
{
    public interface IIBGECommandsService
    {
        Task<IBGEViewModel> Create(IBGEViewModel vm);
        Task<IBGEViewModel> Update(IBGEViewModel vm);
        Task<bool> Delete(string id);
        Task<IEnumerable<IBGEViewModel>> ExcelUpdload(IFormFile file);
    }
}
