using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace EcomzExercise.Utilities
{
    public class Utilities
    {
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
        public static string HashText(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }
        public static bool ValidateHash(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
        public static string GenerateSignature(string timestamp, string method, string url, string body, string appSecret)
        {
            return GetHMACInHex(appSecret, timestamp + method + url + body);
        }
        public static string GetHMACInHex(string key, string data)
        {
            var hmacKey = Encoding.UTF8.GetBytes(key);
            var dataBytes = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA256(hmacKey))
            {
                var sig = hmac.ComputeHash(dataBytes);
                return ByteToHexString(sig);
            }
        }
        static string ByteToHexString(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            int b;
            for (int i = 0; i < bytes.Length; i++)
            {
                b = bytes[i] >> 4;
                c[i * 2] = (char)(87 + b + (((b - 10) >> 31) & -39));
                b = bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(87 + b + (((b - 10) >> 31) & -39));
            }
            return new string(c);
        }
    }
}
