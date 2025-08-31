using Microsoft.EntityFrameworkCore;
using TempoLivreAPI.Data;
using TempoLivreAPI.DTOs;
using TempoLivreAPI.Models;
using TempoLivreAPI.Helpers;

namespace TempoLivreAPI.Endpoints
{
    public static class LocalizacaoEndpoints
    {
        public static void MapLocalizacaoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/localizacaousuarios", async (AppDbContext context) =>
            {
                var items = await context.Localizacoes
                    .Select(l => new LocalizacaoUsuarioReadDto
                    {
                        Id = l.Id,
                        UsuarioId = l.UsuarioId,
                        Latitude = l.Latitude,
                        Longitude = l.Longitude,
                        DataHoraRegistro = l.DataHoraRegistro
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetLocalizacoes")
            .WithTags(Constants.Localizacoes)
            .Produces<List<LocalizacaoUsuarioReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/localizacaousuarios/{id}", async (int id, AppDbContext context) =>
            {
                var l = await context.Localizacoes.FindAsync(id);
                return l == null
                    ? Results.NotFound()
                    : Results.Ok(new LocalizacaoUsuarioReadDto
                    {
                        Id = l.Id,
                        UsuarioId = l.UsuarioId,
                        Latitude = l.Latitude,
                        Longitude = l.Longitude,
                        DataHoraRegistro = l.DataHoraRegistro
                    });
            })
            .WithName("GetLocalizacaoById")
            .WithTags(Constants.Localizacoes)
            .Produces<LocalizacaoUsuarioReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/localizacaousuarios", async (LocalizacaoUsuarioCreateDto dto, AppDbContext context) =>
            {
                var l = new LocalizacaoUsuario
                {
                    UsuarioId = dto.UsuarioId,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    DataHoraRegistro = DateTime.UtcNow
                };
                context.Localizacoes.Add(l);
                await context.SaveChangesAsync();

                var readDto = new LocalizacaoUsuarioReadDto
                {
                    Id = l.Id,
                    UsuarioId = l.UsuarioId,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    DataHoraRegistro = l.DataHoraRegistro
                };
                return Results.Created($"/localizacaousuarios/{l.Id}", readDto);
            })
            .WithName("CreateLocalizacao")
            .WithTags(Constants.Localizacoes)
            .Accepts<LocalizacaoUsuarioCreateDto>(Constants.ApplicationJson)
            .Produces<LocalizacaoUsuarioReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/localizacaousuarios/{id}", async (int id, LocalizacaoUsuarioUpdateDto dto, AppDbContext context) =>
            {
                var l = await context.Localizacoes.FindAsync(id);
                if (l == null) return Results.NotFound();

                l.Latitude = dto.Latitude;
                l.Longitude = dto.Longitude;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateLocalizacao")
            .WithTags(Constants.Localizacoes)
            .Accepts<LocalizacaoUsuarioUpdateDto>(Constants.ApplicationJson)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/localizacaousuarios/{id}", async (int id, AppDbContext context) =>
            {
                var l = await context.Localizacoes.FindAsync(id);
                if (l == null) return Results.NotFound();

                context.Localizacoes.Remove(l);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteLocalizacao")
            .WithTags(Constants.Localizacoes)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
