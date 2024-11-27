
namespace Demo.WebApp.Classes.Utilities
{
    public class WebServicesConfiguration
    {
        #region properties

        public string ServiceName { get; set; }

        public string BaseURL { get; set; }

        public ICollection<WebServicesFunctionConfiguration> Functions { get; set; }

        #endregion properties
    }

    public class WebServicesFunctionConfiguration
    {
        #region properties

        public string Name { get; set; }

        public string UrlPath { get; set; }

        public string RequestType { get; set; }

        #endregion properties
    }
}
