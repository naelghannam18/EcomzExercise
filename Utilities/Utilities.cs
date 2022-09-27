using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace EcomzExercise.Utilities
{
    /// <summary>
    /// Utility Class For Encryption And Hashing
    /// </summary>
    public class Utilities
    {

        // Encryption Was Not Used in This Project Because There is no Front End And All work is Local
        #region Encryption

        // Used To Encrypt Data 
        public static string EncryptByAES(string input)
        {
            string EncryptionKey = "5v8y/B?E(H+MbQeThWmZq4t7w9z$C&F)"; // TODO: Securing Keys
            using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Mode = CipherMode.CBC;
                rijndaelManaged.Padding = PaddingMode.PKCS7;
                rijndaelManaged.FeedbackSize = 128;

                rijndaelManaged.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                rijndaelManaged.IV = Encoding.UTF8.GetBytes("eShVmYq3t6w9z$C&"); // TODO: Securing IVs

                ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        byte[] bytes = msEncrypt.ToArray();
                        return Convert.ToBase64String(bytes);
                    }
                }
            }
        }

        // Used To Decrypt Data
        public static string DecryptByAES(string input)
        {
            string EncryptionKey = "5v8y/B?E(H+MbQeThWmZq4t7w9z$C&F)"; // TODO: Secure Keys
            var buffer = Convert.FromBase64String(input);
            using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Mode = CipherMode.CBC;
                rijndaelManaged.Padding = PaddingMode.PKCS7;
                rijndaelManaged.FeedbackSize = 128;

                rijndaelManaged.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                rijndaelManaged.IV = Encoding.UTF8.GetBytes("eShVmYq3t6w9z$C&"); // TODO:Secure IV

                ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using (MemoryStream msEncrypt = new MemoryStream(buffer))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                        {
                            return srEncrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion

        // Hashing Using Bcrypt Package
        #region Hashing

        public static string HashText(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }
        public static bool ValidateHash(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }

        #endregion

    }
}
