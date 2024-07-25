namespace WebApi7.Services
{
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using WebApi7.Models;

    public class ExternalApiServices
    {
        private readonly HttpClient _httpClient;

        public ExternalApiServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            // Realizar la solicitud GET al endpoint externo
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");

            // Lanzar una excepción si la solicitud no fue exitosa
            response.EnsureSuccessStatusCode();

            // Leer el contenido de la respuesta
            var content = await response.Content.ReadAsStringAsync();

            // Deserializar el JSON en una lista de objetos Post
            return JsonSerializer.Deserialize<List<Post>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
