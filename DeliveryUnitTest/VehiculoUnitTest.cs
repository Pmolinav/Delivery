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
    public class VehiculoUnitTest
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(1);
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(2);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Pasamos como parámetro el Id del primer vehículo (Id = 1).
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(3);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);

            var controller = new VehiculoController(repository, mapper);

            //Pasamos un Id que no se encuentra en los datos.
            int indexOut = 99;
            var result = controller.GetVehiculo(indexOut);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        /***    Pruebas unitarias POST  ***/
        public void CreateVehiculoTest_BadRequest()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(4);

            //No introducimos datos debido a que no serán necesarios en esta prueba.
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un vehiculo nulo para pasarlo como parámetro.
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(5);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un vehiculo cuyo conductor ya exista en BD para pasarlo como parámetro.
            VehiculoCreateDTO vehiculoConductor = new VehiculoCreateDTO
            {
                Direccion = "Demo nueva Direc.", Conductor = "Conductor 1", Latitud = 1.5642, Longitud = -2.6924,
            };
            var result = controller.CreateVehiculo(vehiculoConductor);

            // Assert
            try
            {
                ObjectResult obj = (ObjectResult)result;
                Assert.Equal(obj.StatusCode, ERROR_404);
            }
            //Si salta alguna excepción, la prueba no es correcta.
            catch(Exception e)
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(6);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un vehiculo con datos incorrectos.
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
            //Si salta alguna excepción, la prueba no es correcta.
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(7);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un vehiculo con datos incorrectos.
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
        [Fact]
        /***    Pruebas unitarias PATCH  ***/
        public void UpdateVehiculoTest_BadRequest()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(8);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un vehiculo nulo para pasarlo como parámetro.
            VehiculoUpdateDTO vehiculoNull = null;

            //Pasamos también como parámetro el Id del primer vehículo (Id = 1).
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(9);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un vehiculo cuyo Id no existe en BD para pasarlo como parámetro.
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(10);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un vehiculo con datos incorrectos.
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(11);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un vehiculo con datos incorrectos.
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
        [Fact]
        /***    Pruebas unitarias DELETE  ***/
        public void DeleteVehiculoTest_NotFound()
        {
            // Arrange
            IMapper mapper = mappingConfig.CreateMapper();

            // Act
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(12);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un Id que no existe en BD para pasarlo como parámetro.
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
            ApplicationDbContext testVehiculos = new ContextMemoria().ObtenerContext(13);

            InicializaDatos.InicializarVehiculos_OK(testVehiculos);
            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            //Creamos un vehiculo con datos incorrectos.
            int idVehiculoDelete = 1;
            var result = controller.DeleteVehiculo(idVehiculoDelete);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}