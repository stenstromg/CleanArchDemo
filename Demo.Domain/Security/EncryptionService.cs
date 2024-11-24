using System.Security.Cryptography;
using System.Text;

namespace Demo.Domain.Security
{
    public class EncryptionService
    {

        #region Encryption Constant Parameters
        private static string SaltValue         = "@(%U^Kd)(876fgVB7_^Fr54e)#96"; // can be any string
        private static string HashAlgorithm     = "MD5";                          // can be "MD5"
        private static int PasswordIterations   = 2;                              // can be any number
        private static string InitVector        = "@1B2wWD4h5F%g7H8";             // must be 16 bytes
        private static int KeySize              = 256;                            // can be 192 or 128
        private static string PassPhrase        = "@(*UJ:myHIPU)9u0y6wyqF$%";
        #endregion

        #region c-tor
        /// <summary>
        /// Instantiating a new instance of the Encryption class allows the consumer to provide 
        /// specific encryption paramters, otherwise default static values will be used.
        /// </summary>
        /// <param name="saltValue"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="passwordIterations"></param>
        /// <param name="initVector"></param>
        /// <param name="keySize"></param>
        /// <param name="passPhrase"></param>
        public EncryptionService(string saltValue, HashAlgorithm hashAlgorithm, int passwordIterations, string initVector, KeySizes keySize, string passPhrase)
        {
            SaltValue           = saltValue;
            HashAlgorithm       = hashAlgorithm.ToString();
            PasswordIterations  = passwordIterations;

            // Check if string is 16 bytes long
            var encoding = new ASCIIEncoding();
            var bytes = encoding.GetBytes(initVector);
            if (bytes.Length != 16)
            {
                throw new Exception("Init vector must be 16 bytes long");
            }

            InitVector = initVector;
            KeySize = Convert.ToInt32(keySize);
            PassPhrase = passPhrase;
        }

        #endregion c-tor

        #region public static methods

        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm and returns a 
        /// base64-encoded result.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be encrypted.
        /// </param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            return Encrypt(plainText, SaltValue, HashAlgorithm, PasswordIterations, InitVector, KeySize);
        }

        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-formatted ciphertext value.
        /// </param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            return Decrypt(cipherText, SaltValue, HashAlgorithm, PasswordIterations, InitVector, KeySize);
        }

        /// <summary>
        /// Gets a flag indicating whether the plainText argument, after encryption, is the same as 
        /// the cipherText argument. Allows a password to be verified without decrypting the 
        /// encrypted password.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="cipherText">Previously encrypted value</param>
        /// <returns></returns>
        public static Boolean AreEqual(String plainText, String cipherText)
        {
            String cipherToCompare = Encrypt(plainText);
            return (cipherToCompare == cipherText);
        }

        #endregion public static methods

        #region private static methods

        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be encrypted.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hash algorithm used to generate password. Allowed values are: "MD5" and
        /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be 
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Encrypted value formatted as a base64-encoded string.
        /// </returns>
        private static string Encrypt(string plainText,
                                     string saltValue,
                                     string hashAlgorithm,
                                     int passwordIterations,
                                     string initVector,
                                     int keySize)
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            PassPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            byte[] cipherTextBytes;

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                // Start encrypting.
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream into a byte array.
                cipherTextBytes = memoryStream.ToArray();
            }


            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-formatted ciphertext value.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hash algorithm used to generate password. Allowed values are: "MD5" and
        /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        /// <remarks>
        /// Most of the logic in this function is similar to the Encrypt
        /// logic. In order for decryption to work, all parameters of this function
        /// - except cipherText value - must match the corresponding parameters of
        /// the Encrypt function which was called to generate the
        /// ciphertext.
        /// </remarks>
        private static string Decrypt(string cipherText,
                                     string saltValue,
                                     string hashAlgorithm,
                                     int passwordIterations,
                                     string initVector,
                                     int keySize)
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            PassPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            byte[] plainTextBytes;
            int decryptedByteCount;

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {

                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold ciphertext;
                // plaintext is never longer than ciphertext.
                plainTextBytes = new byte[cipherTextBytes.Length];

                // Start decrypting.
                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            }

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            // Return decrypted string.   
            return plainText;
        }

        #endregion private static methods

    }
}
