using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAPI.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Titulo { get; set; }
        public enum TipoUrgencia { Baja, Media, Alta, Crítica }
        [Required]
        public TipoUrgencia Urgencia { get; set; }
        [Required]
        public int VehiculoId { get; set; }
        [ForeignKey("VehiculoId")]
        public Vehiculo Vehiculo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? RevisionDate { get; set; }
    }
}
