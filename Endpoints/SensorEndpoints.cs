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
                var items = await context.Sensors
                    .Select(s => new SensorReadDto
                    {
                        Id = s.Id,
                        TipoSensor = s.TipoSensor,
                        LocalizacaoLat = s.LocalizacaoLat,
                        LocalizacaoLong = s.LocalizacaoLong,
                        Status = s.Status,
                        DataInstalacao = s.DataInstalacao
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
                var s = await context.Sensors.FindAsync(id);
                return s == null
                    ? Results.NotFound()
                    : Results.Ok(new SensorReadDto
                    {
                        Id = s.Id,
                        TipoSensor = s.TipoSensor,
                        LocalizacaoLat = s.LocalizacaoLat,
                        LocalizacaoLong = s.LocalizacaoLong,
                        Status = s.Status,
                        DataInstalacao = s.DataInstalacao
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
                    TipoSensor = dto.TipoSensor,
                    LocalizacaoLat = dto.LocalizacaoLat,
                    LocalizacaoLong = dto.LocalizacaoLong,
                    Status = dto.Status,
                    DataInstalacao = dto.DataInstalacao
                };
                context.Sensors.Add(s);
                await context.SaveChangesAsync();

                var readDto = new SensorReadDto
                {
                    Id = s.Id,
                    TipoSensor = s.TipoSensor,
                    LocalizacaoLat = s.LocalizacaoLat,
                    LocalizacaoLong = s.LocalizacaoLong,
                    Status = s.Status,
                    DataInstalacao = s.DataInstalacao
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
                var s = await context.Sensors.FindAsync(id);
                if (s == null) return Results.NotFound();

                s.TipoSensor = dto.TipoSensor;
                s.LocalizacaoLat = dto.LocalizacaoLat;
                s.LocalizacaoLong = dto.LocalizacaoLong;
                s.Status = dto.Status;
                s.DataInstalacao = dto.DataInstalacao;
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
                var s = await context.Sensors.FindAsync(id);
                if (s == null) return Results.NotFound();

                context.Sensors.Remove(s);
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