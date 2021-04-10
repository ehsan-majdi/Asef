using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Asefian.Common.Security
{
    public class PasswordUtility
    {
        private const string KEY = "2b7q8!@#";

        public static string Encrypt(string input)
        {
            return (string.IsNullOrEmpty(input)) ? "null" : InternalEncrypt(input, KEY);
        }

        private static string Encrypt(string input, string uv)
        {
            return InternalEncrypt(input, uv);
        }

        public static string EncryptPassword(string username, string password)
        {
            var UVKey = username.Substring(0, 3).ToCharArray();
            string UV = ((int)UVKey[0] * (int)UVKey[1] * (int)UVKey[2]).ToString();
            return InternalEncrypt(password.Replace(" ", "+"), UV);
        }

        public static string Decrypt(string input)
        {
            try
            {
                return Decrypt(input, KEY);
            }
            catch
            {
                return "";
            }
        }

        private static byte[] Encrypt(byte[] clearData, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        private static string InternalEncrypt(string clearText, string password)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            // ReSharper disable CSharpWarnings::CS0612
            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            // ReSharper restore CSharpWarnings::CS0612
            return Convert.ToBase64String(encryptedData);
        }

        private static byte[] Decrypt(byte[] cipherData, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

        private static string DecryptPassword(string username, string password)
        {
            var UVKey = username.Substring(0, 3).ToCharArray();
            string UV = ((int)UVKey[0] * (int)UVKey[1] * (int)UVKey[2]).ToString();
            return Decrypt(password, UV);
        }

        private static string Decrypt(string cipherText, string password)
        {
            byte[] cipherBytes = null;
            try
            {
                cipherBytes = Convert.FromBase64String(cipherText.Replace(" ", "+"));
            }
            catch
            {
                try
                {
                    cipherBytes = Convert.FromBase64String(cipherText.Replace(" ", "+") + "+");
                }
                catch
                {
                    cipherBytes = Convert.FromBase64String(cipherText.Replace(" ", "+") + "=");
                }

            }
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            // ReSharper disable CSharpWarnings::CS0612
            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            // ReSharper restore CSharpWarnings::CS0612
            return Encoding.Unicode.GetString(decryptedData);
        }
    }
}
