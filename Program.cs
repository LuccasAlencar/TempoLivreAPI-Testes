// Program.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TempoLivreAPI.Data;
using TempoLivreAPI.DTOs;
using TempoLivreAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure Oracle + EF Core + Migrations + Seeder
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));


// Configure Swagger / OpenAPI with metadata support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tempo Livre API",
        Version = "v1",
        Description = "API RESTful mínima em .NET 8 com Oracle, EF Core, Swagger e migrations",
        Contact = new OpenApiContact
        {
            Name = "Tempo Livre",
        }
    });
});

var app = builder.Build();



// No startup: drop, migrar e semear dados
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
    DbSeeder.Seed(context);
}

// Habilitar Swagger apenas em ambiente de Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinimalApiOracle v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();


// Endpoints CRUD para Todas as Entidades 

// ======== USUÁRIO ========

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
.WithTags("Usuarios")
.WithSummary("Lista todos os usuários")
.WithDescription("Retorna uma lista completa de usuários cadastrados.")
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
.WithTags("Usuarios")
.WithSummary("Obtém usuário por ID")
.WithDescription("Retorna os dados de um usuário específico, dado seu identificador.")
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
.WithTags("Usuarios")
.WithSummary("Cria um novo usuário")
.WithDescription("Insere um novo usuário no banco de dados. Os campos obrigatórios são nome, email e senha.")
.Accepts<UsuarioCreateDto>("application/json")
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
.WithTags("Usuarios")
.WithSummary("Atualiza um usuário existente")
.WithDescription("Atualiza os dados de um usuário (nome, email e, opcionalmente, senha).")
.Accepts<UsuarioUpdateDto>("application/json")
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
.WithTags("Usuarios")
.WithSummary("Remove um usuário")
.WithDescription("Exclui um usuário existente, dado seu ID.")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// ======== LOCALIZAÇÃO USUÁRIO ========

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
.WithTags("Localizacoes")
.WithSummary("Lista todas as localizações de usuário")
.WithDescription("Retorna todas as posições registradas (latitude/longitude) para todos os usuários.")
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
.WithTags("Localizacoes")
.WithSummary("Obtém localização de usuário por ID")
.WithDescription("Retorna os dados de latitude e longitude de uma localização específica.")
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
.WithTags("Localizacoes")
.WithSummary("Cria nova localização de usuário")
.WithDescription("Registra a latitude e longitude para um usuário (campo UsuarioId obrigatorio).")
.Accepts<LocalizacaoUsuarioCreateDto>("application/json")
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
.WithTags("Localizacoes")
.WithSummary("Atualiza localização de usuário")
.WithDescription("Atualiza latitude e longitude de um registro de localização existente.")
.Accepts<LocalizacaoUsuarioUpdateDto>("application/json")
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
.WithTags("Localizacoes")
.WithSummary("Remove localização de usuário")
.WithDescription("Exclui um registro de latitude/longitude baseado no ID.")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// ======== ALERTA ========

app.MapGet("/alertas", async (AppDbContext context) =>
{
    var items = await context.Alertas
        .Select(a => new AlertaReadDto
        {
            Id = a.Id,
            UsuarioId = a.UsuarioId,
            TipoEvento = a.TipoEvento,
            NivelAlerta = a.NivelAlerta,
            Mensagem = a.Mensagem,
            Latitude = a.Latitude,
            Longitude = a.Longitude,
            DataEmissao = a.DataEmissao,
            Status = a.Status
        })
        .ToListAsync();
    return Results.Ok(items);
})
.WithName("GetAlertas")
.WithTags("Alertas")
.WithSummary("Lista todos os alertas")
.WithDescription("Retorna uma lista de todos os alertas registrados no sistema.")
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
            UsuarioId = a.UsuarioId,
            TipoEvento = a.TipoEvento,
            NivelAlerta = a.NivelAlerta,
            Mensagem = a.Mensagem,
            Latitude = a.Latitude,
            Longitude = a.Longitude,
            DataEmissao = a.DataEmissao,
            Status = a.Status
        });
})
.WithName("GetAlertaById")
.WithTags("Alertas")
.WithSummary("Obtém alerta por ID")
.WithDescription("Retorna os detalhes de um alerta específico.")
.Produces<AlertaReadDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapPost("/alertas", async (AlertaCreateDto dto, AppDbContext context) =>
{
    var a = new Alerta
    {
        UsuarioId = dto.UsuarioId,
        TipoEvento = dto.TipoEvento,
        NivelAlerta = dto.NivelAlerta,
        Mensagem = dto.Mensagem,
        Latitude = dto.Latitude,
        Longitude = dto.Longitude,
        DataEmissao = DateTime.UtcNow,
        Status = dto.Status
    };
    context.Alertas.Add(a);
    await context.SaveChangesAsync();

    var readDto = new AlertaReadDto
    {
        Id = a.Id,
        UsuarioId = a.UsuarioId,
        TipoEvento = a.TipoEvento,
        NivelAlerta = a.NivelAlerta,
        Mensagem = a.Mensagem,
        Latitude = a.Latitude,
        Longitude = a.Longitude,
        DataEmissao = a.DataEmissao,
        Status = a.Status
    };
    return Results.Created($"/alertas/{a.Id}", readDto);
})
.WithName("CreateAlerta")
.WithTags("Alertas")
.WithSummary("Cria novo alerta")
.WithDescription("Insere um alerta vinculando-o a um usuário (campo UsuarioId obrigatório).")
.Accepts<AlertaCreateDto>("application/json")
.Produces<AlertaReadDto>(StatusCodes.Status201Created)
.WithOpenApi();

