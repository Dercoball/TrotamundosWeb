using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TrotamundosNetCore.Clases.Util
{
    public static class Encrypt
    {
        private static string Password = "m0n4pl1cBAZ.10";
        private static string SaltValue = "c9a5d2f21f00469ff48a60fe5311b2ff";
        private static string hashAlgorithm = "MD5";
        private static int PasswordIterations = 2;
        private static string InitialVector = "bQBhAHIAaQANAAoA";
        private static int KeySize = 256;

        public static string Encriptar(string defPlainText)
        {

            try
            {
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(SaltValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(defPlainText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(Password, saltValueBytes, hashAlgorithm, PasswordIterations);

                byte[] keyBytes = password.GetBytes(KeySize / 8);

                Aes symmetricKey = Aes.Create();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, InitialVectorBytes);

                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                string cipherText = Convert.ToBase64String(cipherTextBytes);

                return cipherText;
            }
            catch
            {
                Console.WriteLine("The typed information is wrong. Please, check it.");
                return null;
            }

        }

        public static string Desencriptar(string defPlainText)
        {

            try
            {
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(SaltValue);

                byte[] cipherTextBytes = Convert.FromBase64String(defPlainText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(Password, saltValueBytes, hashAlgorithm, PasswordIterations);

                byte[] keyBytes = password.GetBytes(KeySize / 8);

                Aes symmetricKey = Aes.Create();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, InitialVectorBytes);

                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] decrypted = new byte[cipherTextBytes.Length];
                int read = 0;
                int l = 0;

                //Se retira linea de codigo
                //int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                do
                {
                    read = cryptoStream.Read(decrypted, l, decrypted.Length - l);
                    l += read;
                } while (read != 0);

                memoryStream.Close();
                cryptoStream.Close();

                if (l > 0)
                {
                    byte[] t = new byte[l];
                    Array.Copy(decrypted, t, l);

                    string plainText = Encoding.UTF8.GetString(t, 0, t.Length);

                    return plainText;
                }

                return null;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                return null;
            }

        }
    }
}
