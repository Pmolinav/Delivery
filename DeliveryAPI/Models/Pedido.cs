using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DeliveryAPI.Enums.DeliveryEnums;

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
        [Required]
        [Range(0, 3)]
        public TipoUrgencia Urgencia { get; set; }
        [Required]
        public int VehiculoId { get; set; }
        [ForeignKey("VehiculoId")]
        public Vehiculo Vehiculo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? RevisionDate { get; set; }
    }
}