app.MapPut("/alertas/{id}", async (int id, AlertaUpdateDto dto, AppDbContext context) =>
{
    var a = await context.Alertas.FindAsync(id);
    if (a == null) return Results.NotFound();

    a.TipoEvento = dto.TipoEvento;
    a.NivelAlerta = dto.NivelAlerta;
    a.Mensagem = dto.Mensagem;
    a.Latitude = dto.Latitude;
    a.Longitude = dto.Longitude;
    a.Status = dto.Status;

    await context.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("UpdateAlerta")
.WithTags("Alertas")
.WithSummary("Atualiza alerta existente")
.WithDescription("Altera os dados de um alerta já registrado.")
.Accepts<AlertaUpdateDto>("application/json")
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
.WithTags("Alertas")
.WithSummary("Remove alerta")
.WithDescription("Exclui um alerta existente com base no ID.")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// ======== ABRIGO ========

app.MapGet("/abrigos", async (AppDbContext context) =>
{
    var items = await context.Abrigos
        .Select(ab => new AbrigoReadDto
        {
            Id = ab.Id,
            Nome = ab.Nome,
            Endereco = ab.Endereco,
            Latitude = ab.Latitude,
            Longitude = ab.Longitude,
            CapacidadeMax = ab.CapacidadeMax,
            DisponibilidadeAtual = ab.DisponibilidadeAtual,
            Contato = ab.Contato
        })
        .ToListAsync();
    return Results.Ok(items);
})
.WithName("GetAbrigos")
.WithTags("Abrigos")
.WithSummary("Lista todos os abrigos")
.WithDescription("Retorna informações de todos os abrigos cadastrados.")
.Produces<List<AbrigoReadDto>>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapGet("/abrigos/{id}", async (int id, AppDbContext context) =>
{
    var ab = await context.Abrigos.FindAsync(id);
    return ab == null
        ? Results.NotFound()
        : Results.Ok(new AbrigoReadDto
        {
            Id = ab.Id,
            Nome = ab.Nome,
            Endereco = ab.Endereco,
            Latitude = ab.Latitude,
            Longitude = ab.Longitude,
            CapacidadeMax = ab.CapacidadeMax,
            DisponibilidadeAtual = ab.DisponibilidadeAtual,
            Contato = ab.Contato
        });
})
.WithName("GetAbrigoById")
.WithTags("Abrigos")
.WithSummary("Obtém abrigo por ID")
.WithDescription("Retorna dados de um abrigo específico, dado seu ID.")
.Produces<AbrigoReadDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapPost("/abrigos", async (AbrigoCreateDto dto, AppDbContext context) =>
{
    var ab = new Abrigo
    {
        Nome = dto.Nome,
        Endereco = dto.Endereco,
        Latitude = dto.Latitude,
        Longitude = dto.Longitude,
        CapacidadeMax = dto.CapacidadeMax,
        DisponibilidadeAtual = dto.DisponibilidadeAtual,
        Contato = dto.Contato
    };
    context.Abrigos.Add(ab);
    await context.SaveChangesAsync();

    var readDto = new AbrigoReadDto
    {
        Id = ab.Id,
        Nome = ab.Nome,
        Endereco = ab.Endereco,
        Latitude = ab.Latitude,
        Longitude = ab.Longitude,
        CapacidadeMax = ab.CapacidadeMax,
        DisponibilidadeAtual = ab.DisponibilidadeAtual,
        Contato = ab.Contato
    };
    return Results.Created($"/abrigos/{ab.Id}", readDto);
})
.WithName("CreateAbrigo")
.WithTags("Abrigos")
.WithSummary("Cria novo abrigo")
.WithDescription("Insere um novo abrigo no sistema.")
.Accepts<AbrigoCreateDto>("application/json")
.Produces<AbrigoReadDto>(StatusCodes.Status201Created)
.WithOpenApi();

app.MapPut("/abrigos/{id}", async (int id, AbrigoUpdateDto dto, AppDbContext context) =>
{
    var ab = await context.Abrigos.FindAsync(id);
    if (ab == null) return Results.NotFound();

    ab.Nome = dto.Nome;
    ab.Endereco = dto.Endereco;
    ab.Latitude = dto.Latitude;
    ab.Longitude = dto.Longitude;
    ab.CapacidadeMax = dto.CapacidadeMax;
    ab.DisponibilidadeAtual = dto.DisponibilidadeAtual;
    ab.Contato = dto.Contato;

    await context.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("UpdateAbrigo")
.WithTags("Abrigos")
.WithSummary("Atualiza abrigo existente")
.WithDescription("Altera dados de um abrigo cadastrado.")
.Accepts<AbrigoUpdateDto>("application/json")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapDelete("/abrigos/{id}", async (int id, AppDbContext context) =>
{
    var ab = await context.Abrigos.FindAsync(id);
    if (ab == null) return Results.NotFound();

    context.Abrigos.Remove(ab);
    await context.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteAbrigo")
.WithTags("Abrigos")
.WithSummary("Remove abrigo")
.WithDescription("Exclui um abrigo existente com base no ID.")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// ======== ROTAS SEGURAS ========

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
.WithTags("RotasSeguras")
.WithSummary("Lista todas as rotas seguras")
.WithDescription("Retorna todas as rotas registradas para deslocamento a abrigos.")
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
.WithName("GetRotaById")
.WithTags("RotasSeguras")
.WithSummary("Obtém rota segura por ID")
.WithDescription("Retorna dados de uma rota segura específica.")
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
.WithName("CreateRota")
.WithTags("RotasSeguras")
.WithSummary("Cria nova rota segura")
.WithDescription("Insere uma rota segura, associada a um usuário e destino de abrigo.")
.Accepts<RotasSegurasCreateDto>("application/json")
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
.WithName("UpdateRota")
.WithTags("RotasSeguras")
.WithSummary("Atualiza rota segura existente")
.WithDescription("Altera dados geográficos e tipo de rota de uma rota segura já registrada.")
.Accepts<RotasSegurasUpdateDto>("application/json")
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
.WithName("DeleteRota")
.WithTags("RotasSeguras")
.WithSummary("Remove rota segura")
.WithDescription("Exclui uma rota segura baseada no ID.")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// ======== SENSOR ========

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
.WithName("GetSensors")
.WithTags("Sensors")
.WithSummary("Lista todos os sensores")
.WithDescription("Retorna todos os sensores cadastrados, incluindo tipo, localização, status e data de instalação.")
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
.WithTags("Sensors")
.WithSummary("Obtém sensor por ID")
.WithDescription("Retorna detalhes de um sensor específico.")
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
.WithTags("Sensors")
.WithSummary("Cria novo sensor")
.WithDescription("Insere um sensor com tipo, localização, status e data de instalação.")
.Accepts<SensorCreateDto>("application/json")
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
.WithTags("Sensors")
.WithSummary("Atualiza sensor existente")
.WithDescription("Altera dados de um sensor já registrado.")
.Accepts<SensorUpdateDto>("application/json")
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
.WithTags("Sensors")
.WithSummary("Remove sensor")
.WithDescription("Exclui um sensor existente com base no ID.")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// ======== LEITURA SENSOR ========

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
.WithTags("Leituras")
.WithSummary("Lista todas as leituras de sensor")
.WithDescription("Retorna todas as leituras de valores de sensores, incluindo data/hora e unidade de medida.")
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
.WithTags("Leituras")
.WithSummary("Obtém leitura de sensor por ID")
.WithDescription("Retorna detalhes de uma leitura específica.")
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
.WithTags("Leituras")
.WithSummary("Cria nova leitura de sensor")
.WithDescription("Registra uma nova leitura de sensor, vinculada a um SensorId existente.")
.Accepts<LeituraSensorCreateDto>("application/json")
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
.WithTags("Leituras")
.WithSummary("Atualiza leitura de sensor existente")
.WithDescription("Altera valor e unidade de medida de uma leitura registrada.")
.Accepts<LeituraSensorUpdateDto>("application/json")
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
.WithTags("Leituras")
.WithSummary("Remove leitura de sensor")
.WithDescription("Exclui uma leitura de sensor específica.")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// ======== OCORRÊNCIA COLABORATIVA ========

app.MapGet("/ocorrencias", async (AppDbContext context) =>
{
    var items = await context.Ocorrencias
        .Select(o => new OcorrenciaColaborativaReadDto
        {
            Id = o.Id,
            UsuarioId = o.UsuarioId,
            TipoOcorrencia = o.TipoOcorrencia,
            Descricao = o.Descricao,
            Latitude = o.Latitude,
            Longitude = o.Longitude,
            DataEnvio = o.DataEnvio,
            Status = o.Status
        })
        .ToListAsync();
    return Results.Ok(items);
})
.WithName("GetOcorrencias")
.WithTags("Ocorrencias")
.WithSummary("Lista todas as ocorrências colaborativas")
.WithDescription("Retorna todas as ocorrências registradas por usuários, com descrição e localização.")
.Produces<List<OcorrenciaColaborativaReadDto>>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapGet("/ocorrencias/{id}", async (int id, AppDbContext context) =>
{
    var o = await context.Ocorrencias.FindAsync(id);
    return o == null
        ? Results.NotFound()
        : Results.Ok(new OcorrenciaColaborativaReadDto
        {
            Id = o.Id,
            UsuarioId = o.UsuarioId,
            TipoOcorrencia = o.TipoOcorrencia,
            Descricao = o.Descricao,
            Latitude = o.Latitude,
            Longitude = o.Longitude,
            DataEnvio = o.DataEnvio,
            Status = o.Status
        });
})
.WithName("GetOcorrenciaById")
.WithTags("Ocorrencias")
.WithSummary("Obtém ocorrência colaborativa por ID")
.WithDescription("Retorna os detalhes de uma ocorrência enviada por um usuário.")
.Produces<OcorrenciaColaborativaReadDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapPost("/ocorrencias", async (OcorrenciaColaborativaCreateDto dto, AppDbContext context) =>
{
    var o = new OcorrenciaColaborativa
    {
        UsuarioId = dto.UsuarioId,
        TipoOcorrencia = dto.TipoOcorrencia,
        Descricao = dto.Descricao,
        Latitude = dto.Latitude,
        Longitude = dto.Longitude,
        DataEnvio = DateTime.UtcNow,
        Status = dto.Status
    };
    context.Ocorrencias.Add(o);
    await context.SaveChangesAsync();

    var readDto = new OcorrenciaColaborativaReadDto
    {
        Id = o.Id,
        UsuarioId = o.UsuarioId,
        TipoOcorrencia = o.TipoOcorrencia,
        Descricao = o.Descricao,
        Latitude = o.Latitude,
        Longitude = o.Longitude,
        DataEnvio = o.DataEnvio,
        Status = o.Status
    };
    return Results.Created($"/ocorrencias/{o.Id}", readDto);
})
.WithName("CreateOcorrencia")
.WithTags("Ocorrencias")
.WithSummary("Cria nova ocorrência colaborativa")
.WithDescription("Registra uma nova ocorrência colaborativa vinculada a um usuário.")
.Accepts<OcorrenciaColaborativaCreateDto>("application/json")
.Produces<OcorrenciaColaborativaReadDto>(StatusCodes.Status201Created)
.WithOpenApi();

app.MapPut("/ocorrencias/{id}", async (int id, OcorrenciaColaborativaUpdateDto dto, AppDbContext context) =>
{
    var o = await context.Ocorrencias.FindAsync(id);
    if (o == null) return Results.NotFound();

    o.TipoOcorrencia = dto.TipoOcorrencia;
    o.Descricao = dto.Descricao;
    o.Latitude = dto.Latitude;
    o.Longitude = dto.Longitude;
    o.Status = dto.Status;

    await context.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("UpdateOcorrencia")
.WithTags("Ocorrencias")
.WithSummary("Atualiza ocorrência colaborativa existente")
.WithDescription("Altera dados de uma ocorrência colaborativa já registrada.")
.Accepts<OcorrenciaColaborativaUpdateDto>("application/json")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapDelete("/ocorrencias/{id}", async (int id, AppDbContext context) =>
{
    var o = await context.Ocorrencias.FindAsync(id);
    if (o == null) return Results.NotFound();

    context.Ocorrencias.Remove(o);
    await context.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteOcorrencia")
.WithTags("Ocorrencias")
.WithSummary("Remove ocorrência colaborativa")
.WithDescription("Exclui uma ocorrência colaborativa com base no ID.")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.Run();
