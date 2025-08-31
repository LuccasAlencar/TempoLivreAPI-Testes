namespace TempoLivreAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public DateTime DataCadastro { get; set; }

        // Navegação
        public ICollection<LocalizacaoUsuario> Localizacoes { get; set; } = new List<LocalizacaoUsuario>();
        public ICollection<Alerta> Alertas { get; set; } = new List<Alerta>();
        public ICollection<RotasSeguras> RotasSeguras { get; set; } = new List<RotasSeguras>();
        public ICollection<OcorrenciaColaborativa> Ocorrencias { get; set; } = new List<OcorrenciaColaborativa>();
    }
}