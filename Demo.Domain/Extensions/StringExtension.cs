using System.Text.RegularExpressions;

namespace Demo.Domain.Extensions
{
    public static class StringExtension
    {

        /// <summary>
        /// Removes the alpha characters from the string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String RemoveAlpha(this String str)
        {
            string ret = Regex.Replace(str, "[^0-9.]", "");
            return ret;
        }

        /// <summary>
        /// Removes the numeric digitsfrom the string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String RemoveNumeric(this String str)
        {
            String ret = Regex.Replace(str, @"[\d-]", string.Empty);
            return ret;
        }
    }
}
