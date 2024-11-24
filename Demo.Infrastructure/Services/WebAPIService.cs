using Demo.App.Interfaces;
using Demo.App.Models;
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
    public class WebAPIService(IConfigurationManager configuration) : IWebAPIService
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

        public async Task<RetType?> PostData<RetType, ArgType>(string apiKey, ArgType postData) where RetType : class
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

                // Prepare the payload data
                //
                var json = JsonConvert.SerializeObject(postData, new JsonSerializerSettings()
                                                       {
                                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                       });

                var payload = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, payload, cancellationToken);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync(cancellationToken);
                    RetType? ret = System.Text.Json.JsonSerializer.Deserialize<RetType>(jsonResult);
                    return ret;
                }
                else
                {
                    throw new Exception($"An exception occured posting to {apiUrl}");
                }
            }
        }

        #endregion methods
    }
}
