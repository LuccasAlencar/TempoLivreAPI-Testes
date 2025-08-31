using Microsoft.EntityFrameworkCore;
using TempoLivreAPI.Data;
using TempoLivreAPI.DTOs;
using TempoLivreAPI.Models;
using TempoLivreAPI.Helpers;

namespace TempoLivreAPI.Endpoints
{
    public static class OcorrenciaEndpoints
    {
        public static void MapOcorrenciaEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/ocorrencias", async (AppDbContext context) =>
            {
                var items = await context.Ocorrencias
                    .Select(o => new OcorrenciaReadDto
                    {
                        Id = o.Id,
                        UsuarioId = o.UsuarioId,
                        Descricao = o.Descricao,
                        DataHora = o.DataHora
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetOcorrencias")
            .WithTags(Constants.Ocorrencias)
            .Produces<List<OcorrenciaReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/ocorrencias/{id}", async (int id, AppDbContext context) =>
            {
                var o = await context.Ocorrencias.FindAsync(id);
                return o == null
                    ? Results.NotFound()
                    : Results.Ok(new OcorrenciaReadDto
                    {
                        Id = o.Id,
                        UsuarioId = o.UsuarioId,
                        Descricao = o.Descricao,
                        DataHora = o.DataHora
                    });
            })
            .WithName("GetOcorrenciaById")
            .WithTags(Constants.Ocorrencias)
            .Produces<OcorrenciaReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/ocorrencias", async (OcorrenciaCreateDto dto, AppDbContext context) =>
            {
                var o = new Ocorrencia
                {
                    UsuarioId = dto.UsuarioId,
                    Descricao = dto.Descricao,
                    DataHora = DateTime.UtcNow
                };
                context.Ocorrencias.Add(o);
                await context.SaveChangesAsync();

                var readDto = new OcorrenciaReadDto
                {
                    Id = o.Id,
                    UsuarioId = o.UsuarioId,
                    Descricao = o.Descricao,
                    DataHora = o.DataHora
                };
                return Results.Created($"/ocorrencias/{o.Id}", readDto);
            })
            .WithName("CreateOcorrencia")
            .WithTags(Constants.Ocorrencias)
            .Accepts<OcorrenciaCreateDto>(Constants.ApplicationJson)
            .Produces<OcorrenciaReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/ocorrencias/{id}", async (int id, OcorrenciaUpdateDto dto, AppDbContext context) =>
            {
                var o = await context.Ocorrencias.FindAsync(id);
                if (o == null) return Results.NotFound();

                o.Descricao = dto.Descricao;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateOcorrencia")
            .WithTags(Constants.Ocorrencias)
            .Accepts<OcorrenciaUpdateDto>(Constants.ApplicationJson)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/ocorrencias/{id}", async (int id, AppDbContext context) =>
            {
                var o = await context.Ocorrencias.FindAsync(id);
                if (o == null) return Results.NotFound();

                context.Ocorrencias.Remove(o);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteOcorrencia")
            .WithTags(Constants.Ocorrencias)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
