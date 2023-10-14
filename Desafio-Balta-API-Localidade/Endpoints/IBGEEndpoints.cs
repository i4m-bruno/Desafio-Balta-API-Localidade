using DesafioLocalidade.Domain.Interfaces.IBGE;
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
        
        app.MapGet("/v1/api/localidade/{id}", async ([FromQuery]string id, IIBGEQueriesService _service) => { 
            
            if (string.IsNullOrEmpty(id))
                return Results.BadRequest("Id can't be null or empty!");

            var ibge = await  _service.GetByIBGE(id);

            if (ibge == null)
                return Results.NotFound("No locations found!");

            return Results.Ok(ibge);
        });

        app.MapGet("/v1/api/localidade/cidade/{cidade}", async (string city, IIBGEQueriesService _service) => {

            if (string.IsNullOrEmpty(city))
                return Results.BadRequest("Id can't be null or empty!");

            var ibge = await _service.GetByCity(city);

            if (ibge == null)
                return Results.NotFound("No locations found!");

            return Results.Ok(ibge);
        });

        app.MapGet("/v1/api/localidade/estado/{estado}", async (string uf, IIBGEQueriesService _service) => {

            if (string.IsNullOrEmpty(uf))
                return Results.BadRequest("Id can't be null or empty!");

            var ibge = await _service.GetByUF(uf);

            if (ibge == null)
                return Results.NotFound("No locations found!");

            return Results.Ok(ibge);
        });
        

        app.MapPost("/v1/api/localidade", () => "Criar localidade");
        app.MapPut("/v1/api/localidade", () => "Editar localidade");
        app.MapDelete("/v1/api/localidade", () => "Deletar localidade");

    }
}
