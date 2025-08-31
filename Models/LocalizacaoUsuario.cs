using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempoLivreAPI.Models
{
    [Table("LocalizacaoUsuario")]
    public class LocalizacaoUsuario
    {
        [Key] [Column("id_localizacao")] public int Id { get; set; }
        [Column("id_usuario")] public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Column("latitude")] public double Latitude { get; set; }

        [Column("longitude")] public double Longitude { get; set; }

        [Column("data_hora_registro")] public DateTime DataHoraRegistro { get; set; }
    }
}