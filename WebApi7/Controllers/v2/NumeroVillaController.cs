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
using WebApi7.Repositorio.IRepositorio;

namespace WebApi7.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
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
        public IEnumerable<string> GetApiV2()
        {
            return new string[] { "valor1", "valor2" };
        }

    }
}
