using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Aaa.Common
{
    [Serializable]
    public class EncryptionHelper
    {
        /// <summary>
        /// Encrypts the inbound string using the provided key and initialization vector
        /// </summary>
        /// <param name="keyBase64String">Base64String containing the encryption key</param>
        /// <param name="ivBase64String">Base64String containing the initialization vector</param>
        /// <param name="plainText">Unencrypted text to be encrypted</param>
        /// <returns>Encrypted byte[] containing the cipher data</returns>       
        public static byte[] Encrypt(string keyBase64String, string ivBase64String, string plainText)
        {
            try
            {
                // Create an AesManaged object with the specified key and IV.
                using (AesManaged aesAlg = new AesManaged())
                {
                    // Set encryption key
                    aesAlg.Key = System.Convert.FromBase64String(keyBase64String);
                    // Set initialization vector
                    aesAlg.IV = System.Convert.FromBase64String(ivBase64String);

                    // Create a decrytor to perform the stream transform.
                    using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                    {
                        // Create the streams used for encryption.
                        using (MemoryStream msEncrypt = new MemoryStream())
                        {
                            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                                {
                                    //Write all data to the stream.
                                    swEncrypt.Write(plainText);
                                    plainText = null;
                                }
                                return msEncrypt.ToArray();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Aaa.Common.EncryptionHelper.Encrypt experienced an issue. Exception: " + ex.Message, ex);
            }
        }        

        /// <summary>
        /// Decrypts the inbound encrypted cipher using the provided key and initialization vector
        /// </summary>        
        /// <param name="keyBase64String">Base64String containing the encryption key</param>
        /// <param name="ivBase64String">Base64String containing the initialization vector</param>
        /// <param name="cipherText">Encrypted byte[] containing the data to decrypt</param>
        /// <returns>Decrypted text</returns>        
        public static string Decrypt(string keyBase64String, string ivBase64String, byte[] cipherText)
        {
            try
            {
                // Create an AesManaged object with the specified key and IV.
                using (AesManaged aesAlg = new AesManaged())
                {
                    // Set encryption key
                    aesAlg.Key = System.Convert.FromBase64String(keyBase64String);
                    // Set initialization vector
                    aesAlg.IV = System.Convert.FromBase64String(ivBase64String);

                    // Create a decrytor to perform the stream transform.
                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        // Create the streams used for decryption.
                        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                        {
                            cipherText = null;
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                {
                                    // Read the decrypted bytes from the decrypting stream                               
                                    return srDecrypt.ReadToEnd();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Aaa.Common.EncryptionHelper.Decrypt experienced an issue. Exception: " + ex.Message, ex);
            }
        }       
    }
}
