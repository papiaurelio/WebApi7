using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi7.Models;
using WebApi7.Models.DTO;
using WebApi7.Repositorio.IRepositorio;

namespace WebApi7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private APIResponse _apiResponse;
        public UsuarioController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _apiResponse = new();
        }

        [HttpPost("login")]  //  api/usuario/login
        public async Task<IActionResult> Login([FromBody] LoginRequestDto modelo)
        {
            var loginResponse = await _usuarioRepo.Login(modelo);

            if (loginResponse.Usuario == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages.Add("UserName o password incorrectos");

                return BadRequest(_apiResponse);
            }

            _apiResponse.IsExitoso = true;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Resultado = loginResponse;
            return Ok(_apiResponse);

        }

        [HttpPost("registro")]  //  api/usuario/registro
        public async Task<IActionResult> Registro([FromBody] RegistroRequestDto modelo)
        {
            bool boolIsUsuarioUnico = _usuarioRepo.IsUsuarioUnico(modelo.UserName);
            
            if (!boolIsUsuarioUnico)
            {
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages.Add("El Username ya existe.");

                return BadRequest(_apiResponse);
            }

            var usuarioNuevo = await _usuarioRepo.Registrar(modelo);

            if (usuarioNuevo == null)
            {

                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages.Add("Hubo un error.");

                return BadRequest(_apiResponse);
            }

            _apiResponse.IsExitoso = true;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_apiResponse);

        }


    }
}
