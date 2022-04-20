using DeliveryAPI.Data;
using DeliveryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitTest
{
    internal class InicializaDatos
    {
        public static void InicializarVehiculos_OK(ApplicationDbContext context)
        {
            //Te aseguras que la base de datos haya sido creada
            context.Database.EnsureCreated();
            var vehiculos = new Vehiculo[]
            {
                //1 
                new Vehiculo
            {
                Id = 1, Direccion = "Demo Direccion 1", Conductor = "Conductor 1",
                Latitud = 1.1234, Longitud = -1.4321, CreationDate = DateTime.Now, RevisionDate = null
            }, //2 
                new Vehiculo
            {
                Id = 2, Direccion = "Demo Direccion 2", Conductor = "Conductor 2",
                Latitud = -2.333, Longitud = -1.68771, CreationDate = DateTime.Now, RevisionDate = null
            }, //3 
                new Vehiculo
            {
                Id = 3, Direccion = "Demo Direccion 3", Conductor = "Conductor 3",
                Latitud = 10.97532, Longitud = 21.087654, CreationDate = DateTime.Now, RevisionDate = null
            }, //4 
                new Vehiculo
            {
                Id = 4, Direccion = "Demo Direccion 4", Conductor = "Conductor 4",
                Latitud = -1.1233234, Longitud = 7.243434, CreationDate = DateTime.Now, RevisionDate = null
            }, //5
                new Vehiculo
            {
                Id = 5, Direccion = "Demo Direccion 5", Conductor = "Conductor 5",
                Latitud = 13.92274, Longitud = -2.667892, CreationDate = DateTime.Now, RevisionDate = null
            }
        };
            foreach (Vehiculo registro in vehiculos)
            {
                context.Vehiculos.Add(registro);
            }
            context.SaveChanges();
        }
    }
}
