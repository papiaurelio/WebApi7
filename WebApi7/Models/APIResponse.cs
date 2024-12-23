﻿using System.Net;

namespace WebApi7.Models
{
    public class APIResponse
    {

        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsExitoso { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        //El retorno de la api puede ser cualquier tipo de objeto
        public object Resultado { get; set; }
    }
}
