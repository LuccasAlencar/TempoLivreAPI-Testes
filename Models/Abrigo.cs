using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempoLivreAPI.Models
{
    [Table("Abrigo")]
    public class Abrigo
    {
        [Key] [Column("id_abrigo")] public int Id { get; set; }

        [Column("nome")] [MaxLength(100)] public string Nome { get; set; } = null!;

        [Column("endereco")] [MaxLength(200)] public string? Endereco { get; set; }

        [Column("latitude")] public double? Latitude { get; set; }

        [Column("longitude")] public double? Longitude { get; set; }

        [Column("capacidade_max")] public int? CapacidadeMax { get; set; }

        [Column("disponibilidade_atual")] public int? DisponibilidadeAtual { get; set; }

        [Column("contato")] [MaxLength(50)] public string? Contato { get; set; }
    }
}