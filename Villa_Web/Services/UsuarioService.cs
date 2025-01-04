using Villa_Utilidades;
using Villa_Web.Models.DTO;
using Villa_Web.Services.IServices;

namespace Villa_Web.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        IHttpClientFactory _httpClient;
        private string _villaUrl;


        public UsuarioService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;

            _villaUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }
        public Task<T> Login<T>(LoginRequestDto loginDto)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                TipoApi = DS.TipoApi.POST,
                Datos = loginDto,
                Url = _villaUrl + "/api/usuario/login",
            });
        }

        public Task<T> Registrar<T>(RegistroRequestDto registroDto)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                TipoApi = DS.TipoApi.POST,
                Datos = registroDto,
                Url = _villaUrl + "/api/usuario/registro",
            });
        }
    }
}
