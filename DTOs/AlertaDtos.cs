namespace TempoLivreAPI.DTOs
{
    public class AlertaReadDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string NivelSeveridade { get; set; } = string.Empty;
        public DateTime DataHoraCriacao { get; set; }
    }

    public class AlertaCreateDto
    {
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string NivelSeveridade { get; set; } = string.Empty;
    }

    public class AlertaUpdateDto
    {
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string NivelSeveridade { get; set; } = string.Empty;
    }
}