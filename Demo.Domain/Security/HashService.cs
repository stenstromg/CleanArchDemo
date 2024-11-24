using System.Security.Cryptography;
using System.Text;

namespace Demo.Domain.Security
{
    public class HashService
    {
        /// <summary>
        /// Compute an MD5 hash for the specified input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ComputeMD5Hash(String input)
        {
            // step 1, calculate MD5 hash from input
            //
            //MD5 md5             = MD5.Create();
            //byte[] hash         = md5.ComputeHash(inputBytes);
            byte[] inputBytes   = Encoding.ASCII.GetBytes(input);
            byte[] hash         = MD5.Create().ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            //
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calculate the SHA2Hash for a string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ComputeSHA2Hash(String input)
        {
            //
            // Calculate the hash
            //SHA512 sha  = SHA512.Create();
            //byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = SHA512.Create().ComputeHash(inputBytes);
            //
            // translate to string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();

        }
    }
}
