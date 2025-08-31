namespace TempoLivreAPI.Models
{
    public class LeituraSensor
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public double ValorLido { get; set; }
        public DateTime DataHora { get; set; }
        public string? UnidadeMedida { get; set; }

        // Navegação
        public Sensor Sensor { get; set; } = null!;
    }
}