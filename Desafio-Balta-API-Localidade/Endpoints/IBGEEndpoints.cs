using DesafioLocalidade.Domain.Interfaces.IBGE;

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
        
        app.MapGet("/v1/api/localidade/{id}", () => "Filtro por id");
        app.MapGet("/v1/api/localidade/{estado}", () => "Filtro por estado");
        app.MapGet("/v1/api/localidade/{cidade}", () => "Filtro por cidade");

        app.MapPost("/v1/api/localidade", () => "Criar localidade");
        app.MapPut("/v1/api/localidade", () => "Editar localidade");
        app.MapDelete("/v1/api/localidade", () => "Deletar localidade");

    }
}
