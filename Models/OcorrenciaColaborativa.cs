namespace TempoLivreAPI.Models
{
    public class OcorrenciaColaborativa
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? TipoOcorrencia { get; set; }
        public string? Descricao { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime DataEnvio { get; set; }
        public string? Status { get; set; }

        // Navegação
        public Usuario Usuario { get; set; } = null!;
    }
}