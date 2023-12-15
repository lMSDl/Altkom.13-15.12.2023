using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Services.InFile.Encryption
{
    internal class SymmetricEncryption
    {
        private byte[] _salt;

        public SymmetricEncryption(string salt)
        {
            _salt = Encoding.Unicode.GetBytes(salt);
        }

        private AesManaged _algorithm = new AesManaged();

        public byte[] Encrypt(string stringToEncrypt, string password)
        {
            return Encrypt(Encoding.Unicode.GetBytes(stringToEncrypt), password);
        }

        public byte[] Encrypt(byte[] byteToEncypt, string password)
        {
            Rfc2898DeriveBytes passwordHash = GeneratePasswordHash(password);
            byte[] key = GenerateKey(passwordHash);
            byte[] iv = GenerateIV(passwordHash);
            return Transform(_algorithm.CreateEncryptor(key, iv), byteToEncypt);
        }

        private byte[] Transform(ICryptoTransform encryptor, byte[] bytes)
        {
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();

            return memoryStream.ToArray();
        }


        public string DecryptToString(byte[] byteToDecrypt, string password)
        {
            return Encoding.Unicode.GetString(Decrypt(byteToDecrypt, password));
        }

        public byte[] Decrypt(byte[] byteToDecrypt, string password)
        {
            Rfc2898DeriveBytes passwordHash = GeneratePasswordHash(password);
            byte[] key = GenerateKey(passwordHash);
            byte[] iv = GenerateIV(passwordHash);
            return Transform(_algorithm.CreateDecryptor(key, iv), byteToDecrypt);
        }



        private byte[] GenerateIV(Rfc2898DeriveBytes passwordHash)
        {
            return passwordHash.GetBytes(_algorithm.BlockSize / 8);
        }

        private byte[] GenerateKey(Rfc2898DeriveBytes passwordHash)
        {
            return passwordHash.GetBytes(_algorithm.KeySize / 8);
        }

        private Rfc2898DeriveBytes GeneratePasswordHash(string password)
        {
            return new Rfc2898DeriveBytes(password, _salt);
        }

    }
}
