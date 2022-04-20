using AutoMapper;
using DeliveryAPI.Data;
using DeliveryAPI.Mapper;
using DeliveryAPI.Models;
using DeliveryAPI.Models.DTOs;
using DeliveryAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Xunit;

namespace DeliveryUnitTest
{
    public class VehiculoUnitTestPOST
    {
        const int ERROR_404 = 404;
        const int ERROR_500 = 500;

        MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DeliveryMappings());
        });

        /***    Pruebas unitarias POST  ***/
        [Fact]
        public void CreateVehiculoTest_BadRequest()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(4);

            // No introducimos datos debido a que no serán necesarios en esta prueba.
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un vehiculo nulo para pasarlo como parámetro.
            VehiculoCreateDTO vehiculoNull = null;
            var result = controller.CreateVehiculo(vehiculoNull);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void CreateVehiculoTest_StatusCode404()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(5);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un vehiculo cuyo conductor ya exista en BD para pasarlo como parámetro.
            VehiculoCreateDTO vehiculoConductor = new VehiculoCreateDTO
            {
                Direccion = "Demo nueva Direc.",
                Conductor = "Conductor 1",
                Latitud = 1.5642,
                Longitud = -2.6924,
            };
            var result = controller.CreateVehiculo(vehiculoConductor);

            // Assert
            try
            {
                ObjectResult obj = (ObjectResult)result;
                Assert.Equal(obj.StatusCode, ERROR_404);
            }
            // Si salta alguna excepción, la prueba no es correcta.
            catch (Exception e)
            {
                Assert.True(false, e.ToString());
            }
        }
        [Fact]
        public void CreateVehiculoTest_StatusCode500()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(6);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un vehiculo con datos incorrectos.
            VehiculoCreateDTO vehiculoDirecNull = new VehiculoCreateDTO
            {
                Direccion = null,
                Conductor = "ConductorD",
                Latitud = 1.4352,
                Longitud = -2.82574,
            };
            var result = controller.CreateVehiculo(vehiculoDirecNull);

            // Assert
            try
            {
                ObjectResult obj = (ObjectResult)result;
                Assert.Equal(obj.StatusCode, ERROR_500);
            }
            // Si salta alguna excepción, la prueba no es correcta.
            catch (Exception e)
            {
                Assert.True(false, e.ToString());
            }
        }
        [Fact]
        public void CreateVehiculoTest_CreatedAtRoute()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(7);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un vehiculo con datos incorrectos.
            VehiculoCreateDTO vehiculoNew = new VehiculoCreateDTO
            {
                Direccion = "Demo crea nueva Direccion",
                Conductor = "Conductor 7",
                Latitud = 10.743997453,
                Longitud = -1.92549102,
            };
            var result = controller.CreateVehiculo(vehiculoNew);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }
    }
}