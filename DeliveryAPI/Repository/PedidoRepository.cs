using DeliveryAPI.Data;
using DeliveryAPI.Models;
using DeliveryAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _db;

        public PedidoRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreatePedido(Pedido pedido)
        {
            pedido.CreationDate = DateTime.Now;
            _db.Pedidos.Add(pedido);
            return Save();
        }

        public bool DeletePedido(Pedido pedido)
        {
            _db.Remove(pedido);
            return Save();
        }

        public Pedido GetPedido(int idPedido)
        {
            return _db.Pedidos.Include(c => c.Vehiculo).FirstOrDefault(a => a.Id == idPedido);
        }

        public ICollection<Pedido> GetPedidos()
        {
            ICollection<Pedido> result = new List<Pedido>();
            result = _db.Pedidos.Include(c => c.Vehiculo).OrderBy(a => a.Id).ToList();
            return result;
        }

        public ICollection<Pedido> GetPedidosInVehiculo(int idVehiculo)
        {
            return _db.Pedidos.Include(c => c.Vehiculo).Where(c => c.VehiculoId == idVehiculo).ToList();
        }

        public bool PedidoExists(int idPedido)
        {
            return _db.Pedidos.Any(a => a.Id == idPedido);
        }

        public bool PedidoExistsByTitulo(string titulo)
        {
            bool result = _db.Pedidos.Any(a => a.Titulo.ToLower().Trim() == titulo.ToLower().Trim());
            return result;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePedido(Pedido pedido)
        {
            if (pedido.Titulo == null)
            {
                return false;
            }

            var pedidoU = _db.Pedidos.First(a => a.Id == pedido.Id);
            pedidoU.Titulo = pedido.Titulo;
            pedidoU.Urgencia = pedido.Urgencia;
            pedidoU.VehiculoId = pedido.VehiculoId;
            pedidoU.RevisionDate = DateTime.Now;

            return Save();
        }
    }
}
