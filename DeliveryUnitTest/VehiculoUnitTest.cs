using AutoMapper;
using DeliveryAPI;
using DeliveryAPI.Data;
using DeliveryAPI.Mapper;
using DeliveryAPI.Models;
using DeliveryAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DeliveryUnitTest
{
    public class VehiculoUnitTest
    {
        [Fact]
        public void GetVehiculosTest_OK()
        {

            // Arrangeb
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DeliveryMappings());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            // Act
            var testVehiculos = new ContextMemoria().ObtenerContext();
            InicializaDatos.Inicializar_OK(testVehiculos);

            var repository = new VehiculoRepository(testVehiculos);
            var controller = new VehiculoController(repository, mapper);

            var result = controller.GetVehiculos();
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}