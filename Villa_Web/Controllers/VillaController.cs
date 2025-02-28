﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_Utilidades;
using Villa_Web.Models;
using Villa_Web.Services.IServices;

namespace Villa_Web.Controllers
{
    public class VillaController : Controller
    {

        private readonly IVillaService _viallService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService viallService, IMapper mapper)
        {
            _mapper = mapper;
            _viallService = viallService;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> villaList = new();

            var response = await _viallService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Resultado));
            }
            return View(villaList);
        }

        //get
        [Authorize(Roles = "admin")]
        public IActionResult CrearVilla()
        {
            return View(); //retorna la misma funcion
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Evitar ataques CSRF
        public async Task<IActionResult> CrearVilla(CrearVillaDto modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _viallService.Crear<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));

                if (response != null && response.IsExitoso == true) 
                {
                    TempData["exitoso"] = "Villa agregada exitosamente.";
                    return RedirectToAction(nameof(IndexVilla));
                }

                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            TempData["error"] = "Hubo un error.";
            return View(modelo);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ActualizarVilla(int villaId) //Se utiliza asp-route-villaId en el index para trasladar id a otras vistas
        {
            var response = await _viallService.Obtener<APIResponse>(villaId, HttpContext.Session.GetString(DS.SessionToken));

            if(response != null &&  response.IsExitoso == true)
            {
                VillaDTO modelo = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Resultado));
                return View(_mapper.Map<ActualizarVillaDto>(modelo));
            }

            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //Evitar ataques CSRF
        public async Task<IActionResult> ActualizarVilla(ActualizarVillaDto modelo)
        {
            if(ModelState.IsValid)
            {
                var response = await _viallService.Actualizar<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));
                if(response != null && response.IsExitoso == true)
                {
                    TempData["exitoso"] = "Villa actualizada exitosamente.";
                    return RedirectToAction(nameof(IndexVilla));
                }

                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            TempData["error"] = "Hubo un error.";
            return View(modelo);
        }


        //Get
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoverVilla(int villaId) //Se utiliza asp-route-villaId en el index para trasladar id a otras vistas
        {
            var response = await _viallService.Obtener<APIResponse>(villaId, HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso == true)
            {
                VillaDTO modelo = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Resultado));
                return View(modelo);
            }

            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //Evitar ataques CSRF
        public async Task<IActionResult> RemoverVilla(VillaDTO modelo)
        {

            var response = await _viallService.Remover<APIResponse>(modelo.Id, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso == true)
            {
                TempData["exitoso"] = "Villa removida exitosamente.";
                return RedirectToAction(nameof(IndexVilla));
            }

            TempData["error"] = "Hubo un error.";
            return View(modelo);
        }


    }
}
