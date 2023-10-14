using Desafio_Balta_API_Localidade.ViewModels;
using DesafioLocalidade.Application.Interfaces.Services;
using DesafioLocalidade.Identity.Configuration;
using DesafioLocalidade.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DesafioLocalidade.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
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

        public async Task<UsuarioLoginResponseViewModel> Login(UsuarioLoginViewModel request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);

            if (result.Succeeded)
                return await GerarToken(request.Email);

            UsuarioLoginResponseViewModel response = new(result.Succeeded);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    response.AdicionarErro("Conta bloqueada");
                else if (result.IsNotAllowed)
                    response.AdicionarErro("Conta não autorizada");
                else
                    response.AdicionarErro("Erro ao fazer login. Revise os campos");
            };

            return response;
        }

        private async Task<UsuarioLoginResponseViewModel> GerarToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new NullReferenceException(nameof(user));

            var tokenClaims = await ObterClaims(user);
            var dataExpiracao = DateTime.Now.AddHours(_jwtOptions.Expiration);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new UsuarioLoginResponseViewModel(
                sucesso: true,
                token: token,
                dataExpiracao: dataExpiracao);
        }

        private async Task<IList<Claim>> ObterClaims(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }
    }
}
