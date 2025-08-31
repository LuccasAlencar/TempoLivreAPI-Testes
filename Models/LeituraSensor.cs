using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempoLivreAPI.Models
{
    [Table("LeituraSensor")]
    public class LeituraSensor
    {
        [Key] [Column("id_leitura")] public int Id { get; set; }

        [Column("id_sensor")] public int SensorId { get; set; }
        public Sensor Sensor { get; set; } = null!;

        [Column("valor_lido")] public double ValorLido { get; set; }

        [Column("data_hora")] public DateTime DataHora { get; set; }

        [Column("unidade_medida")]
        [MaxLength(20)]
        public string? UnidadeMedida { get; set; }
    }
}