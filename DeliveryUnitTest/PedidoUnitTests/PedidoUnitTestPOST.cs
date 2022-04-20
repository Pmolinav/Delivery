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
    public class PedidoUnitTestPOST
    {
        const int ERROR_404 = 404;
        const int ERROR_500 = 500;

        MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DeliveryMappings());
        });

        /***    Pruebas unitarias POST  ***/
        [Fact]
        public void CreatePedidoTest_BadRequest()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(6);

            //No introducimos datos debido a que no serán necesarios en esta prueba.
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            //Creamos un pedido nulo para pasarlo como parámetro.
            PedidoCreateDTO pedidoNull = null;
            var result = controller.CreatePedido(pedidoNull);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void CreatePedidoTest_StatusCode404_PedidoRep()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(7);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Creamos un pedido cuyo título ya existe en BD para pasarlo como parámetro.
            PedidoCreateDTO pedidoTitulo = new PedidoCreateDTO
            {
                Titulo = "Demo Pedido 1",
                Urgencia = TipoUrgencia.Baja,
                VehiculoId = 1
            };
            var result = controller.CreatePedido(pedidoTitulo);

            // Assert
            try
            {
                ObjectResult obj = (ObjectResult)result;
                Assert.Equal(obj.StatusCode, ERROR_404);
            }
            //Si salta alguna excepción, la prueba no es correcta.
            catch (Exception e)
            {
                Assert.True(false, e.ToString());
            }
        }
        [Fact]
        public void CreatePedidoTest_StatusCode404_IdVehiculo()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(8);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Creamos un pedido cuyo Id de vehículo no existe en BD.
            PedidoCreateDTO pedidoIdVhOut = new PedidoCreateDTO
            {
                Titulo = "Demo Pedido Nuevo",
                Urgencia = TipoUrgencia.Alta,
                VehiculoId = 99
            };
            var result = controller.CreatePedido(pedidoIdVhOut);

            // Assert
            try
            {
                ObjectResult obj = (ObjectResult)result;
                Assert.Equal(obj.StatusCode, ERROR_404);
            }
            //Si salta alguna excepción, la prueba no es correcta.
            catch (Exception e)
            {
                Assert.True(false, e.ToString());
            }
        }
        [Fact]
        public void CreatePedidoTest_StatusCode500()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(9);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Creamos un pedido con datos erróneos para pasarlo como parámetro.
            PedidoCreateDTO pedidoError = new PedidoCreateDTO
            {
                Titulo = null,
                Urgencia = TipoUrgencia.Alta,
                VehiculoId = 1
            };
            var result = controller.CreatePedido(pedidoError);

            // Assert
            try
            {
                ObjectResult obj = (ObjectResult)result;
                Assert.Equal(obj.StatusCode, ERROR_500);
            }
            //Si salta alguna excepción, la prueba no es correcta.
            catch (Exception e)
            {
                Assert.True(false, e.ToString());
            }
        }
        [Fact]
        public void CreatePedidoTest_CreatedAtRoute()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(10);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            //Creamos un pedido con datos correctos.
            PedidoCreateDTO pedidoNew = new PedidoCreateDTO
            {
                Titulo = "Nuevo Pedido Creado",
                Urgencia = TipoUrgencia.Alta,
                VehiculoId = 1
            };
            var result = controller.CreatePedido(pedidoNew);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }
    }
}