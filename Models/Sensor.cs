using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempoLivreAPI.Models
{
    [Table("Sensor")]
    public class Sensor
    {
        [Key] 
        [Column("id_sensor")] 
        public int Id { get; set; }

        [Column("tipo_sensor")]
        [MaxLength(50)]
        public string? TipoSensor { get; set; }

        [Column("localizacao_lat")] 
        public double? LocalizacaoLat { get; set; }

        [Column("localizacao_long")] 
        public double? LocalizacaoLong { get; set; }

        [Column("status")] 
        [MaxLength(20)] 
        public string? Status { get; set; }

        [Column("data_instalacao")] 
        public DateTime? DataInstalacao { get; set; }

        public ICollection<LeituraSensor> Leituras { get; set; } = new List<LeituraSensor>();
    }
}