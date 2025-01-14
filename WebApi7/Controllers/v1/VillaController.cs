using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi7.Datos;
using WebApi7.Models;
using WebApi7.Models.DTO;
using WebApi7.Models.Especificaciones;
using WebApi7.Repositorio.IRepositorio;

namespace WebApi7.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepositorio _villaRepositorio;
        private readonly IMapper _mapper;

        protected APIResponse _apiResponse;

        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepositoriop, IMapper mapper)
        {
            _logger = logger;
            _villaRepositorio = villaRepositoriop;
            _mapper = mapper;

            _apiResponse = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 30)]  //Agregando caching
        [Authorize]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {

            try
            {
                _logger.LogInformation("Obteniendo informacion");
                IEnumerable<Villa> villasList = await _villaRepositorio.ObtenerTodos();

                _apiResponse.Resultado = _mapper.Map<IEnumerable<VillaDTO>>(villasList);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages = new List<string>()
                {
                    ex.Message
                };

            }

            return _apiResponse;
        }

        [HttpGet("VillasPaginado")]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 30)] 
        public ActionResult<APIResponse> GetVillaPaginado([FromQuery] Parametros parametros) 
        {
            try 
	        {
                var villaList = _villaRepositorio.ObtenerTodosPaginado(parametros);
                _apiResponse.Resultado = _mapper.Map<IEnumerable<VillaDTO>>(villaList);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.TotalPaginas = villaList.Metadata.TotalPages;

                return Ok(_apiResponse);
	        }
            catch (Exception ex)
            {
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages = new List<string>()
                {
                    ex.Message
                };

            }

            return _apiResponse;
        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]

        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error en el id");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }

                var villa = await _villaRepositorio.Obtener(x => x.Id == id);

                if (villa == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsExitoso = false;
                    return NotFound(_apiResponse);
                }

                _apiResponse.Resultado = _mapper.Map<VillaDTO>(villa);
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages = new List<string>()
                {
                    ex.Message
                };
            }

            return _apiResponse;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> CrearVilla([FromBody] CrearVillaDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(ModelState);
                }

                if (await _villaRepositorio.Obtener(x => x.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "La villa ya existe.");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                Villa modelo = _mapper.Map<Villa>(createDto);
                modelo.FechaActualizacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _villaRepositorio.Crear(modelo);
                _apiResponse.Resultado = modelo;
                _apiResponse.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetVilla", new { id = modelo.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages = new List<string>()
                {
                    ex.Message
                };
            }

            return _apiResponse;
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteVilla(int id)
        {

            try
            {
                if (id == 0)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }

                var villa = await _villaRepositorio.Obtener(x => x.Id == id);

                if (villa == null)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }

                await _villaRepositorio.Remover(villa);


                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.Message };
            }

            return BadRequest(_apiResponse);

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] ActualizarVillaDto villaActualizada)
        {
            try
            {
                if (villaActualizada == null || id != villaActualizada.Id)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }


                var villa = await _villaRepositorio.Obtener(x => x.Id == id, tracked: false);

                if (villa == null)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound("No existe el id: " + id);
                }

                Villa modelo = _mapper.Map<Villa>(villaActualizada);

                await _villaRepositorio.Actualizar(modelo);

                _apiResponse.StatusCode = HttpStatusCode.NoContent;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.Message };
            }

            return BadRequest(_apiResponse);
        }
    }
}
