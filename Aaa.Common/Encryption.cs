namespace Aaa.Common
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public static class Encryption
    {
        public static byte[] Encrypt(string textToEncrypt)
        {
            //TODO - store Key/IV in config
            using (var aesManaged = new AesManaged())
            {
                return EncryptStringToBytes(textToEncrypt, aesManaged.Key, aesManaged.IV);
            }
        }

        public static string Decrypt(byte[] encryptedData)
        {
            using (var aesManaged = new AesManaged())
            {
                //TODO - store Key/IV in config
                return DecryptStringFromBytes(encryptedData, aesManaged.Key, aesManaged.IV);
            }
        }

        public static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            using (var aesAlg = new AesManaged())
            {

                //TODO - use the key/iv
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.KeySize = 128;          // in bits
                aesAlg.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aesAlg.IV = new byte[128 / 8];   // AES needs a 16-byte IV
                  
                // Create a decrytor to perform the stream transform.
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    // Create the streams used for encryption.
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
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

        public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Create an AesManaged object with the specified key and IV.
            using (var aesAlg = new AesManaged())
            {
                //TODO - use the key/iv
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.KeySize = 128;          // in bits
                aesAlg.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aesAlg.IV = new byte[128 / 8];   // AES needs a 16-byte IV

                // Create a decrytor to perform the stream transform.
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        cipherText = null;
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream                               
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}
