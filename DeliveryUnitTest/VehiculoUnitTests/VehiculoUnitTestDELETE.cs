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
    public class VehiculoUnitTestDELETE
    {
        const int ERROR_404 = 404;
        const int ERROR_500 = 500;

        MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DeliveryMappings());
        });

        /***    Pruebas unitarias DELETE  ***/
        [Fact]
        public void DeleteVehiculoTest_NotFound()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(12);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un Id que no existe en BD para pasarlo como parámetro.
            int idOutIndex = 10;

            var result = controller.DeleteVehiculo(idOutIndex);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteVehiculoTest_Ok()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContextVehiculo(13);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            // Creamos un vehiculo con datos incorrectos.
            Vehiculo vehiculoDelete = testVehiculos.Vehiculos.First();
            var result = controller.DeleteVehiculo(vehiculoDelete.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}