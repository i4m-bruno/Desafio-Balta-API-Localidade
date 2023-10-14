using DesafioLocalidade.Domain.ViewModels;

namespace DesafioLocalidade.Domain.Interfaces.IBGEServices
{
    public interface IIBGECommandsService
    {
        Task<IBGEViewModel> Create(IBGEViewModel vm);
        Task<IBGEViewModel> Update(IBGEViewModel vm);
        Task<bool> Delete(IBGEViewModel vm);
        Task<IEnumerable<IBGEViewModel>> ExcelUpdload();
    }
}
