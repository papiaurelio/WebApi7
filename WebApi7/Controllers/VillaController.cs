using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi7.Datos;
using WebApi7.Models;
using WebApi7.Models.DTO;
using WebApi7.Repositorio.IRepositorio;

namespace WebApi7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepositorio _villaRepositorio;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepositoriop, IMapper mapper)
        {
            _logger = logger;
            _villaRepositorio = villaRepositoriop;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            _logger.LogInformation("Obteniendo informacion");
            IEnumerable<Villa> villasList = await _villaRepositorio.ObtenerTodos();
            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villasList));
        }

        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error en el id");
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            var villa = await _villaRepositorio.Obtener(x => x.Id == id); 
             
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CrearVilla([FromBody] CrearVillaDto createDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _villaRepositorio.Obtener(x => x.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa ya existe.");
                return BadRequest(ModelState);
            }

            if(createDto == null)
            {
                return BadRequest();
            }

            Villa modelo = _mapper.Map<Villa>(createDto);

            await _villaRepositorio.Crear(modelo);
            return CreatedAtRoute("GetVilla", new {id= modelo.Id}, modelo);
        }

        [HttpDelete("id",Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }

            var villa = await _villaRepositorio.Obtener(x => x.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            await _villaRepositorio.Remover(villa);

            //Un delete simepre retorna un NoContent
            return NoContent();
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] ActualizarVillaDto villaActualizada) 
        {
            if(villaActualizada == null || id != villaActualizada.Id)
            {
                return BadRequest();
            }


            var villa = await _villaRepositorio.Obtener(x => x.Id == id, tracked:false);

            if(villa == null)
            {
                return NotFound("No existe el id: " + id);
            }

            Villa modelo = _mapper.Map<Villa>(villaActualizada);

            await _villaRepositorio.Actualizar(modelo);
            return NoContent();
        }
    }
}
