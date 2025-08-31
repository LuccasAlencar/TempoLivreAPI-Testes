using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TempoLivreAPI.Data;
using TempoLivreAPI.Endpoints;

Env.Load(); // carrega variáveis do .env

var builder = WebApplication.CreateBuilder(args);

// DB Oracle - Usando variáveis de ambiente
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var oracleUser = Environment.GetEnvironmentVariable("ORACLE_USER") ?? "SeuUsuario";
    var oraclePassword = Environment.GetEnvironmentVariable("ORACLE_PASSWORD") ?? "SuaSenha";
    var oracleHost = Environment.GetEnvironmentVariable("ORACLE_HOST") ?? "oracle.fiap.com.br";
    var oraclePort = Environment.GetEnvironmentVariable("ORACLE_PORT") ?? "1521";
    var oracleService = Environment.GetEnvironmentVariable("ORACLE_SERVICE") ?? "orcl";

    var connectionString = 
        $"User Id={oracleUser};Password={oraclePassword};Data Source=" +
        $"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={oracleHost})(PORT={oraclePort}))" +
        $"(CONNECT_DATA=(SERVICE_NAME={oracleService})));";

    opt.UseOracle(connectionString);
});


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tempo Livre API",
        Version = "v1",
        Description = "API RESTful mínima em .NET 8 com Oracle, EF Core, Swagger e migrations",
        Contact = new OpenApiContact { Name = "Tempo Livre" }
    });
});

var app = builder.Build();

// Banco: migrate e seed
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.EnsureDeletedAsync();
    await context.Database.MigrateAsync();
    DbSeeder.Seed(context);
}

// Swagger em Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tempo Livre API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// Registrar endpoints via extensão
app.MapUsuarioEndpoints();
app.MapLocalizacaoEndpoints();
app.MapAlertaEndpoints();
app.MapAbrigoEndpoints();
app.MapRotaSeguraEndpoints();
app.MapSensorEndpoints();
app.MapLeituraEndpoints();
app.MapOcorrenciaEndpoints();

await app.RunAsync();