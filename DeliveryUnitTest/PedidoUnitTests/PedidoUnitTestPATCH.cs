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
    public class PedidoUnitTestPATCH
    {
        const int ERROR_404 = 404;
        const int ERROR_500 = 500;

        MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DeliveryMappings());
        });

        /***    Pruebas unitarias PATCH  ***/
        [Fact]
        public void UpdatePedidoTest_BadRequest()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(11);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            //Creamos un pedido nulo para pasarlo como parámetro.
            PedidoUpdateDTO pedidoNull = null;

            //Pasamos también como parámetro el Id del primer vehículo (Id = 1).
            Pedido pedido1 = testPedidos.Pedidos.First(a => a.Id == 1);

            var result = controller.UpdatePedido(pedido1.Id, pedidoNull);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdatePedidoTest_NotFound()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(12);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            //Creamos un pedido cuyo Id no existe en BD para pasarlo como parámetro.
            PedidoUpdateDTO pedidoOutId = new PedidoUpdateDTO
            {
                Id = 10,
                Titulo = "Demo Titulo Update",
                Urgencia = TipoUrgencia.Media,
                VehiculoId = 1
            };

            var result = controller.UpdatePedido(pedidoOutId.Id, pedidoOutId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void UpdatePedidoTest_StatusCode404()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(13);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            //Creamos un pedido con un Id de vehículo que no existe en BD.
            PedidoUpdateDTO pedidoErrVh = new PedidoUpdateDTO
            {
                Id = 1,
                Titulo = "Demo Titulo Update",
                Urgencia = TipoUrgencia.Media,
                VehiculoId = 99
            };
            var result = controller.UpdatePedido(pedidoErrVh.Id, pedidoErrVh);

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
        public void UpdatePedidoTest_StatusCode500()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(14);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            //Creamos un pedido con datos incorrectos.
            PedidoUpdateDTO pedidoTituloNull = new PedidoUpdateDTO
            {
                Id = 1,
                Titulo = null,
                Urgencia = TipoUrgencia.Media,
                VehiculoId = 1
            };
            var result = controller.UpdatePedido(pedidoTituloNull.Id, pedidoTituloNull);

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
        public void UpdatePedidoTest_Ok()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testPedidos = new ContextMemoria().ObtenerContextPedido(15);

            InicializaDatos.InicializarPedidos_OK(testPedidos);
            var vehiculoRepo = new VehiculoRepository(testPedidos);
            var pedidoRepo = new PedidoRepository(testPedidos);

            var controller = new PedidoController(pedidoRepo, vehiculoRepo, mapper);

            // Creamos un pedido a actualizar con datos correctos.
            PedidoUpdateDTO pedidoUpdate = new PedidoUpdateDTO
            {
                Id = 1,
                Titulo = "Actualiza Pedido 1",
                Urgencia = TipoUrgencia.Alta,
                VehiculoId = 2
            };

            var result = controller.UpdatePedido(pedidoUpdate.Id, pedidoUpdate);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}