using Desafio_Balta_API_Localidade.ViewModels;
using DesafioLocalidade.Application.Interfaces.Services;
using DesafioLocalidade.Identity.ViewModels;

public static class IdentityEndpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost("/v1/api/usuario/login", async (IIdentityService _identityService, UsuarioLoginViewModel vm) =>
        {
            vm.Validate();

            if(vm.IsValid)
            {
                var result = await _identityService.Login(vm);

                if (result.Sucesso)
                    return Results.Ok(result);
                else if (result.Erros.Count > 0)
                    return Results.BadRequest(result);
            }

            return Results.BadRequest(vm.Notifications);
        });

        app.MapPost("/v1/api/usuario", async (IIdentityService _identityService, UsuarioViewModel vm) =>
        {
            vm.Validate();

            if (vm.IsValid)
            {
                var result = await _identityService.CadastrarUsuario(vm);

                if(result.Sucesso)
                    return Results.Ok(result);
                else if (result.Erros.Count > 0)
                    return Results.BadRequest(result);
            }

            return Results.BadRequest(vm.Notifications);
        });
    }
}
