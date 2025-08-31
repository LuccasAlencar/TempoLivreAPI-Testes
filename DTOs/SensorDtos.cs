namespace TempoLivreAPI.DTOs
{
    public class SensorReadDto
    {
        public int Id { get; set; }
        public string? TipoSensor { get; set; }
        public double? LocalizacaoLat { get; set; }
        public double? LocalizacaoLong { get; set; }
        public string? Status { get; set; }
        public DateTime? DataInstalacao { get; set; }
    }

    public class SensorCreateDto
    {
        public string? TipoSensor { get; set; }
        public double? LocalizacaoLat { get; set; }
        public double? LocalizacaoLong { get; set; }
        public string? Status { get; set; }
        public DateTime? DataInstalacao { get; set; }
    }

    public class SensorUpdateDto
    {
        public string? TipoSensor { get; set; }
        public double? LocalizacaoLat { get; set; }
        public double? LocalizacaoLong { get; set; }
        public string? Status { get; set; }
        public DateTime? DataInstalacao { get; set; }
    }
}