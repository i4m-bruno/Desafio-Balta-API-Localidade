using Desafio_Balta_API_Localidade.ViewModels;
using DesafioLocalidade.Application.Interfaces.Services;

namespace DesafioLocalidade.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<UsuarioViewModel> CadastrarUsuario(object request)
        {
            throw new NotImplementedException();
        }

        public Task<object> Login(object request)
        {
            throw new NotImplementedException();
        }
    }
}
