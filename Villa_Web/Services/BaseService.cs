﻿ 
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
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

                if(apiRequest.Parametros == null)
                {
                    message.RequestUri = new Uri(apiRequest.Url);
                }
                else  //agregar paginacion si los parametros estan llenos.
                {
                    var builder  = new UriBuilder(apiRequest.Url);
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["PageNumber"] = apiRequest.Parametros.PageNumber.ToString();
                    query["PageSize"] = apiRequest.Parametros.PageSize.ToString();
                    builder.Query = query.ToString();
                    string url = builder.ToString();
                    message.RequestUri = new Uri(url);
                }
                

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

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }
                apiResponse = await client.SendAsync(message);

                var apiContet = await apiResponse.Content.ReadAsStringAsync();
             
                try
                {
                    APIResponse response = JsonConvert.DeserializeObject<APIResponse>(apiContet);
                    if (response != null && (apiResponse.StatusCode == HttpStatusCode.BadRequest 
                        || apiResponse.StatusCode == HttpStatusCode.NotFound))
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.IsExitoso = false;
                        var res = JsonConvert.SerializeObject(response);
                        var obj = JsonConvert.DeserializeObject<T>(res);

                        return obj;
                    }
                }
                catch (Exception)
                {

                    var errorResponse = JsonConvert.DeserializeObject<T>(apiContet);
                    return errorResponse;
                }

                var APIResponse = JsonConvert.DeserializeObject<T>(apiContet);
                return APIResponse;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsExitoso = false
                };

                var res = JsonConvert.SerializeObject(dto);
                var responseEx = JsonConvert.DeserializeObject<T>(res);

                return responseEx;
            }
        }
    }
}
