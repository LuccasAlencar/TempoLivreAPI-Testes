namespace TempoLivreAPI.DTOs
{
    public class AbrigoReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Endereco { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? CapacidadeMax { get; set; }
        public int? DisponibilidadeAtual { get; set; }
        public string? Contato { get; set; }
    }

    public class AbrigoCreateDto
    {
        public string Nome { get; set; } = null!;
        public string? Endereco { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? CapacidadeMax { get; set; }
        public int? DisponibilidadeAtual { get; set; }
        public string? Contato { get; set; }
    }

    public class AbrigoUpdateDto
    {
        public string Nome { get; set; } = null!;
        public string? Endereco { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? CapacidadeMax { get; set; }
        public int? DisponibilidadeAtual { get; set; }
        public string? Contato { get; set; }
    }
}