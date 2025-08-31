using Microsoft.EntityFrameworkCore;
using TempoLivreAPI.Data;
using TempoLivreAPI.DTOs;
using TempoLivreAPI.Models;
using TempoLivreAPI.Helpers;

namespace TempoLivreAPI.Endpoints
{
    public static class RotaSeguraEndpoints
    {
        public static void MapRotaSeguraEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/rotasseguras", async (AppDbContext context) =>
            {
                var items = await context.RotasSeguras
                    .Select(r => new RotaSeguraReadDto
                    {
                        Id = r.Id,
                        Nome = r.Nome,
                        Coordenadas = r.Coordenadas
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetRotasSeguras")
            .WithTags(Constants.RotasSeguras)
            .Produces<List<RotaSeguraReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/rotasseguras/{id}", async (int id, AppDbContext context) =>
            {
                var r = await context.RotasSeguras.FindAsync(id);
                return r == null
                    ? Results.NotFound()
                    : Results.Ok(new RotaSeguraReadDto
                    {
                        Id = r.Id,
                        Nome = r.Nome,
                        Coordenadas = r.Coordenadas
                    });
            })
            .WithName("GetRotaSeguraById")
            .WithTags(Constants.RotasSeguras)
            .Produces<RotaSeguraReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/rotasseguras", async (RotaSeguraCreateDto dto, AppDbContext context) =>
            {
                var r = new RotaSegura
                {
                    Nome = dto.Nome,
                    Coordenadas = dto.Coordenadas
                };
                context.RotasSeguras.Add(r);
                await context.SaveChangesAsync();

                var readDto = new RotaSeguraReadDto
                {
                    Id = r.Id,
                    Nome = r.Nome,
                    Coordenadas = r.Coordenadas
                };
                return Results.Created($"/rotasseguras/{r.Id}", readDto);
            })
            .WithName("CreateRotaSegura")
            .WithTags(Constants.RotasSeguras)
            .Accepts<RotaSeguraCreateDto>(Constants.ApplicationJson)
            .Produces<RotaSeguraReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/rotasseguras/{id}", async (int id, RotaSeguraUpdateDto dto, AppDbContext context) =>
            {
                var r = await context.RotasSeguras.FindAsync(id);
                if (r == null) return Results.NotFound();

                r.Nome = dto.Nome;
                r.Coordenadas = dto.Coordenadas;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateRotaSegura")
            .WithTags(Constants.RotasSeguras)
            .Accepts<RotaSeguraUpdateDto>(Constants.ApplicationJson)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/rotasseguras/{id}", async (int id, AppDbContext context) =>
            {
                var r = await context.RotasSeguras.FindAsync(id);
                if (r == null) return Results.NotFound();

                context.RotasSeguras.Remove(r);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteRotaSegura")
            .WithTags(Constants.RotasSeguras)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
