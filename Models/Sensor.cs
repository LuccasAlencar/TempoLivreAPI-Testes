namespace TempoLivreAPI.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string? TipoSensor { get; set; }
        public double? LocalizacaoLat { get; set; }
        public double? LocalizacaoLong { get; set; }
        public string? Status { get; set; }
        public DateTime? DataInstalacao { get; set; }

        // Navegação
        public ICollection<LeituraSensor> Leituras { get; set; } = new List<LeituraSensor>();
    }
}