using AutoMapper;
using DeliveryAPI.Models;
using DeliveryAPI.Models.DTOs;
using DeliveryAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PedidoController : Controller
    {
        private IPedidoRepository _pedidoRepo;
        private readonly IMapper _pedidoMapper;

        public PedidoController(IPedidoRepository pRepo, IMapper mapper)
        {
            _pedidoRepo = pRepo;
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

        [HttpGet("[action]/{vhId:int}", Name = "GetPedidosInVehiculo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PedidoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPedidosInVehiculo(int vhId)
        {
            var objList = _pedidoRepo.GetPedidosInVehiculo(vhId);

            if (objList == null)
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

            if (_pedidoRepo.PedidoExistsByTitulo(pedidoCreateDTO.Titulo))
            {
                ModelState.AddModelError("", "¡Este título de pedido ya existe!");
                return StatusCode(404, ModelState);
            }

            var pedidoObj = _pedidoMapper.Map<Pedido>(pedidoCreateDTO);

            if (!_pedidoRepo.CreatePedido(pedidoObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al guardar el pedido con título: {pedidoObj.Titulo}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPedido", new { idPedido = pedidoObj.Id }, pedidoObj);
        }

        [HttpPatch("{idPedido:int}", Name = "UpdatePedido")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePedido(int idPedido, [FromBody] PedidoUpdateDTO pedidolUpdateDTO)
        {
            if (pedidolUpdateDTO == null || idPedido != pedidolUpdateDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var pedidoObj = _pedidoMapper.Map<Pedido>(pedidolUpdateDTO);

            if (!_pedidoRepo.UpdatePedido(pedidoObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al actualizar el pedido con título: {pedidoObj.Titulo}");
                return StatusCode(500, ModelState);
            }

            return Ok(pedidoObj);
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
