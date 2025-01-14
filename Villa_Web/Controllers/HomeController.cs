using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Villa_Utilidades;
using Villa_Web.Models;
using Villa_Web.Models.ViewModel;
using Villa_Web.Services.IServices;

namespace Villa_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IVillaService villaService, IMapper mapper)
        {
            _logger = logger;
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageNumber = 1 )
        {
            List<VillaDTO> villaList = new();

            VillaPaginadoViewModel villaVM = new VillaPaginadoViewModel();

            if (pageNumber < 1) pageNumber = 1;
            var response = await _villaService.ObtenerTodosPaginado<APIResponse>(HttpContext.Session.GetString(DS.SessionToken), pageNumber, 4);

            if (response != null && response.IsExitoso)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Resultado));
                villaVM = new VillaPaginadoViewModel()
                {
                    VillaList = villaList,
                    PagesNumber = pageNumber,
                    TotalPages = JsonConvert.DeserializeObject<int>(Convert.ToString(response.TotalPaginas))
                };

                if (pageNumber > 1) villaVM.Previo = "";
                if (villaVM.TotalPages <= pageNumber) villaVM.Siguiente = "disabled";
            }
            return View(villaVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
