using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi7.Models;
using WebApi7.Services;

namespace WebApi7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ExternalApiServices _externalApiService;

        public PostController(ExternalApiServices externalApiService)
        {
            _externalApiService = externalApiService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            // Obtener la lista de publicaciones desde la API externa
            var posts = await _externalApiService.GetPostsAsync();

            // Retornar la lista de publicaciones
            return Ok(posts);
        }
    }
}
