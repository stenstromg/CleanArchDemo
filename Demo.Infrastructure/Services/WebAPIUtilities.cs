using Demo.App.Interfaces.WebAPI;
using Demo.App.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Demo.Infrastructure.Services
{
    /// <summary>
    /// Provides members fro abstracting the details of the GET/POST/PUT/DELETE Http requests.
    /// </summary>
    /// <param name="configuration"></param>
    public class WebAPIUtilities(IConfigurationManager configuration) : IWebAPIUtilities
    {
        #region properties

        IConfigurationManager _configuration {get; set;} = configuration;

        #endregion properties

        #region methods

        public async Task<WebServiceResponse> Delete<TReturn>(string apiUrl)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;

            HttpClient client = new();

            HttpResponseMessage webApiResponse = await client.DeleteAsync( apiUrl, tokenSource.Token );

            // Declare the variable which will return value and status code to the consumer
            //
            WebServiceResponse returnPayload;

            // If the WebService returns a success code then process the return value and ppulate
            // the returnPayload
            //
            if (webApiResponse.IsSuccessStatusCode)
            {
                string jsonResult = await webApiResponse.Content.ReadAsStringAsync(cancellationToken);
                // Use the NewtonSoft JsonConvert instead of the System.Text.Json.JsonSerializer to
                // deserialize the result.
                //
                TReturn? ret = JsonConvert.DeserializeObject<TReturn>(jsonResult);
                returnPayload = new WebServiceResponse(System.Net.HttpStatusCode.OK, ret);
            }

            // Otherwise, populate the returnPayload with failure StatusCode
            else
            {
                returnPayload = new WebServiceResponse(webApiResponse.StatusCode, null, "Error");
            }

            return returnPayload;

        }

        public async Task<WebServiceResponse> Get<TReturn>(string apiUrl)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;

            HttpClient client = new();

            HttpResponseMessage webApiResponse = await client.GetAsync(apiUrl);

            // Declare the variable which will return value and status code to the consumer
            //
            WebServiceResponse returnPayload;

            // If the WebService returns a success code then process the return value and ppulate
            // the returnPayload
            //
            if (webApiResponse.IsSuccessStatusCode)
            {
                string jsonResult = await webApiResponse.Content.ReadAsStringAsync(cancellationToken);
                // Use the NewtonSoft JsonConvert instead of the System.Text.Json.JsonSerializer to
                // deserialize the result.
                //
                TReturn? ret = JsonConvert.DeserializeObject<TReturn>(jsonResult);
                returnPayload = new WebServiceResponse(System.Net.HttpStatusCode.OK, ret);
            }

            // Otherwise, populate the returnPayload with failure StatusCode
            else
            {
                returnPayload = new WebServiceResponse(webApiResponse.StatusCode, null, "Error");
            }

            return returnPayload;

        }

        public async Task<WebServiceResponse> Post<TReturn, TArg>(string apiUrl, TArg postData) where TReturn : class
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;

            HttpClient client = new();

            // Prepare the requestPayload data
            //
            var json = JsonConvert.SerializeObject(postData, new JsonSerializerSettings()
            { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            var requestPayload = new StringContent(json, Encoding.UTF8, "application/json");

            // Get the response from the WebApi web service
            //
            HttpResponseMessage webApiResponse = await client.PostAsync(apiUrl, requestPayload, cancellationToken);

            // Declare the variable which will return value and status code to the consumer
            //
            WebServiceResponse returnPayload;

            // If the WebService returns a success code then process the return value and ppulate
            // the returnPayload
            //
            if (webApiResponse.IsSuccessStatusCode)
            {
                string jsonResult = await webApiResponse.Content.ReadAsStringAsync(cancellationToken);
                // Use the NewtonSoft JsonConvert instead of the System.Text.Json.JsonSerializer to
                // deserialize the result.
                //
                TReturn? ret = JsonConvert.DeserializeObject<TReturn>(jsonResult);

                returnPayload = new WebServiceResponse(System.Net.HttpStatusCode.OK, ret);
            }

            // Otherwise, populate the returnPayload with failure StatusCode
            else
            {
                returnPayload = new WebServiceResponse(webApiResponse.StatusCode, null, "Error");
            }

            return returnPayload;
        }

        #endregion methods
    }
}
