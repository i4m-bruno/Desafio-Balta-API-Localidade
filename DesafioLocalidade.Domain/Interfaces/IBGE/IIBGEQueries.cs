using DesafioLocalidade.Domain.ViewModels;

namespace DesafioLocalidade.Domain.Interfaces.IBGE
{
    public interface IIBGEQueriesService
    {
        Task<IEnumerable<IBGEViewModel>> GetAll(int page, int pageSize);
        Task<IBGEViewModel>? GetByIBGE();
        Task<IBGEViewModel>? GetByCity(string city);
        Task<IEnumerable<IBGEViewModel>> GetByUF();
    }
}
