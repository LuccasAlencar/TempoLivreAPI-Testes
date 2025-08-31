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
                    .Select(l => new LeituraReadDto
                    {
                        Id = l.Id,
                        SensorId = l.SensorId,
                        Valor = l.Valor,
                        DataHora = l.DataHora
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetLeituras")
            .WithTags(Constants.Leituras)
            .Produces<List<LeituraReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/leituras/{id}", async (int id, AppDbContext context) =>
            {
                var l = await context.Leituras.FindAsync(id);
                return l == null
                    ? Results.NotFound()
                    : Results.Ok(new LeituraReadDto
                    {
                        Id = l.Id,
                        SensorId = l.SensorId,
                        Valor = l.Valor,
                        DataHora = l.DataHora
                    });
            })
            .WithName("GetLeituraById")
            .WithTags(Constants.Leituras)
            .Produces<LeituraReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/leituras", async (LeituraCreateDto dto, AppDbContext context) =>
            {
                var l = new Leitura
                {
                    SensorId = dto.SensorId,
                    Valor = dto.Valor,
                    DataHora = DateTime.UtcNow
                };
                context.Leituras.Add(l);
                await context.SaveChangesAsync();

                var readDto = new LeituraReadDto
                {
                    Id = l.Id,
                    SensorId = l.SensorId,
                    Valor = l.Valor,
                    DataHora = l.DataHora
                };
                return Results.Created($"/leituras/{l.Id}", readDto);
            })
            .WithName("CreateLeitura")
            .WithTags(Constants.Leituras)
            .Accepts<LeituraCreateDto>(Constants.ApplicationJson)
            .Produces<LeituraReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/leituras/{id}", async (int id, LeituraUpdateDto dto, AppDbContext context) =>
            {
                var l = await context.Leituras.FindAsync(id);
                if (l == null) return Results.NotFound();

                l.Valor = dto.Valor;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateLeitura")
            .WithTags(Constants.Leituras)
            .Accepts<LeituraUpdateDto>(Constants.ApplicationJson)
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
