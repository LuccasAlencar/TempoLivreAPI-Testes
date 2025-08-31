namespace TempoLivreAPI.Models
{
    public class Alerta
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public string TipoEvento { get; set; } = string.Empty;
        public string NivelAlerta { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Status { get; set; } = string.Empty;

        // Navegação
        public Usuario? Usuario { get; set; }
    }
}