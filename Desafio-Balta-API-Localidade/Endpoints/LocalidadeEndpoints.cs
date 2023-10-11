public static class LocalidadeEndpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapGet("/v1/api/localidade", () => "Todas localidades");
        app.MapGet("/v1/api/localidade/{id}", () => "Filtro por id");
        app.MapGet("/v1/api/localidade/{estado}", () => "Filtro por estado");
        app.MapGet("/v1/api/localidade/{cidade}", () => "Filtro por cidade");

        app.MapPost("/v1/api/localidade", () => "Criar localidade");
        app.MapPut("/v1/api/localidade", () => "Editar localidade");
        app.MapDelete("/v1/api/localidade", () => "Deletar localidade");

    }
}
