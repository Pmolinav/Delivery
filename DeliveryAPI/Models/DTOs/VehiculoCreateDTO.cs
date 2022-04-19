using System.ComponentModel.DataAnnotations;

namespace DeliveryAPI.Models.DTOs
{
    public class VehiculoCreateDTO
    {
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Conductor { get; set; }
        [Required]
        public double Latitud { get; set; }
        [Required]
        public double Longitud { get; set; }
        //public DateTime CreationDate { get; set; }
        //public DateTime RevisionDate { get; set; }
    }
}
