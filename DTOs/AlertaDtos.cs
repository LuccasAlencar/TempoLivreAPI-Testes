namespace TempoLivreAPI.DTOs
{
    public class AlertaReadDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string TipoEvento { get; set; } = null!;
        public string NivelAlerta { get; set; } = null!;
        public string? Mensagem { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Status { get; set; } = null!;
    }

    public class AlertaCreateDto
    {
        public int UsuarioId { get; set; }
        public string TipoEvento { get; set; } = null!;
        public string NivelAlerta { get; set; } = null!;
        public string? Mensagem { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Status { get; set; } = null!;
    }

    public class AlertaUpdateDto
    {
        public string TipoEvento { get; set; } = null!;
        public string NivelAlerta { get; set; } = null!;
        public string? Mensagem { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Status { get; set; } = null!;
    }
}