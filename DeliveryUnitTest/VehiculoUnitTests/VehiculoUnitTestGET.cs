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
    public class VehiculoUnitTestGET
    {
        const int ERROR_404 = 404;
        const int ERROR_500 = 500;

        MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DeliveryMappings());
        });

        /***    Pruebas unitarias GET   ***/
        [Fact]
        public void GetVehiculosTest_OK()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(1);
            InicializaDatos.InicializarVehiculos_OK(testVehiculos);

            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            var result = controller.GetVehiculos();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetVehiculoTest_OK()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(2);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Pasamos como parámetro el Id del primer vehículo (Id = 1).
            Vehiculo vehiculo1 = testVehiculos.Vehiculos.First(a => a.Id == 1);
            var result = controller.GetVehiculo(vehiculo1.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public void GetVehiculoTest_NotFound()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();
            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(3);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);

            var controller = new VehiculoController(repository, mapper);

            // Pasamos un Id que no se encuentra en los datos.
            int indexOut = 99;
            var result = controller.GetVehiculo(indexOut);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}