using Desafio_Balta_API_Localidade.ViewModels;
using DesafioLocalidade.Identity.ViewModels;

namespace DesafioLocalidade.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<UsuarioResponseViewModel> CadastrarUsuario(UsuarioViewModel request);
        Task<UsuarioLoginResponseViewModel> Login(UsuarioLoginViewModel request);
    }
}
