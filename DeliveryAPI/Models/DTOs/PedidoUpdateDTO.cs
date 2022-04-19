using System.ComponentModel.DataAnnotations;
using static DeliveryAPI.Models.Pedido;

namespace DeliveryAPI.Models.DTOs
{
    public class PedidoUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public TipoUrgencia Urgencia { get; set; }
        [Required]
        public int VehiculoId { get; set; }

        //public VehiculoDTO Vehiculo { get; set; }
        //public DateTime CreationDate { get; set; }
        //public DateTime RevisionDate { get; set; }
    }
}
