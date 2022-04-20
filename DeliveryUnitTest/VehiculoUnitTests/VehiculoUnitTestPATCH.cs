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
    public class VehiculoUnitTestPATCH
    {
        const int ERROR_404 = 404;
        const int ERROR_500 = 500;

        MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DeliveryMappings());
        });

        /***    Pruebas unitarias PATCH  ***/
        [Fact]
        public void UpdateVehiculoTest_BadRequest()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(8);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un vehiculo nulo para pasarlo como parámetro.
            VehiculoUpdateDTO vehiculoNull = null;

            // Pasamos también como parámetro el Id del primer vehículo (Id = 1).
            Vehiculo vehiculo1 = testVehiculos.Vehiculos.First(a => a.Id == 1);

            var result = controller.UpdateVehiculo(vehiculo1.Id, vehiculoNull);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdateVehiculoTest_NotFound()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(9);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un vehiculo cuyo Id no existe en BD para pasarlo como parámetro.
            VehiculoUpdateDTO vehiculoOutId = new VehiculoUpdateDTO
            {
                Id = 10,
                Direccion = "Demo actuliza Direc.",
                Conductor = "Conductor 10",
                Latitud = 1.234,
                Longitud = 6.789,
            };

            var result = controller.UpdateVehiculo(vehiculoOutId.Id, vehiculoOutId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void UpdateVehiculoTest_StatusCode500()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(10);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un vehiculo con datos incorrectos.
            VehiculoUpdateDTO vehiculoError = new VehiculoUpdateDTO
            {
                Id = 3,
                Direccion = null,
                Conductor = "Conductor Num 3",
                Latitud = 1.4352,
                Longitud = -2.82574,
            };
            var result = controller.UpdateVehiculo(vehiculoError.Id, vehiculoError);

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
        public void UpdateVehiculoTest_Ok()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(11);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un vehiculo con datos incorrectos.
            VehiculoUpdateDTO vehiculoUpdate = new VehiculoUpdateDTO
            {
                Id = 1,
                Direccion = "Demo edita nueva Direccion",
                Conductor = "Conductor 1 Editado",
                Latitud = 10.743997453,
                Longitud = -1.92549102,
            };
            var result = controller.UpdateVehiculo(vehiculoUpdate.Id, vehiculoUpdate);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}