using DeliveryAPI.Models;

namespace DeliveryAPI.Repository.IRepository
{
    public interface IVehiculoRepository
    {
        ICollection<Vehiculo> GetVehiculos();
        Vehiculo GetVehiculo(int idVehiculo);
        bool VehiculoExistsByConductor(string conductor);
        bool VehiculoExists(int idVehiculo);
        bool CreateVehiculo(Vehiculo vehiculo);
        bool UpdateVehiculo(Vehiculo vehiculo);
        bool DeleteVehiculo(Vehiculo vehiculo);
        bool Save();
    }
}
