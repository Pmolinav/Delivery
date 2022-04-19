﻿using DeliveryAPI.Data;
using DeliveryAPI.Models;
using DeliveryAPI.Repository.IRepository;

namespace DeliveryAPI.Repository
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly ApplicationDbContext _db;

        public VehiculoRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateVehiculo(Vehiculo vehiculo)
        {
            _db.Vehiculos.Add(vehiculo);
            return Save();
        }

        public bool DeleteVehiculo(Vehiculo vehiculo)
        {
            _db.Vehiculos.Remove(vehiculo);
            return Save();
        }

        public Vehiculo GetVehiculo(int idVehiculo)
        {
            Vehiculo result = new Vehiculo();
            result = _db.Vehiculos.FirstOrDefault(a => a.Id == idVehiculo);
            return result;
        }

        public ICollection<Vehiculo> GetVehiculos()
        {
            ICollection<Vehiculo> result = new List<Vehiculo>();
            result = _db.Vehiculos.OrderBy(a => a.Id).ToList();
            return result;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateVehiculo(Vehiculo vehiculo)
        {
            _db.Vehiculos.Update(vehiculo);
            return Save();
        }

        public bool VehiculoExists(int idVehiculo)
        {
            return _db.Vehiculos.Any(a => a.Id == idVehiculo);
        }

        public bool VehiculoExistsByDireccion(string direccion)
        {
            bool result = _db.Vehiculos.Any(a => a.Direccion.ToLower().Trim() == direccion.ToLower().Trim());
            return result;
        }
    }
}