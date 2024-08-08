
using Newtonsoft.Json;
using System.Text;
using Villa_Web.Models;
using Villa_Web.Services.IServices;

namespace Villa_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get ; set ; }

        public IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new APIResponse();

            _httpClient = httpClient;

        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("VillaAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Datos != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Datos),
                        Encoding.UTF8, "application/json");
                }

                switch(apiRequest.TipoApi) 
                {
                    case Villa_Utilidades.DS.TipoApi.GET:
                        message.Method = HttpMethod.Get;
                        break;

                    case Villa_Utilidades.DS.TipoApi.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case Villa_Utilidades.DS.TipoApi.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    case Villa_Utilidades.DS.TipoApi.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);

                var apiContet = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContet);

                return APIResponse;
            }
            catch (Exception ex)
            {
                var errors = new APIResponse
                {
                    ErrorMessages = new List<string>()
                    {
                        Convert.ToString(ex.Message)
                    },
                    IsExitoso = false
                };

                var res = JsonConvert.SerializeObject(errors);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);

                return APIResponse;
            }
        }
    }
}
