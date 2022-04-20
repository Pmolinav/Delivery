using DeliveryAPI.Data;
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
            vehiculo.CreationDate = DateTime.Now;
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
            try
            {
                return _db.SaveChanges() >= 0 ? true : false;
            } 
            catch(Exception e)
            {
                return false;
            }
        }

        public bool UpdateVehiculo(Vehiculo vehiculo)
        {
            if (vehiculo.Direccion == null || vehiculo.Conductor == null)
            {
                return false;
            }
            var vehiculo1 = _db.Vehiculos.First(a => a.Id == vehiculo.Id);
            vehiculo1.Direccion = vehiculo.Direccion;
            vehiculo1.Conductor = vehiculo.Conductor;
            vehiculo1.Longitud = vehiculo.Longitud;
            vehiculo1.Latitud = vehiculo.Latitud;
            vehiculo1.RevisionDate = DateTime.Now;

            return Save();
        }

        public bool VehiculoExists(int idVehiculo)
        {
            return _db.Vehiculos.Any(a => a.Id == idVehiculo);
        }

        public bool VehiculoExistsByConductor(string conductor)
        {
            if (conductor != null)
            {
                bool result = _db.Vehiculos.Any(a => a.Conductor.ToLower().Trim() == conductor.ToLower().Trim());
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
