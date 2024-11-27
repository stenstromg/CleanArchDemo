using System.Net;

namespace Demo.App.Models.DTO
{
    /// <summary>
    /// Carries the Response delivered by a WebAPI Request.
    /// </summary>
    public class WebServiceResponse
    {
        #region properties

        /// <summary>
        /// Get/Sets an error or other message associated with the response
        /// </summary>
        public string?  Message { get; set; }

        /// <summary>
        /// Gets/Sets the return value of the Web Service request.
        /// </summary>
        public object? Payload { get; set; }

        /// <summary>
        /// Gets/Sets the status of the Web Service response.
        /// </summary>
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
