using Villa_Utilidades;
using Villa_Web.Models;
using Villa_Web.Services.IServices;

namespace Villa_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _villaUrl;

        public VillaService(IHttpClientFactory httpClient, IConfiguration config): base(httpClient) 
        {
            _httpClient = httpClient;
            _villaUrl = config.GetValue<string>("ServiceUrls:API_URL");
        }
        public Task<T> Actualizar<T>(ActualizarVillaDto villaDto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.PUT,
                Datos = villaDto,
                Url = _villaUrl + "/api/Villa/"+villaDto.Id,
                Token = token
            });
        }


        public Task<T> Crear<T>(CrearVillaDto villaDto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.POST,
                Datos = villaDto,
                Url = _villaUrl+"/api/Villa",
                Token = token
            });
        }

        public Task<T> Obtener<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.GET,
                Url = _villaUrl + "/api/Villa/"+id,
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.GET,
                Url = _villaUrl + "/api/Villa/",
                Token = token
            });
        }

        public Task<T> Remover<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.DELETE,
                Url = _villaUrl + "/api/Villa/" + id,
                Token = token
            });
        }
    }
}
