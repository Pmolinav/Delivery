using AutoMapper;
using DeliveryAPI.Models;
using DeliveryAPI.Models.DTOs;
using DeliveryAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAPI.Mapper
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class VehiculoController : Controller
    {
        private IVehiculoRepository _vehiculoRepo;
        private readonly IMapper _mapper;

        public VehiculoController(IVehiculoRepository vhRepo, IMapper mapper)
        {
            _vehiculoRepo = vhRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VehiculoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetVehiculos()
        {
            var objList = _vehiculoRepo.GetVehiculos();

            var objDTO = new List<VehiculoDTO>();

            foreach (var obj in objList)
            {
                objDTO.Add(_mapper.Map<VehiculoDTO>(obj));
            }

            return Ok(objDTO);

        }

        [HttpGet("{idVehiculo:int}", Name = "GetVehiculo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehiculoDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetVehiculo(int idVehiculo)
        {
            var obj = _vehiculoRepo.GetVehiculo(idVehiculo);
            if (obj == null)
            {
                return NotFound();
            }
            var objDTO = _mapper.Map<VehiculoDTO>(obj);
            return Ok(objDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VehiculoCreateDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateVehiculo([FromBody] VehiculoCreateDTO vehiculoCreateDTO)
        {
            if (vehiculoCreateDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_vehiculoRepo.VehiculoExistsByConductor(vehiculoCreateDTO.Conductor))
            {
                ModelState.AddModelError("", "Este conductor es nulo o ya se encuentra en otro vehículo.");
                return StatusCode(404, ModelState);
            }

            var vehiculoObj = _mapper.Map<Vehiculo>(vehiculoCreateDTO);

            if (!_vehiculoRepo.CreateVehiculo(vehiculoObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al guardar el vehiculo con Id = {vehiculoObj.Id}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetVehiculo", new { idVehiculo = vehiculoObj.Id }, vehiculoCreateDTO);

        }

        [HttpPatch("{idVehiculo:int}", Name = "UpdateVehiculo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateVehiculo(int idVehiculo, [FromBody] VehiculoUpdateDTO vehiculoUpdateDTO)
        {
            if (vehiculoUpdateDTO == null || idVehiculo != vehiculoUpdateDTO.Id)
            {
                ModelState.AddModelError("", "El Id de vehículo no coincide.");
                return BadRequest(ModelState);
            }
            else if (!_vehiculoRepo.VehiculoExists(idVehiculo))
            {
                return NotFound();
            }

            var vehiculoObj = _mapper.Map<Vehiculo>(vehiculoUpdateDTO);

            if (!_vehiculoRepo.UpdateVehiculo(vehiculoObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al actualizar el vehiculo con Id = {vehiculoObj.Id}");
                return StatusCode(500, ModelState);
            }

            string result = "Vehiculo con Id = " + vehiculoObj.Id + " actualizado correctamente";
            return Ok(result);
        }

        [HttpDelete("{idVehiculo:int}", Name = "DeleteVehiculo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteVehiculo(int idVehiculo)
        {
            if (!_vehiculoRepo.VehiculoExists(idVehiculo))
            {
                return NotFound();
            }

            var vehiculoObj = _vehiculoRepo.GetVehiculo(idVehiculo);

            if (!_vehiculoRepo.DeleteVehiculo(vehiculoObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al eliminar el vehiculo con Id = {vehiculoObj.Id}");
                return StatusCode(500, ModelState);
            }
            string result = "Vehiculo con Id = " + vehiculoObj.Id + " borrado correctamente";
            return Ok(result);
        }
    }
}
