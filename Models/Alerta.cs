using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempoLivreAPI.Models
{
    [Table("Alerta")]
    public class Alerta
    {
        [Key] [Column("id_alerta")] public int Id { get; set; }

        [Column("id_usuario")] public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Column("tipo_evento")]
        [MaxLength(50)]
        public string TipoEvento { get; set; } = null!;

        [Column("nivel_alerta")]
        [MaxLength(20)]
        public string NivelAlerta { get; set; } = null!;

        [Column("mensagem")] [MaxLength(255)] public string? Mensagem { get; set; }

        [Column("latitude")] public double? Latitude { get; set; }

        [Column("longitude")] public double? Longitude { get; set; }

        [Column("data_emissao")] public DateTime DataEmissao { get; set; }

        [Column("status")] [MaxLength(20)] public string Status { get; set; } = null!;
    }
}