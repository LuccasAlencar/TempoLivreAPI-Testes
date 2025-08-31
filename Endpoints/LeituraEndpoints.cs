using Microsoft.EntityFrameworkCore;
using TempoLivreAPI.Data;
using TempoLivreAPI.DTOs;
using TempoLivreAPI.Models;
using TempoLivreAPI.Helpers;

namespace TempoLivreAPI.Endpoints
{
    public static class LeituraEndpoints
    {
        public static void MapLeituraEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/leituras", async (AppDbContext context) =>
            {
                var items = await context.Leituras
                    .Select(l => new LeituraSensorReadDto
                    {
                        Id = l.Id,
                        SensorId = l.SensorId,
                        ValorLido = l.ValorLido,
                        DataHora = l.DataHora,
                        UnidadeMedida = l.UnidadeMedida
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetLeituras")
            .WithTags(Constants.Leituras)
            .Produces<List<LeituraSensorReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/leituras/{id}", async (int id, AppDbContext context) =>
            {
                var l = await context.Leituras.FindAsync(id);
                return l == null
                    ? Results.NotFound()
                    : Results.Ok(new LeituraSensorReadDto
                    {
                        Id = l.Id,
                        SensorId = l.SensorId,
                        ValorLido = l.ValorLido,
                        DataHora = l.DataHora,
                        UnidadeMedida = l.UnidadeMedida
                    });
            })
            .WithName("GetLeituraById")
            .WithTags(Constants.Leituras)
            .Produces<LeituraSensorReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/leituras", async (LeituraSensorCreateDto dto, AppDbContext context) =>
            {
                var l = new LeituraSensor
                {
                    SensorId = dto.SensorId,
                    ValorLido = dto.ValorLido,
                    DataHora = DateTime.UtcNow,
                    UnidadeMedida = dto.UnidadeMedida
                };
                context.Leituras.Add(l);
                await context.SaveChangesAsync();

                var readDto = new LeituraSensorReadDto
                {
                    Id = l.Id,
                    SensorId = l.SensorId,
                    ValorLido = l.ValorLido,
                    DataHora = l.DataHora,
                    UnidadeMedida = l.UnidadeMedida
                };
                return Results.Created($"/leituras/{l.Id}", readDto);
            })
            .WithName("CreateLeitura")
            .WithTags(Constants.Leituras)
            .Accepts<LeituraSensorCreateDto>(Constants.ApplicationJson)
            .Produces<LeituraSensorReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/leituras/{id}", async (int id, LeituraSensorUpdateDto dto, AppDbContext context) =>
            {
                var l = await context.Leituras.FindAsync(id);
                if (l == null) return Results.NotFound();

                l.ValorLido = dto.ValorLido;
                l.UnidadeMedida = dto.UnidadeMedida;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateLeitura")
            .WithTags(Constants.Leituras)
            .Accepts<LeituraSensorUpdateDto>(Constants.ApplicationJson)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/leituras/{id}", async (int id, AppDbContext context) =>
            {
                var l = await context.Leituras.FindAsync(id);
                if (l == null) return Results.NotFound();

                context.Leituras.Remove(l);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteLeitura")
            .WithTags(Constants.Leituras)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}