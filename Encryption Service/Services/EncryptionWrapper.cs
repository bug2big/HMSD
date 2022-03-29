using System.Security.Cryptography;

namespace Encryption_Service.Services
{
    public class EncryptionWrapper
    {
        private byte[]? Key;
        private byte[]? IV;

        public void RotateKey()
        { 
            GenerateKey(true);
        }

        public string? Encrypt(string plainText) 
        {
            using (Aes aesAlg = Aes.Create())
            {
                if ((Key == null || Key.Any()) && (IV == null || IV.Any())) 
                {
                    GenerateKey(true);
                }
                
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        return msEncrypt?.ToString();
                    }
                }
            }
        }

        public string Decrypt(string encryptedText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                if ((Key == null || Key.Any()) && (IV == null || IV.Any()))
                {
                    GenerateKey(true);
                }

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                var encryptedArray = Convert.FromBase64String(encryptedText);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedArray))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private void GenerateKey(bool generateNew = false)
        {
            if (!generateNew)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.GenerateKey();
                    aesAlg.GenerateIV();

                    Key = aesAlg.Key;
                    IV = aesAlg.IV;
                }
            }
        }
    }
}
