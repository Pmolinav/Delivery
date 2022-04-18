using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAPI.Models
{
    [Table("Vehiculo")]
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public double Latitud { get; set; }
        [Required]
        public double Longitud { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime RevisionDate { get; set; }
    }
}
