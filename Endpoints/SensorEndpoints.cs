using Microsoft.EntityFrameworkCore;
using TempoLivreAPI.Data;
using TempoLivreAPI.DTOs;
using TempoLivreAPI.Models;
using TempoLivreAPI.Helpers;

namespace TempoLivreAPI.Endpoints
{
    public static class SensorEndpoints
    {
        public static void MapSensorEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/sensors", async (AppDbContext context) =>
            {
                var items = await context.Sensores
                    .Select(s => new SensorReadDto
                    {
                        Id = s.Id,
                        Tipo = s.Tipo,
                        Localizacao = s.Localizacao
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetSensores")
            .WithTags(Constants.Sensors)
            .Produces<List<SensorReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/sensors/{id}", async (int id, AppDbContext context) =>
            {
                var s = await context.Sensores.FindAsync(id);
                return s == null
                    ? Results.NotFound()
                    : Results.Ok(new SensorReadDto
                    {
                        Id = s.Id,
                        Tipo = s.Tipo,
                        Localizacao = s.Localizacao
                    });
            })
            .WithName("GetSensorById")
            .WithTags(Constants.Sensors)
            .Produces<SensorReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/sensors", async (SensorCreateDto dto, AppDbContext context) =>
            {
                var s = new Sensor
                {
                    Tipo = dto.Tipo,
                    Localizacao = dto.Localizacao
                };
                context.Sensores.Add(s);
                await context.SaveChangesAsync();

                var readDto = new SensorReadDto
                {
                    Id = s.Id,
                    Tipo = s.Tipo,
                    Localizacao = s.Localizacao
                };
                return Results.Created($"/sensors/{s.Id}", readDto);
            })
            .WithName("CreateSensor")
            .WithTags(Constants.Sensors)
            .Accepts<SensorCreateDto>(Constants.ApplicationJson)
            .Produces<SensorReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/sensors/{id}", async (int id, SensorUpdateDto dto, AppDbContext context) =>
            {
                var s = await context.Sensores.FindAsync(id);
                if (s == null) return Results.NotFound();

                s.Tipo = dto.Tipo;
                s.Localizacao = dto.Localizacao;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateSensor")
            .WithTags(Constants.Sensors)
            .Accepts<SensorUpdateDto>(Constants.ApplicationJson)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/sensors/{id}", async (int id, AppDbContext context) =>
            {
                var s = await context.Sensores.FindAsync(id);
                if (s == null) return Results.NotFound();

                context.Sensores.Remove(s);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteSensor")
            .WithTags(Constants.Sensors)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
