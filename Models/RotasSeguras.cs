using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempoLivreAPI.Models
{
    [Table("RotasSeguras")]
    public class RotasSeguras
    {
        [Key] 
        [Column("id_rota")] 
        public int Id { get; set; }
        
        [Column("id_usuario")] 
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Column("id_abrigo_destino")] 
        public int AbrigoDestinoId { get; set; }
        public Abrigo AbrigoDestino { get; set; } = null!;

        [Column("origem_latitude")] 
        public double? OrigemLatitude { get; set; }

        [Column("origem_longitude")] 
        public double? OrigemLongitude { get; set; }

        [Column("destino_latitude")] 
        public double? DestinoLatitude { get; set; }

        [Column("destino_longitude")] 
        public double? DestinoLongitude { get; set; }

        [Column("tipo_rota")] 
        [MaxLength(50)] 
        public string? TipoRota { get; set; }
    }
}