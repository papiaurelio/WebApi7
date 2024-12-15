using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;
using Villa_Web.Models;
using Villa_Web.Models.ViewModel;
using Villa_Web.Services.IServices;

namespace Villa_Web.Controllers
{
    public class NumeroVillaController : Controller
    {
        private readonly INumeroVillaService _numeroVillaService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public NumeroVillaController(INumeroVillaService numeroVillaService, IMapper mapper, IVillaService villaService)
        {
            _numeroVillaService = numeroVillaService;
            _mapper = mapper;
            _villaService = villaService;
        }
        public async Task<IActionResult> IndexNumeroVilla()
        {
            List<NumeroVillaDto> numeroVillasList = new();
            var response = await _numeroVillaService.ObtenerTodos<APIResponse>();

            if (response != null && response.IsExitoso) 
            {
                numeroVillasList = JsonConvert.DeserializeObject<List<NumeroVillaDto>>(Convert.ToString(response.Resultado));
            }
            return View(numeroVillasList);
        }


        //get
        public async Task<IActionResult> CrearNumeroVilla()
        {
            NumeroVillaViewModel numeroVillaM = new();
            var response = await _villaService.ObtenerTodos<APIResponse>();

            if (response != null && response.IsExitoso)
            {
                numeroVillaM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Resultado))
                                         .Select(v => new SelectListItem
                                         {
                                            Text = v.Nombre,
                                            Value = v.Id.ToString()
                                         });
            }

            return View(numeroVillaM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearNumeroVilla(NumeroVillaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Crear<APIResponse>(model.NumeroVilla);

                if(response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Numero de villa creada.";
                    return RedirectToAction(nameof(IndexNumeroVilla));
                }

                else
                {
                    if(response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _villaService.ObtenerTodos<APIResponse>();

            if (res != null && res.IsExitoso)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Resultado))
                                         .Select(v => new SelectListItem
                                         {
                                             Text = v.Nombre,
                                             Value = v.Id.ToString()
                                         });
            }

            TempData["error"] = "Hubo un error";
            return View(model);
        }



        //get 
        public async Task<IActionResult> ActualizarNumeroVilla(int villaNo)
        {
            NumeroVilllaActualizarViewModel numeroVillaVM = new();

            var response = await _numeroVillaService.Obtener<APIResponse>(villaNo);
            if(response != null && response.IsExitoso)
            {
                NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                numeroVillaVM.NumeroVilla = _mapper.Map<ActualizarNumeroVillaDto>(modelo);

                var res = await _villaService.ObtenerTodos<APIResponse>();

                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Resultado))
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.Nombre,
                                                Value = v.Id.ToString()
                                            });

                return View(numeroVillaVM);
            }

            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarNumeroVilla(NumeroVilllaActualizarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Actualizar<APIResponse>(model.NumeroVilla);

                if (response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Numero de villa actualizada.";
                    return RedirectToAction(nameof(IndexNumeroVilla));
                }

                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _villaService.ObtenerTodos<APIResponse>();

            if (res != null && res.IsExitoso)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Resultado))
                                         .Select(v => new SelectListItem
                                         {
                                             Text = v.Nombre,
                                             Value = v.Id.ToString()
                                         });
            }

            TempData["error"] = "Hubo un error";
            return View(model);
        }

        //get
        public async Task<IActionResult> RemoverNumeroVilla(int villaNo)
        {
            NumeroVilllaRemoverViewModel numeroVillaVM = new();

            var response = await _numeroVillaService.Obtener<APIResponse>(villaNo);
            if (response != null && response.IsExitoso)
            {
                NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                numeroVillaVM.NumeroVilla = modelo; //no se mapea por que ya es del mismo tipo

                var res = await _villaService.ObtenerTodos<APIResponse>();

                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Resultado))
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.Nombre,
                                                Value = v.Id.ToString()
                                            });

                return View(numeroVillaVM);
            }

            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverNumeroVilla(NumeroVilllaRemoverViewModel modelo)
        {
            var response =  await _numeroVillaService.Remover<APIResponse>(modelo.NumeroVilla.NoVilla);

            if(response != null && response.IsExitoso)
            {
                TempData["exitoso"] = "Numero de villa eliminado exitosamente.";
                return RedirectToAction(nameof(IndexNumeroVilla));
            }

            TempData["error"] = "Hubo un error";
            return View(modelo);
        }
    }
}
