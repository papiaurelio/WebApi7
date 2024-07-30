using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi7.Datos;
using WebApi7.Models;
using WebApi7.Models.DTO;

namespace WebApi7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;

        private readonly ApplicationDbContext _context;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Obteniendo informacion");
            return Ok(_context.Villas.ToList());
        }

        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error en el id");
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            var villa = _context.Villas.FirstOrDefault(x => x.Id == id); 

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CrearVilla([FromBody] VillaDTO villaDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(_context.Villas.FirstOrDefault(x => x.Nombre.ToLower() == villaDTO.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa ya existe.");
                return BadRequest(ModelState);
            }

            if(villaDTO == null)
            {
                return BadRequest();
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa modelo = new Villa
            {
                Nombre = villaDTO.Nombre,
                Detalle = villaDTO.Detalle,
                ImagenUrl = villaDTO.ImagenUrl,
                Amenidad = villaDTO.Amenidad,
                Tarifa = villaDTO.Tarifa,
                Ocupantes = villaDTO.Ocupantes,
                MetrosCuadrados = villaDTO.MetrosCuadrados

            };

            _context.Villas.Add(modelo);
            _context.SaveChanges();
            return CreatedAtRoute("GetVilla", new {id= villaDTO.Id}, villaDTO);
        }

        [HttpDelete("id",Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }

            var villa = _context.Villas.FirstOrDefault(x => x.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            _context.Villas.Remove(villa);
            _context.SaveChanges();

            //Un delete simepre retorna un NoContent
            return NoContent();
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaActualizada) 
        {
            if(villaActualizada == null || id != villaActualizada.Id)
            {
                return BadRequest();
            }


            var villa = _context.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if(villa == null)
            {
                return NotFound("No existe el id: " + id);
            }

            Villa modelo = new Villa()
            {
                Id = villaActualizada.Id,
                Nombre = villaActualizada.Nombre,
                Detalle = villaActualizada.Detalle,
                ImagenUrl = villaActualizada.ImagenUrl,
                Ocupantes = villaActualizada.Ocupantes,
                Tarifa = villaActualizada.Tarifa,
                MetrosCuadrados = villaActualizada.MetrosCuadrados
            }; 

            _context.Villas.Update(modelo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
