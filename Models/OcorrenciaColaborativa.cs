using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempoLivreAPI.Models
{
    [Table("OcorrenciaColaborativa")]
    public class OcorrenciaColaborativa
    {
        [Key] [Column("id_ocorrencia")] public int Id { get; set; }
        [Column("id_usuario")] public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Column("tipo_ocorrencia")]
        [MaxLength(100)]
        public string? TipoOcorrencia { get; set; }

        [Column("descricao")] [MaxLength(255)] public string? Descricao { get; set; }

        [Column("latitude")] public double? Latitude { get; set; }

        [Column("longitude")] public double? Longitude { get; set; }

        [Column("data_envio")] public DateTime DataEnvio { get; set; }

        [Column("status")] [MaxLength(20)] public string? Status { get; set; }
    }
}