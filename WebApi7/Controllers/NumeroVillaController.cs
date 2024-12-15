using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi7.Datos;
using WebApi7.Models;
using WebApi7.Models.DTO;
using WebApi7.Repositorio.IRepositorio;

namespace WebApi7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepositorio;
        private readonly INumeroVillaRepositorio _numeroVillaRepositorio;
        private readonly IMapper _mapper;

        protected APIResponse _apiResponse;

        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepositoriop, IMapper mapper, INumeroVillaRepositorio numeroVillaRepositorio)
        {
            _logger = logger;
            _villaRepositorio = villaRepositoriop;
            _numeroVillaRepositorio = numeroVillaRepositorio;
            _mapper = mapper;

            _apiResponse = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {

            try
            {
                _logger.LogInformation("Obteniendo informacion");
                IEnumerable<NumeroVilla> numeroVillasList = await _numeroVillaRepositorio.ObtenerTodos(incluirPropiedades: "Villa");

                _apiResponse.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillasList);
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

        [HttpGet("{id:int}", Name = "GetNumeroVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error en el id");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }

                var numeroVilla = await _numeroVillaRepositorio.Obtener(x => x.NoVilla == id, incluirPropiedades: "Villa");

                if (numeroVilla == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsExitoso= false;
                    return NotFound(_apiResponse);
                }

                _apiResponse.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
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
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] CrearNumeroVillaDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(ModelState);
                }

                if (await _numeroVillaRepositorio.Obtener(x => x.NoVilla == createDto.NoVilla) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El numero de villa ya existe.");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(ModelState);
                }

                if (await _villaRepositorio.Obtener(v => v.Id == createDto.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hay un error con la villa principal.");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);
                modelo.FechaActualizacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _numeroVillaRepositorio.Crear(modelo);
                _apiResponse.Resultado = modelo;
                _apiResponse.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.NoVilla }, _apiResponse);
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

        [HttpDelete("{id:int}", Name = "DeleteNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {

            try
            {
                if (id == 0)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }

                var numeroVilla = await _numeroVillaRepositorio.Obtener(x => x.NoVilla == id);

                if (numeroVilla == null)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }

                await _numeroVillaRepositorio.Remover(numeroVilla);


                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsExitoso =false;
                _apiResponse.ErrorMessages = new List<string>() { ex.Message };
            }

            return BadRequest(_apiResponse);
           
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] ActualizarNumeroVillaDto nVillaActualizada) 
        {
            try
            {
                if (nVillaActualizada == null || id != nVillaActualizada.NoVilla)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }

                if (await _villaRepositorio.Obtener(v => v.Id == nVillaActualizada.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Hay un error con la villa principal.");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(ModelState);
                }


                var numeroVilla = await _numeroVillaRepositorio.Obtener(x => x.NoVilla == id, tracked: false);

                if (numeroVilla == null)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound("El numero de villa no existe. " + id);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(nVillaActualizada);

                await _numeroVillaRepositorio.Actualizar(modelo);

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
