using Microsoft.EntityFrameworkCore;
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
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Direccion { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string Conductor { get; set; }
        [Required]
        [Column(TypeName = "decimal(20, 16)")]
        public double Latitud { get; set; }
        [Required]
        [Column(TypeName = "decimal(20, 16)")]
        public double Longitud { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? RevisionDate { get; set; }
    }
}
