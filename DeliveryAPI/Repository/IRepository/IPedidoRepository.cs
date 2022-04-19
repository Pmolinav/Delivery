using DeliveryAPI.Models;

namespace DeliveryAPI.Repository.IRepository
{
    public interface IPedidoRepository
    {
        ICollection<Pedido> GetPedidos();
        ICollection<Pedido> GetPedidosInVehiculo(int idVehiculo);
        Pedido GetPedido(int idPedido);
        bool PedidoExistsByTitulo(string titulo);
        bool PedidoExists(int idPedido);
        bool CreatePedido(Pedido pedido);
        bool UpdatePedido(Pedido pedido);
        bool DeletePedido(Pedido pedido);
        bool Save();
    }
}
