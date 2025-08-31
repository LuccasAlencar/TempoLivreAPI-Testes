using Microsoft.EntityFrameworkCore;
using TempoLivreAPI.Data;
using TempoLivreAPI.DTOs;
using TempoLivreAPI.Models;
using TempoLivreAPI.Helpers;

namespace TempoLivreAPI.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/usuarios", async (AppDbContext context) =>
            {
                var usuarios = await context.Usuarios
                    .Select(u => new UsuarioReadDto
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Email = u.Email,
                        DataCadastro = u.DataCadastro
                    })
                    .ToListAsync();
                return Results.Ok(usuarios);
            })
            .WithName("GetUsuarios")
            .WithTags(Constants.Usuarios)
            .Produces<List<UsuarioReadDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/usuarios/{id}", async (int id, AppDbContext context) =>
            {
                var u = await context.Usuarios.FindAsync(id);
                return u == null
                    ? Results.NotFound()
                    : Results.Ok(new UsuarioReadDto
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Email = u.Email,
                        DataCadastro = u.DataCadastro
                    });
            })
            .WithName("GetUsuarioById")
            .WithTags(Constants.Usuarios)
            .Produces<UsuarioReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/usuarios", async (UsuarioCreateDto dto, AppDbContext context) =>
            {
                var u = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    DataCadastro = DateTime.UtcNow
                };
                context.Usuarios.Add(u);
                await context.SaveChangesAsync();

                var readDto = new UsuarioReadDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    DataCadastro = u.DataCadastro
                };
                return Results.Created($"/usuarios/{u.Id}", readDto);
            })
            .WithName("CreateUsuario")
            .WithTags(Constants.Usuarios)
            .Accepts<UsuarioCreateDto>(Constants.ApplicationJson)
            .Produces<UsuarioReadDto>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/usuarios/{id}", async (int id, UsuarioUpdateDto dto, AppDbContext context) =>
            {
                var u = await context.Usuarios.FindAsync(id);
                if (u == null) return Results.NotFound();

                u.Nome = dto.Nome;
                u.Email = dto.Email;
                if (!string.IsNullOrEmpty(dto.Senha))
                    u.Senha = dto.Senha;

                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("UpdateUsuario")
            .WithTags(Constants.Usuarios)
            .Accepts<UsuarioUpdateDto>(Constants.ApplicationJson)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/usuarios/{id}", async (int id, AppDbContext context) =>
            {
                var u = await context.Usuarios.FindAsync(id);
                if (u == null) return Results.NotFound();

                context.Usuarios.Remove(u);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteUsuario")
            .WithTags(Constants.Usuarios)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
