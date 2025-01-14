using static Villa_Utilidades.DS;

namespace Villa_Web.Models
{
    public class APIRequest
    {
        public TipoApi TipoApi { get; set; } = TipoApi.GET;

        public string Url { get; set; }

        public object Datos { get; set; }

        public string Token { get; set; }

        public Parametros Parametros { get; set; }
    }

    public class Parametros
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
