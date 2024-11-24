using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.Domain.Models;
using Demo.Domain.Security;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Demo.Infrastructure.Services
{
    public class WebAPIRepository(IConfigurationManager configuration) : IWebAPIRepository
    {
        #region properties

        IConfigurationManager _configuration {get; set;} = configuration;

        #endregion properties

        #region methods

        public async Task<T?> GetData<T>(string apiKey) where T : class
        {
            string? baseUri = this._configuration.GetSection($"WebServiceURLs:{apiKey}:BaseUri").Value;
            string? path    = this._configuration.GetSection($"WebServiceURLs:{apiKey}:UrlPath").Value;

            if (string.IsNullOrEmpty(baseUri) || string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"WebServiceURL for key {apiKey} was not found.");
            }
            else
            {
                HttpClient client = new();
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.NotFound:
                            throw new Exception($"{response.StatusCode} :: Url {response.ReasonPhrase} - {baseUri}{path}");
                    }
                }

                return default(T);
            }
        }

        public async Task<WebServiceResponse> PostData<RetType, ArgType>(string apiKey, ArgType postData) where RetType : class
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;

            string? baseUri = this._configuration.GetSection($"WebServiceURLs:{apiKey}:BaseUri").Value;
            string? path    = this._configuration.GetSection($"WebServiceURLs:{apiKey}:UrlPath").Value;

            if (string.IsNullOrEmpty(baseUri) || string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"WebServiceURL for key {apiKey} was not found.");
            }
            else
            {
                HttpClient client = new();
                string apiUrl = $"{baseUri}{path}";

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
                    RetType? ret = System.Text.Json.JsonSerializer.Deserialize<RetType>(jsonResult);

                    returnPayload = new WebServiceResponse(System.Net.HttpStatusCode.OK, ret);
                }

                // Otherwise, populate the returnPayload with failure StatusCode
                else
                {
                    returnPayload = new WebServiceResponse(webApiResponse.StatusCode, null);
                }

                return returnPayload;
            }
        }

        #endregion methods
    }
}
