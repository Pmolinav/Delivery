using AutoMapper;
using DeliveryAPI.Controllers;
using DeliveryAPI.Data;
using DeliveryAPI.Mapper;
using DeliveryAPI.Models;
using DeliveryAPI.Models.DTOs;
using DeliveryAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Xunit;
using static DeliveryAPI.Enums.DeliveryEnums;

namespace DeliveryUnitTest
{
    public class PedidoUnitTestDELETE
    {
        const int ERROR_404 = 404;
        const int ERROR_500 = 500;

        MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DeliveryMappings());
        });

        /***    Pruebas unitarias DELETE  ***/
        [Fact]
        public void DeletePedidoTest_NotFound()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(16);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Creamos un Id de pedido que no existe en BD para pasarlo como parámetro.
            int idOutIndex = 99;

            var result = controller.DeletePedido(idOutIndex);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeletePedidoTest_Ok()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(17);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Cogemos un Id de pedido existente para borrarlo de BD.
            Pedido pedido1 = testPedidos.Pedidos.First();
            var result = controller.DeletePedido(pedido1.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}