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
    public class PedidoUnitTestGET
    {
        const int ERROR_404 = 404;
        const int ERROR_500 = 500;

        MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DeliveryMappings());
        });

        /***    Pruebas unitarias GET   ***/
        [Fact]
        public void GetPedidosTest_OK()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(1);
            InicializaDatos.InicializarPedidos_OK(testPedidos);

            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);
            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            var result = controller.GetPedidos();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetPedidoTest_OK()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(2);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);
            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Pasamos como parámetro el Id del primer pedido (Id = 1).
            Pedido pedido1 = testPedidos.Pedidos.First(a => a.Id == 1);
            var result = controller.GetPedido(pedido1.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetPedidoTest_NotFound()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();
            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(3);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Pasamos un Id de pedido que no se encuentra en los datos.
            int indexOut = 99;
            var result = controller.GetPedido(indexOut);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetPedidoInVehiculoTest_OK()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(4);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);
            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Pasamos como parámetro el Id del primer vehiculo (Id = 1).
            Vehiculo vehiculo1 = testPedidos.Vehiculos.First(a => a.Id == 1);
            var result = controller.GetPedidosInVehiculo(vehiculo1.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetPedidoInVehiculoTest_NotFound()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();
            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(5);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Pasamos un Id de vehículo que no se encuentra en los datos.
            int indexOut = 99;
            var result = controller.GetPedidosInVehiculo(indexOut);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}