using Demo.WebApp.Classes;

namespace Demo.WebApp.Components.Pages
{
    public partial class Test : UnqPageBase
    {
        #region properties
        private string? searchText;
        private string[]? searchResult;
        #endregion properties

        #region parameters
        #endregion parameters

        #region data
        #endregion data

        #region lifecycle
        #endregion lifecycle

        #region event handlers
        #endregion event handlers

        #region private

        private async Task PerformSearch()
        {
            await Task.Run( () => 
            {
                List<string> data = new List<string> { "Gary", "Diana", "Ana", "Kameryn", "Daily", "Knightly" };
                searchResult = data.Where(e=>e.Contains(searchText)).ToArray();
            });
        }

        
        #endregion private

        #region public
        #endregion public
    }
}
