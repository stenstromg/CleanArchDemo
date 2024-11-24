using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Demo.App.Models.DTO
{
    public class WebServiceResponse
    {
        #region properties


        public string?  Message { get; set; }

        public object? Payload { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        #endregion properties

        #region ctor

        public WebServiceResponse(HttpStatusCode statusCode, object? payload, string? message = null)
        {
            this.StatusCode = statusCode;
            this.Payload = payload;
            this.Message = message;
        }

        #endregion ctor
    }
}
