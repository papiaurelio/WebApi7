using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi7.Datos;
using WebApi7.Models;
using WebApi7.Models.DTO;

namespace WebApi7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.villaList;
        }

        [HttpGet("id")]
        public VillaDTO GetVilla(int id)
        {
            return VillaStore.villaList.FirstOrDefault(x =>x.Id == id);
        }
    }
}
