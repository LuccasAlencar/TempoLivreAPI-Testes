namespace TempoLivreAPI.DTOs
{
    public class LocalizacaoUsuarioReadDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DataHoraRegistro { get; set; }
    }

    public class LocalizacaoUsuarioCreateDto
    {
        public int UsuarioId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class LocalizacaoUsuarioUpdateDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}