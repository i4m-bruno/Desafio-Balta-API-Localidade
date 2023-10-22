using DesafioLocalidade.Domain.Interfaces.IBGEServices;
using DesafioLocalidade.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public static class IBGEEndpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapGet("/v1/api/localidade", async (int page, int pageSize, IIBGEQueriesService _service) => {

            if (page <= 0 || pageSize <= 0)
            {
                return Results.BadRequest("Invalid pagination parameters.");
            }
            var result = await _service.GetAll(page, pageSize);

            return Results.Ok(result);
                
        });   
        
        app.MapGet("/v1/api/localidade/{id}", [Authorize] async ([FromQuery]string id, IIBGEQueriesService _service) => {
            
            if (string.IsNullOrEmpty(id))
                return Results.BadRequest("Id can't be null or empty!");

            var ibge = await _service.GetByIBGE(id);

            if (ibge == null)
                return Results.NotFound("No locations found!");

            return Results.Ok(ibge);
        });

        app.MapGet("/v1/api/localidade/cidade/{cidade}", [Authorize] async (string city, IIBGEQueriesService _service) => {

            if (string.IsNullOrEmpty(city))
                return Results.BadRequest("Id can't be null or empty!");

            var ibge = await _service.GetByCity(city);

            if (ibge == null)
                return Results.NotFound("No locations found!");

            return Results.Ok(ibge);
        });

        app.MapGet("/v1/api/localidade/estado/{estado}", [Authorize] async (string uf, IIBGEQueriesService _service) => {

            if (string.IsNullOrEmpty(uf))
                return Results.BadRequest("Id can't be null or empty!");

            var ibge = await _service.GetByUF(uf);

            if (ibge == null)
                return Results.NotFound("No locations found!");

            return Results.Ok(ibge);
        });

        app.MapPost("/v1/api/localidade", [Authorize] async (IBGEViewModel vm, IIBGECommandsService _service) =>
        {
            vm.Validate();
            if (vm.IsValid)
            {
                var result = await _service.Create(vm);
                if (result == null)
                    return Results.BadRequest();

                return Results.Ok(result);
            }
            return Results.BadRequest(vm.Notifications);
        });
        
        app.MapPost("/v1/api/localidade/excel", [Authorize] async ([FromForm]IFormFile file, IIBGECommandsService _service) =>
        {
            if (file == null || file.Length == 0)
                return Results.BadRequest("Arquivo inválido");

            var result = await _service.ExcelUpdload(file);
            if (result == null)
                return Results.BadRequest();

            return Results.Ok(result);
        });

        app.MapPut("/v1/api/localidade", [Authorize(Roles = "Admin")] async (IBGEViewModel vm, IIBGECommandsService _service) =>
        {
            vm.Validate();
            if (vm.IsValid)
            {
                var result = await _service.Update(vm);
                if (result == null)
                    return Results.BadRequest();

                return Results.Ok(result);
            }
            return Results.BadRequest(vm.Notifications);
        });

        app.MapDelete("/v1/api/localidade/delete/{id}", [Authorize(Roles = "Admin")] async (string id, IIBGECommandsService _service) =>
        {
            var result = await _service.Delete(id);
            if (result)
                return Results.Ok(result);

            return Results.BadRequest();
        });

    }
}
