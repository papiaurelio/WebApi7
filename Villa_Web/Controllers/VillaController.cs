using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_Web.Models;
using Villa_Web.Services.IServices;

namespace Villa_Web.Controllers
{
    public class VillaController : Controller
    {

        private readonly IViallService _viallService;
        private readonly IMapper _mapper;

        public VillaController(IViallService viallService, IMapper mapper)
        {
            _mapper = mapper;
            _viallService = viallService;
        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> villaList = new();

            var response = await _viallService.ObtenerTodos<APIResponse>();

            if (response != null && response.IsExitoso)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Resultado));
            }
            return View(villaList);
        }

        //get
        public async Task<IActionResult> CrearVilla()
        {
            return View(); //retorna la misma funcion
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Evitar ataques CSRF
        public async Task<IActionResult> CrearVilla(CrearVillaDto modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _viallService.Crear<APIResponse>(modelo);

                if (response != null && response.IsExitoso == true) 
                { 
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            return View(modelo);
        }
    }
}
