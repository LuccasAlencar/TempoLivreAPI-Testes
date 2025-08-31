// Data/AppDbContext.cs

using Microsoft.EntityFrameworkCore;
using TempoLivreAPI.Models;

namespace TempoLivreAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<LocalizacaoUsuario> Localizacoes { get; set; } = null!;
        public DbSet<Alerta> Alertas { get; set; } = null!;
        public DbSet<Abrigo> Abrigos { get; set; } = null!;
        public DbSet<RotasSeguras> RotasSeguras { get; set; } = null!;
        public DbSet<Sensor> Sensors { get; set; } = null!;
        public DbSet<LeituraSensor> Leituras { get; set; } = null!;
        public DbSet<OcorrenciaColaborativa> Ocorrencias { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Removido UseOracleIdentityColumn() para evitar erro.
            // O Oracle EF Core criará as colunas IDENTITY automaticamente para chaves inteiras.

            // Configurações de relacionamento

            modelBuilder.Entity<LocalizacaoUsuario>()
                .HasOne(l => l.Usuario)
                .WithMany(u => u.Localizacoes)
                .HasForeignKey(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Alertas)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RotasSeguras>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.RotasSeguras)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RotasSeguras>()
                .HasOne(r => r.AbrigoDestino)
                .WithMany()
                .HasForeignKey(r => r.AbrigoDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeituraSensor>()
                .HasOne(l => l.Sensor)
                .WithMany(s => s.Leituras)
                .HasForeignKey(l => l.SensorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OcorrenciaColaborativa>()
                .HasOne(o => o.Usuario)
                .WithMany(u => u.Ocorrencias)
                .HasForeignKey(o => o.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
