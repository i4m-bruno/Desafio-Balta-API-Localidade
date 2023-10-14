using DesafioLocalidade.Domain.ViewModels;

namespace DesafioLocalidade.Domain.Interfaces.IBGEServices
{
    public interface IIBGECommandsService
    {
        Task<IBGEViewModel> Create(IBGEViewModel vm);
        Task<IBGEViewModel> Update();
        Task<bool> Delete();
        Task<IEnumerable<IBGEViewModel>> ExcelUpdload();
    }
}
