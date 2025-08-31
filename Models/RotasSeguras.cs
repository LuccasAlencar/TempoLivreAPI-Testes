namespace TempoLivreAPI.Models
{
    public class RotasSeguras
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int AbrigoDestinoId { get; set; }
        public double? OrigemLatitude { get; set; }
        public double? OrigemLongitude { get; set; }
        public double? DestinoLatitude { get; set; }
        public double? DestinoLongitude { get; set; }
        public string? TipoRota { get; set; }

        // Navegação
        public Usuario Usuario { get; set; } = null!;
        public Abrigo AbrigoDestino { get; set; } = null!;
    }
}