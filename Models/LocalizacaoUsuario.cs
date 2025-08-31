namespace TempoLivreAPI.Models
{
    public class LocalizacaoUsuario
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DataHoraRegistro { get; set; }

        // Navegação
        public Usuario Usuario { get; set; } = null!;
    }
}