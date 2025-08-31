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
                    .Select(o => new OcorrenciaColaborativaReadDto
                    {
                        Id = o.Id,
                        UsuarioId = o.UsuarioId,
                        TipoOcorrencia = o.TipoOcorrencia,
                        Descricao = o.Descricao,
                        Latitude = o.Latitude,
                        Longitude = o.Longitude,
                        DataEnvio = o.DataEnvio,
                        Status = o.Status
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetOcorrencias")
            .WithTags(Constants.Ocorrencias)
            .Produces<List<OcorrenciaColaborativaReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/ocorrencias/{id}", async (int id, AppDbContext context) =>
            {
                var o = await context.Ocorrencias.FindAsync(id);
                return o == null
                    ? Results.NotFound()
                    : Results.Ok(new OcorrenciaColaborativaReadDto
                    {
                        Id = o.Id,
                        UsuarioId = o.UsuarioId,
                        TipoOcorrencia = o.TipoOcorrencia,
                        Descricao = o.Descricao,
                        Latitude = o.Latitude,
                        Longitude = o.Longitude,
                        DataEnvio = o.DataEnvio,
                        Status = o.Status
                    });
            })
            .WithName("GetOcorrenciaById")
            .WithTags(Constants.Ocorrencias)
            .Produces<OcorrenciaColaborativaReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/ocorrencias", async (OcorrenciaColaborativaCreateDto dto, AppDbContext context) =>
            {
                var o = new OcorrenciaColaborativa
                {
                    UsuarioId = dto.UsuarioId,
                    TipoOcorrencia = dto.TipoOcorrencia,
                    Descricao = dto.Descricao,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    DataEnvio = DateTime.UtcNow,
                    Status = dto.Status ?? "pendente"
                };
                context.Ocorrencias.Add(o);
                await context.SaveChangesAsync();

                var readDto = new OcorrenciaColaborativaReadDto
                {
                    Id = o.Id,
                    UsuarioId = o.UsuarioId,
                    TipoOcorrencia = o.TipoOcorrencia,
                    Descricao = o.Descricao,
                    Latitude = o.Latitude,
                    Longitude = o.Longitude,
                    DataEnvio = o.DataEnvio,
                    Status = o.Status
                };
                return Results.Created($"/ocorrencias/{o.Id}", readDto);
            })
            .WithName("CreateOcorrencia")
            .WithTags(Constants.Ocorrencias)
            .Accepts<OcorrenciaColaborativaCreateDto>(Constants.ApplicationJson)
            .Produces<OcorrenciaColaborativaReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/ocorrencias/{id}", async (int id, OcorrenciaColaborativaUpdateDto dto, AppDbContext context) =>
            {
                var o = await context.Ocorrencias.FindAsync(id);
                if (o == null) return Results.NotFound();

                o.TipoOcorrencia = dto.TipoOcorrencia;
                o.Descricao = dto.Descricao;
                o.Latitude = dto.Latitude;
                o.Longitude = dto.Longitude;
                o.Status = dto.Status;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateOcorrencia")
            .WithTags(Constants.Ocorrencias)
            .Accepts<OcorrenciaColaborativaUpdateDto>(Constants.ApplicationJson)
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