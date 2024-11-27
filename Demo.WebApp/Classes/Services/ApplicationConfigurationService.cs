
using Demo.WebApp.Classes.Utilities;

namespace Demo.WebApp.Classes.Services
{
    public class ApplicationConfigurationService(ConfigurationManager configuration)
    {
        #region properties

        /// <summary>
        /// Gets/Sets the internal refernce to the ConfigurationManager
        /// </summary>
        ConfigurationManager _configuration { get; set; } = configuration;

        /// <summary>
        /// Underlying variable which holds the WebServices and functions configured in appsettings.json
        /// </summary>
        List<WebServicesConfiguration>? _webServices = null;

        /// <summary>
        /// Gets the list of WebServices and functions configured in appsettings.json
        /// </summary>
        public List<WebServicesConfiguration>? WebServices
        {
            get
            {
                if (_webServices == null)
                {
                    _webServices = this.GetWebServicesConfig();
                }

                return _webServices;
            }
        }

        #endregion properties

        #region data
        #endregion data

        #region ctor
        #endregion ctor

        #region private

        /// <summary>
        /// Initializes the configured WebServices nodes.
        /// </summary>
        /// <returns></returns>
        List<WebServicesConfiguration>? GetWebServicesConfig()
        {
            var webServices = this._configuration.GetSection("WebServices").Get<List<WebServicesConfiguration>>();
            return webServices;
        }

        #endregion private

        #region public

        /// <summary>
        /// Returns URL for a specific WebService function
        /// </summary>
        /// <param name="functionName">The name of the function whose URL we want to return</param>
        /// <returns></returns>
        public string? GetWebServiceFunctionURL(string functionName)
        {
            string? ret = null;
            if (this.WebServices != null)
            {
                foreach (var item in this.WebServices)
                {
                    foreach (var function in item.Functions)
                    {
                        if (function.Name.ToLower() == functionName.ToLower())
                        {
                            ret = $"{item.BaseURL}/{function.UrlPath}";
                            break;
                        }
                    }
                }
            }

            return ret;
        }

        #endregion public
    }
}
