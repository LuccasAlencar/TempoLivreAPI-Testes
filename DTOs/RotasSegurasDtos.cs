namespace TempoLivreAPI.DTOs
{
    public class RotasSegurasReadDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int AbrigoDestinoId { get; set; }
        public double? OrigemLatitude { get; set; }
        public double? OrigemLongitude { get; set; }
        public double? DestinoLatitude { get; set; }
        public double? DestinoLongitude { get; set; }
        public string? TipoRota { get; set; }
    }

    public class RotasSegurasCreateDto
    {
        public int UsuarioId { get; set; }
        public int AbrigoDestinoId { get; set; }
        public double? OrigemLatitude { get; set; }
        public double? OrigemLongitude { get; set; }
        public double? DestinoLatitude { get; set; }
        public double? DestinoLongitude { get; set; }
        public string? TipoRota { get; set; }
    }

    public class RotasSegurasUpdateDto
    {
        public double? OrigemLatitude { get; set; }
        public double? OrigemLongitude { get; set; }
        public double? DestinoLatitude { get; set; }
        public double? DestinoLongitude { get; set; }
        public string? TipoRota { get; set; }
    }
}