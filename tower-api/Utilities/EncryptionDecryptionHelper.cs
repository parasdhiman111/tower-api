using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace tower_api.Utilities
{
    public class EncryptionDecryptionHelper
    {
        private const string Key = "B374A26A71490437AA024E4FADD5B497"; // 32-character (256-bit) key
        private const string IV = "7E892875A52C59A3"; // 16-character (128-bit) IV

        public static string Decrypt(string encryptedText)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);
                aesAlg.Padding = PaddingMode.PKCS7;

                Console.WriteLine($"Key: {BitConverter.ToString(aesAlg.Key)}");
                Console.WriteLine($"IV: {BitConverter.ToString(aesAlg.IV)}");

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    var decryptedData = srDecrypt.ReadToEnd();
                    Console.WriteLine($"Decrypted Data: {decryptedData}");
                    return decryptedData;
                }
            }
        }

    }

}

