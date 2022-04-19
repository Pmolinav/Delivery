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

        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VehiculoDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateVehiculo([FromBody] VehiculoDTO vehiculoDTO)
        {
            if (vehiculoDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_vehiculoRepo.VehiculoExists(vehiculoDTO.Id))
            {
                ModelState.AddModelError("", "Este Parque ya existe!");
                return StatusCode(404, ModelState);
            }

            var vehiculoObj = _mapper.Map<Vehiculo>(vehiculoDTO);

            if (!_vehiculoRepo.CreateVehiculo(vehiculoObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al guardar el vehiculo con Id = {vehiculoObj.Id}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = vehiculoObj.Id }, vehiculoObj);

        }

        [HttpPatch("{idVehiculo:int}", Name = "UpdateVehiculo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateVehiculo(int idVehiculo, [FromBody] VehiculoDTO vehiculoDTO)
        {
            if (vehiculoDTO == null || idVehiculo != vehiculoDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObj = _mapper.Map<Vehiculo>(vehiculoDTO);

            if (!_vehiculoRepo.UpdateVehiculo(nationalParkObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al actualizar el Vehiculo con Id = {nationalParkObj.Id}");
                return StatusCode(500, ModelState);
            }

            return Ok(nationalParkObj);
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

            var nationalParkObj = _vehiculoRepo.GetVehiculo(idVehiculo);

            if (!_vehiculoRepo.DeleteVehiculo(nationalParkObj))
            {
                ModelState.AddModelError("", $"Algo ha fallado al eliminar el vehiculo con Id = {nationalParkObj.Id}");
                return StatusCode(500, ModelState);
            }
            string result = "Vehiculo con Id = " + nationalParkObj.Id + " borrado correctamente";
            return Ok(result);
        }
    }
}
