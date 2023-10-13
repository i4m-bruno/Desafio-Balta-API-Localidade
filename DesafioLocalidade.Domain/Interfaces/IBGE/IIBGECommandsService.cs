using DesafioLocalidade.Domain.ViewModels;

namespace DesafioLocalidade.Domain.Interfaces.IBGE
{
    public interface IIBGECommandsService
    {
        Task<IBGEViewModel> Create();
        Task<IBGEViewModel> Update();
        Task<bool> Delete();
        Task<IEnumerable<IBGEViewModel>> ExcelUpdload();
    }
}
