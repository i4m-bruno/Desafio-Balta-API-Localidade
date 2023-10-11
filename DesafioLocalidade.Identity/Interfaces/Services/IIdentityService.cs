using Desafio_Balta_API_Localidade.ViewModels;

namespace DesafioLocalidade.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<UsuarioViewModel> CadastrarUsuario(Object request);
        Task<Object> Login(Object request);
    }
}
