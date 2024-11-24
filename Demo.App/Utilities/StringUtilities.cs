
namespace Demo.App.Utilities
{
    public static class StringUtilities
    {
        #region public

        /// <summary>
        /// Returns a flag indicating whether the string is null, empty, or whitespace.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUndefined(string? value = null)
        {
            return (String.IsNullOrEmpty(value) && String.IsNullOrWhiteSpace(value));
        }

        #endregion public
    }
}
