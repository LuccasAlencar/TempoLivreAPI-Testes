namespace TempoLivreAPI.DTOs
{
    public class LeituraSensorReadDto
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public double ValorLido { get; set; }
        public DateTime DataHora { get; set; }
        public string? UnidadeMedida { get; set; }
    }

    public class LeituraSensorCreateDto
    {
        public int SensorId { get; set; }
        public double ValorLido { get; set; }
        public string? UnidadeMedida { get; set; }
    }

    public class LeituraSensorUpdateDto
    {
        public double ValorLido { get; set; }
        public string? UnidadeMedida { get; set; }
    }
}