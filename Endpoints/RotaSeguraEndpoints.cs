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
                    .Select(r => new RotasSegurasReadDto
                    {
                        Id = r.Id,
                        UsuarioId = r.UsuarioId,
                        AbrigoDestinoId = r.AbrigoDestinoId,
                        OrigemLatitude = r.OrigemLatitude,
                        OrigemLongitude = r.OrigemLongitude,
                        DestinoLatitude = r.DestinoLatitude,
                        DestinoLongitude = r.DestinoLongitude,
                        TipoRota = r.TipoRota
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetRotasSeguras")
            .WithTags(Constants.RotasSeguras)
            .Produces<List<RotasSegurasReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/rotasseguras/{id}", async (int id, AppDbContext context) =>
            {
                var r = await context.RotasSeguras.FindAsync(id);
                return r == null
                    ? Results.NotFound()
                    : Results.Ok(new RotasSegurasReadDto
                    {
                        Id = r.Id,
                        UsuarioId = r.UsuarioId,
                        AbrigoDestinoId = r.AbrigoDestinoId,
                        OrigemLatitude = r.OrigemLatitude,
                        OrigemLongitude = r.OrigemLongitude,
                        DestinoLatitude = r.DestinoLatitude,
                        DestinoLongitude = r.DestinoLongitude,
                        TipoRota = r.TipoRota
                    });
            })
            .WithName("GetRotaSeguraById")
            .WithTags(Constants.RotasSeguras)
            .Produces<RotasSegurasReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/rotasseguras", async (RotasSegurasCreateDto dto, AppDbContext context) =>
            {
                var r = new RotasSeguras
                {
                    UsuarioId = dto.UsuarioId,
                    AbrigoDestinoId = dto.AbrigoDestinoId,
                    OrigemLatitude = dto.OrigemLatitude,
                    OrigemLongitude = dto.OrigemLongitude,
                    DestinoLatitude = dto.DestinoLatitude,
                    DestinoLongitude = dto.DestinoLongitude,
                    TipoRota = dto.TipoRota
                };
                context.RotasSeguras.Add(r);
                await context.SaveChangesAsync();

                var readDto = new RotasSegurasReadDto
                {
                    Id = r.Id,
                    UsuarioId = r.UsuarioId,
                    AbrigoDestinoId = r.AbrigoDestinoId,
                    OrigemLatitude = r.OrigemLatitude,
                    OrigemLongitude = r.OrigemLongitude,
                    DestinoLatitude = r.DestinoLatitude,
                    DestinoLongitude = r.DestinoLongitude,
                    TipoRota = r.TipoRota
                };
                return Results.Created($"/rotasseguras/{r.Id}", readDto);
            })
            .WithName("CreateRotaSegura")
            .WithTags(Constants.RotasSeguras)
            .Accepts<RotasSegurasCreateDto>(Constants.ApplicationJson)
            .Produces<RotasSegurasReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/rotasseguras/{id}", async (int id, RotasSegurasUpdateDto dto, AppDbContext context) =>
            {
                var r = await context.RotasSeguras.FindAsync(id);
                if (r == null) return Results.NotFound();

                r.OrigemLatitude = dto.OrigemLatitude;
                r.OrigemLongitude = dto.OrigemLongitude;
                r.DestinoLatitude = dto.DestinoLatitude;
                r.DestinoLongitude = dto.DestinoLongitude;
                r.TipoRota = dto.TipoRota;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateRotaSegura")
            .WithTags(Constants.RotasSeguras)
            .Accepts<RotasSegurasUpdateDto>(Constants.ApplicationJson)
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