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


        public HttpStatusCode StatusCode { get; set; }

        public object? Payload { get; set; }


        #endregion properties

        #region ctor

        public WebServiceResponse(HttpStatusCode statusCode, object? payload)
        {
            this.StatusCode = statusCode;
            this.Payload = payload;
        }

        #endregion ctor
    }
}
