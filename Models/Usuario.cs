using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TempoLivreAPI.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key] [Column("id_usuario")] public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        [Column("senha")]
        public string Senha { get; set; } = null!;

        [Column("data_cadastro")] public DateTime DataCadastro { get; set; }

        public ICollection<LocalizacaoUsuario> Localizacoes { get; set; } = new List<LocalizacaoUsuario>();
        public ICollection<Alerta> Alertas { get; set; } = new List<Alerta>();
        public ICollection<RotasSeguras> RotasSeguras { get; set; } = new List<RotasSeguras>();
        public ICollection<OcorrenciaColaborativa> Ocorrencias { get; set; } = new List<OcorrenciaColaborativa>();
    }
}
