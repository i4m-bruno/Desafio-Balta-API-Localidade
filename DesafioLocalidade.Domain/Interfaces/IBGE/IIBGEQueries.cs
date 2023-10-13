using DesafioLocalidade.Domain.ViewModels;

namespace DesafioLocalidade.Domain.Interfaces.IBGE
{
    public interface IIBGEQueriesService
    {
        Task<IEnumerable<IBGEViewModel>> GetAll(int page, int pageSize);
        Task<IEnumerable<IBGEViewModel>> GetByIBGE();
        Task<IEnumerable<IBGEViewModel>> GetByCity();
        Task<IEnumerable<IBGEViewModel>> GetByUF();
    }
}
