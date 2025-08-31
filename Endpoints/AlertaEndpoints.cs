using Microsoft.EntityFrameworkCore;
using TempoLivreAPI.Data;
using TempoLivreAPI.DTOs;
using TempoLivreAPI.Models;
using TempoLivreAPI.Helpers;

namespace TempoLivreAPI.Endpoints
{
    public static class AlertaEndpoints
    {
        public static void MapAlertaEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/alertas", async (AppDbContext context) =>
            {
                var items = await context.Alertas
                    .Select(a => new AlertaReadDto
                    {
                        Id = a.Id,
                        Tipo = a.Tipo,
                        Descricao = a.Descricao,
                        NivelSeveridade = a.NivelSeveridade,
                        DataHoraCriacao = a.DataHoraCriacao
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetAlertas")
            .WithTags(Constants.Alertas)
            .Produces<List<AlertaReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/alertas/{id}", async (int id, AppDbContext context) =>
            {
                var a = await context.Alertas.FindAsync(id);
                return a == null
                    ? Results.NotFound()
                    : Results.Ok(new AlertaReadDto
                    {
                        Id = a.Id,
                        Tipo = a.Tipo,
                        Descricao = a.Descricao,
                        NivelSeveridade = a.NivelSeveridade,
                        DataHoraCriacao = a.DataHoraCriacao
                    });
            })
            .WithName("GetAlertaById")
            .WithTags(Constants.Alertas)
            .Produces<AlertaReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/alertas", async (AlertaCreateDto dto, AppDbContext context) =>
            {
                var a = new Alerta
                {
                    Tipo = dto.Tipo,
                    Descricao = dto.Descricao,
                    NivelSeveridade = dto.NivelSeveridade,
                    DataHoraCriacao = DateTime.UtcNow
                };
                context.Alertas.Add(a);
                await context.SaveChangesAsync();

                var readDto = new AlertaReadDto
                {
                    Id = a.Id,
                    Tipo = a.Tipo,
                    Descricao = a.Descricao,
                    NivelSeveridade = a.NivelSeveridade,
                    DataHoraCriacao = a.DataHoraCriacao
                };
                return Results.Created($"/alertas/{a.Id}", readDto);
            })
            .WithName("CreateAlerta")
            .WithTags(Constants.Alertas)
            .Accepts<AlertaCreateDto>(Constants.ApplicationJson)
            .Produces<AlertaReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/alertas/{id}", async (int id, AlertaUpdateDto dto, AppDbContext context) =>
            {
                var a = await context.Alertas.FindAsync(id);
                if (a == null) return Results.NotFound();

                a.Tipo = dto.Tipo;
                a.Descricao = dto.Descricao;
                a.NivelSeveridade = dto.NivelSeveridade;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateAlerta")
            .WithTags(Constants.Alertas)
            .Accepts<AlertaUpdateDto>(Constants.ApplicationJson)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/alertas/{id}", async (int id, AppDbContext context) =>
            {
                var a = await context.Alertas.FindAsync(id);
                if (a == null) return Results.NotFound();

                context.Alertas.Remove(a);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteAlerta")
            .WithTags(Constants.Alertas)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
