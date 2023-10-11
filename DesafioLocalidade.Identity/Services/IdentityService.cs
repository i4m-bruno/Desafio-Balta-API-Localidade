using Desafio_Balta_API_Localidade.ViewModels;
using DesafioLocalidade.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace DesafioLocalidade.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<UsuarioResponseViewModel> CadastrarUsuario(UsuarioViewModel request)
        {
            IdentityUser user = new()
            {
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Senha);
            if (result.Succeeded)
                await _userManager.SetLockoutEnabledAsync(user, false);

            UsuarioResponseViewModel response = new(result.Succeeded);

            if (!result.Succeeded && result.Errors.Count() > 0)
                response.AdicionarErros(result.Errors.Select(error => error.Description));

            return response;
        }

        public Task<object> Login(object request)
        {
            throw new NotImplementedException();
        }
    }
}
