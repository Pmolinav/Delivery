using AutoMapper;
using DeliveryAPI.Models;
using DeliveryAPI.Models.DTOs;
using DeliveryAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using static DeliveryAPI.Enums.DeliveryEnums;
using static DeliveryAPI.Models.Pedido;

namespace DeliveryAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PedidoController : Controller
    {
        private IPedidoRepository _pedidoRepo;
        private IVehiculoRepository _vehiculoRepo;
        private readonly IMapper _pedidoMapper;

        public PedidoController(IPedidoRepository pRepo, IVehiculoRepository vRepo, IMapper mapper)
        {
            _pedidoRepo = pRepo;
            _vehiculoRepo = vRepo;
            _pedidoMapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PedidoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPedidos()
        {
            var objList = _pedidoRepo.GetPedidos();

            var objDTO = new List<PedidoDTO>();

            foreach (var obj in objList)
            {
                objDTO.Add(_pedidoMapper.Map<PedidoDTO>(obj));
            }

            return Ok(objDTO);
        }

        [HttpGet("[action]/{idVehiculo:int}", Name = "GetPedidosInVehiculo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PedidoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPedidosInVehiculo(int idVehiculo)
        {
            var objList = _pedidoRepo.GetPedidosInVehiculo(idVehiculo);

            if (objList == null)
            {
                return NotFound();
            }
            else if (objList.Count == 0)
            {
                return NotFound();
            }

            var objDTO = new List<PedidoDTO>();

            foreach (var obj in objList)
            {
                objDTO.Add(_pedidoMapper.Map<PedidoDTO>(obj));
            }

            return Ok(objDTO);
        }

        [HttpGet("{idPedido:int}", Name = "GetPedido")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PedidoDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetPedido(int idPedido)
        {
            var obj = _pedidoRepo.GetPedido(idPedido);
            if (obj == null)
            {
                return NotFound();
            }
            var objDTO = _pedidoMapper.Map<PedidoDTO>(obj);
            return Ok(objDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PedidoDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePedido([FromBody] PedidoCreateDTO pedidoCreateDTO)
        {
            if (pedidoCreateDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (pedidoCreateDTO.Titulo == null)
            {
                ModelState.AddModelError("", "El título del pedido no puede ser nulo.");
                return StatusCode(500, ModelState);
            }
            else if (_pedidoRepo.PedidoExistsByTitulo(pedidoCreateDTO.Titulo))
            {
                ModelState.AddModelError("", "Este título de pedido ya existe.");
                return StatusCode(404, ModelState);
            }

            if (!_vehiculoRepo.VehiculoExists(pedidoCreateDTO.VehiculoId))
            {
                ModelState.AddModelError("", "No existe ningún vehículo con el Id indicado.");
                return StatusCode(404, ModelState);
            }
            else if ((int) pedidoCreateDTO.Urgencia < 0 || (int) pedidoCreateDTO.Urgencia > 3)
            {
                ModelState.AddModelError("", "La urgencia del pedido debe encontrarse entre 0 y 3.");
                return StatusCode(404, ModelState);
            }

            var pedidoObj = _pedidoMapper.Map<Pedido>(pedidoCreateDTO);

            if (!_pedidoRepo.CreatePedido(pedidoObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al guardar el pedido con título: {pedidoObj.Titulo}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPedido", new { idPedido = pedidoObj.Id }, pedidoCreateDTO);
        }

        [HttpPatch("{idPedido:int}", Name = "UpdatePedido")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePedido(int idPedido, [FromBody] PedidoUpdateDTO pedidoUpdateDTO)
        {
            if (pedidoUpdateDTO == null || idPedido != pedidoUpdateDTO.Id)
            {
                ModelState.AddModelError("", "El Id de pedido no coincide.");
                return BadRequest(ModelState);
            }

            if (!_pedidoRepo.PedidoExists(idPedido))
            {
                return NotFound();
            }

            if (!_vehiculoRepo.VehiculoExists(pedidoUpdateDTO.VehiculoId))
            {
                ModelState.AddModelError("", "No existe ningún vehículo con el Id indicado.");
                return StatusCode(404, ModelState);
            }
            else if ((int)pedidoUpdateDTO.Urgencia < 0 || (int)pedidoUpdateDTO.Urgencia > 3)
            {
                ModelState.AddModelError("", "La urgencia del pedido debe encontrarse entre 0 y 3.");
                return StatusCode(404, ModelState);
            }

            var pedidoObj = _pedidoMapper.Map<Pedido>(pedidoUpdateDTO);

            if (!_pedidoRepo.UpdatePedido(pedidoObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al actualizar el pedido con título: {pedidoObj.Titulo}");
                return StatusCode(500, ModelState);
            }

            string result = "Pedido con Id = " + pedidoObj.Id + " actualizado correctamente";
            return Ok(result);
        }

        [HttpDelete("{idPedido:int}", Name = "DeletePedido")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePedido(int idPedido)
        {
            if (!_pedidoRepo.PedidoExists(idPedido))
            {
                return NotFound();
            }

            var pedidoObj = _pedidoRepo.GetPedido(idPedido);

            if (!_pedidoRepo.DeletePedido(pedidoObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al eliminar el Pedido con Id = {pedidoObj.Id}");
                return StatusCode(500, ModelState);
            }
            string result = "Pedido con Id = " + pedidoObj.Id + " borrado correctamente";
            return Ok(result);
        }
    }
}
