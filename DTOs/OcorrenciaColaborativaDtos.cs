namespace TempoLivreAPI.DTOs
{
    public class OcorrenciaColaborativaReadDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? TipoOcorrencia { get; set; }
        public string? Descricao { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime DataEnvio { get; set; }
        public string? Status { get; set; }
    }

    public class OcorrenciaColaborativaCreateDto
    {
        public int UsuarioId { get; set; }
        public string? TipoOcorrencia { get; set; }
        public string? Descricao { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Status { get; set; }
    }

    public class OcorrenciaColaborativaUpdateDto
    {
        public string? TipoOcorrencia { get; set; }
        public string? Descricao { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Status { get; set; }
    }
}