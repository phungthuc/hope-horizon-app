using System;
using System.Text;

namespace HopeHorizon.Scripts.PlayerPrefs
{
    public class EncryptionUtils
    {
        public static string Encrypt(string plainText)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            string encryptedData = Convert.ToBase64String(data);
            return encryptedData;
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] data = Convert.FromBase64String(encryptedText);
            string plainText = Encoding.UTF8.GetString(data);
            return plainText;
        }
    }
}
