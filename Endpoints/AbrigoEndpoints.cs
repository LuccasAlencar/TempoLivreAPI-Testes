using Microsoft.EntityFrameworkCore;
using TempoLivreAPI.Data;
using TempoLivreAPI.DTOs;
using TempoLivreAPI.Models;
using TempoLivreAPI.Helpers;

namespace TempoLivreAPI.Endpoints
{
    public static class AbrigoEndpoints
    {
        public static void MapAbrigoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/abrigos", async (AppDbContext context) =>
            {
                var items = await context.Abrigos
                    .Select(a => new AbrigoReadDto
                    {
                        Id = a.Id,
                        Nome = a.Nome,
                        Endereco = a.Endereco,
                        Latitude = a.Latitude,
                        Longitude = a.Longitude,
                        CapacidadeMax = a.CapacidadeMax,
                        DisponibilidadeAtual = a.DisponibilidadeAtual,
                        Contato = a.Contato
                    })
                    .ToListAsync();
                return Results.Ok(items);
            })
            .WithName("GetAbrigos")
            .WithTags(Constants.Abrigos)
            .Produces<List<AbrigoReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/abrigos/{id}", async (int id, AppDbContext context) =>
            {
                var a = await context.Abrigos.FindAsync(id);
                return a == null
                    ? Results.NotFound()
                    : Results.Ok(new AbrigoReadDto
                    {
                        Id = a.Id,
                        Nome = a.Nome,
                        Endereco = a.Endereco,
                        Latitude = a.Latitude,
                        Longitude = a.Longitude,
                        CapacidadeMax = a.CapacidadeMax,
                        DisponibilidadeAtual = a.DisponibilidadeAtual,
                        Contato = a.Contato
                    });
            })
            .WithName("GetAbrigoById")
            .WithTags(Constants.Abrigos)
            .Produces<AbrigoReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/abrigos", async (AbrigoCreateDto dto, AppDbContext context) =>
            {
                var a = new Abrigo
                {
                    Nome = dto.Nome,
                    Endereco = dto.Endereco,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    CapacidadeMax = dto.CapacidadeMax,
                    DisponibilidadeAtual = dto.DisponibilidadeAtual,
                    Contato = dto.Contato
                };
                context.Abrigos.Add(a);
                await context.SaveChangesAsync();

                var readDto = new AbrigoReadDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Endereco = a.Endereco,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    CapacidadeMax = a.CapacidadeMax,
                    DisponibilidadeAtual = a.DisponibilidadeAtual,
                    Contato = a.Contato
                };
                return Results.Created($"/abrigos/{a.Id}", readDto);
            })
            .WithName("CreateAbrigo")
            .WithTags(Constants.Abrigos)
            .Accepts<AbrigoCreateDto>(Constants.ApplicationJson)
            .Produces<AbrigoReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/abrigos/{id}", async (int id, AbrigoUpdateDto dto, AppDbContext context) =>
            {
                var a = await context.Abrigos.FindAsync(id);
                if (a == null) return Results.NotFound();

                a.Nome = dto.Nome;
                a.Endereco = dto.Endereco;
                a.Latitude = dto.Latitude;
                a.Longitude = dto.Longitude;
                a.CapacidadeMax = dto.CapacidadeMax;
                a.DisponibilidadeAtual = dto.DisponibilidadeAtual;
                a.Contato = dto.Contato;
                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateAbrigo")
            .WithTags(Constants.Abrigos)
            .Accepts<AbrigoUpdateDto>(Constants.ApplicationJson)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/abrigos/{id}", async (int id, AppDbContext context) =>
            {
                var a = await context.Abrigos.FindAsync(id);
                if (a == null) return Results.NotFound();

                context.Abrigos.Remove(a);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteAbrigo")
            .WithTags(Constants.Abrigos)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}