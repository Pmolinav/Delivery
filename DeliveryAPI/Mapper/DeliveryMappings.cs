using AutoMapper;
using DeliveryAPI.Models;
using DeliveryAPI.Models.DTOs;

namespace DeliveryAPI.Mapper
{
    public class DeliveryMappings : Profile
    {
        public DeliveryMappings()
        {
            CreateMap<Vehiculo, VehiculoDTO>().ReverseMap();
            CreateMap<Vehiculo, VehiculoCreateDTO>().ReverseMap();
            CreateMap<Vehiculo, VehiculoUpdateDTO>().ReverseMap();
            CreateMap<Pedido, PedidoDTO>().ReverseMap();
            CreateMap<Pedido, PedidoCreateDTO>().ReverseMap();
            CreateMap<Pedido, PedidoUpdateDTO>().ReverseMap();
        }
    }
}
