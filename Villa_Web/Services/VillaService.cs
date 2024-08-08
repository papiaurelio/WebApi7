using Villa_Utilidades;
using Villa_Web.Models;
using Villa_Web.Services.IServices;

namespace Villa_Web.Services
{
    public class VillaService : BaseService, IViallService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _villaUrl;

        public VillaService(IHttpClientFactory httpClient, IConfiguration config): base(httpClient) 
        {
            _httpClient = httpClient;
            _villaUrl = config.GetValue<string>("ServiceUrls:API_URL");
        }
        public Task<T> Actualizar<T>(ActualizarNumeroVillaDto villaDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.PUT,
                Datos = villaDto,
                Url = _villaUrl + "/api/Villa/"+villaDto.VillaId
            });
        }

        public Task<T> Crear<T>(CrearVillaDto villaDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.POST,
                Datos = villaDto,
                Url = _villaUrl+"/api/Villa"
            });
        }

        public Task<T> Obtener<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.GET,
                Url = _villaUrl + "/api/Villa/"+id
            });
        }

        public Task<T> ObtenerTodos<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.GET,
                Url = _villaUrl + "/api/Villa/"
            });
        }

        public Task<T> Remover<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                TipoApi = DS.TipoApi.DELETE,
                Url = _villaUrl + "/api/Villa/" + id
            });
        }
    }
}
