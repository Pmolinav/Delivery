using System.ComponentModel.DataAnnotations;

namespace DeliveryAPI.Models.DTOs
{
    public class VehiculoDTO
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
