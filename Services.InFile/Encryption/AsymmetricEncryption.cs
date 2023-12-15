using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.InFile.Encryption
{
    internal class AsymmetricEncryption
    {
        public byte[] Encrypt(string stringToEncrypt, string certName)
        {
            return Encrypt(Encoding.Unicode.GetBytes(stringToEncrypt), certName);
        }

        public byte[] Encrypt(byte[] byteToEncypt, string certName)
        {
            X509Certificate2? cert = GetCert(certName);
            AsymmetricAlgorithm key = cert.PublicKey.Key;

            using AesManaged algorithm = new AesManaged();
            using MemoryStream memoryStream= new MemoryStream();
            ICryptoTransform encryptor = algorithm.CreateEncryptor();

            RSAPKCS1KeyExchangeFormatter formatter = new RSAPKCS1KeyExchangeFormatter(key);
            byte[] encyptedKey = formatter.CreateKeyExchange(algorithm.Key, algorithm.GetType());


            var keyLength = BitConverter.GetBytes(encyptedKey.Length);
            var ivLength = BitConverter.GetBytes(algorithm.IV.Length);

            memoryStream.Write(keyLength, 0, 4);
            memoryStream.Write(ivLength, 0, 4);
            memoryStream.Write(encyptedKey, 0, encyptedKey.Length);
            memoryStream.Write(algorithm.IV, 0, algorithm.IV.Length);

            using CryptoStream encrypt = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            
                encrypt.Write(byteToEncypt, 0, byteToEncypt.Length);
                encrypt.FlushFinalBlock();

                return memoryStream.ToArray();
        }


        public string DecryptToString(byte[] bytesToDecrypt, string password)
        {
            return Encoding.Unicode.GetString(Decrypt(bytesToDecrypt, password));
        }
        public byte[] Decrypt(byte[] bytesToDecrypt, string certName)
        {
            var cert = GetCert(certName);
            var provider = cert.GetRSAPrivateKey();
            using (var algorithm = new AesManaged())
            using (var inStream = new MemoryStream(bytesToDecrypt))
            {
                var keyLength = new byte[4];
                var ivLength = new byte[4];
                inStream.Seek(0, SeekOrigin.Begin);

                inStream.Read(keyLength, 0, keyLength.Length);
                inStream.Read(ivLength, 0, ivLength.Length);
                var keyLengthInt = BitConverter.ToInt32(keyLength, 0);
                var ivLengthInt = BitConverter.ToInt32(ivLength, 0);

                var dataStartPosition = keyLengthInt + ivLengthInt + keyLength.Length + ivLength.Length;
                var dataSize = (int)inStream.Length - dataStartPosition;

                var key = new byte[keyLengthInt];
                var iv = new byte[ivLengthInt];
                var data = new byte[dataSize];

                inStream.Read(key, 0, keyLengthInt);
                inStream.Read(iv, 0, ivLengthInt);
                inStream.Read(data, 0, dataSize);

                var decryptedKey = provider.Decrypt(key, RSAEncryptionPadding.Pkcs1);

                using (var outStream = new MemoryStream())
                using (var decryptor = algorithm.CreateDecryptor(decryptedKey, iv))
                using (var decrypStream = new CryptoStream(outStream, decryptor, CryptoStreamMode.Write))
                {
                    decrypStream.Write(data, 0, data.Length);
                    decrypStream.FlushFinalBlock();

                    return outStream.ToArray();
                }

            }
        }


        private X509Certificate2? GetCert(string certName)
        {
            using X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            foreach (var cert in store.Certificates)
            {
                if (cert.SubjectName.Name == certName)
                    return cert;
            }
            return null;
        }

    }
}
