using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using VinaCent.Blaze.Helpers.Encryptions;

namespace VinaCent.Blaze.Encryptions
{
    public class AESHelper : IAESHelper
    {
        private readonly byte[] _salt;

        private readonly string defaultPassword;

        private readonly IConfiguration _configuration;

        public AESHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            defaultPassword = _configuration.GetValue("StringEncryption:DefaultPassword", "hCsQMM6uDDxVsBnKpcFzLYh2");
            var passPhrase = _configuration.GetValue("StringEncryption:DefaultPassPhrase", "I_Love_Raiden_Shogun");
            _salt = Encoding.ASCII.GetBytes(passPhrase);
        }

        /// <summary>
        /// Top level method, another method using encrypt must be start from this method or its children
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Encrypt(string clearText, string password = "")
        {
            if (password.IsNullOrEmpty())
            {
                password = defaultPassword;
            }

            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(password, _salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
            return clearText;
        }

        /// <summary>
        /// Top level method, another method using decrypt must be start from this method or its children
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, string password = "")
        {
            if (password.IsNullOrEmpty())
            {
                password = defaultPassword;
            }

            var cipherBytes = Convert.FromBase64String(cipherText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(password, _salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using MemoryStream ms = new();
                using (CryptoStream cs = new(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
            return cipherText;
        }

        public string Encrypt<T>(T clearObject, string password = "")
        {
            var jsonString = JsonConvert.SerializeObject(clearObject);
            return Encrypt(jsonString, password);
        }

        public T Decrypt<T>(string cipherText, string password = "")
        {
            var jsonString = Decrypt(cipherText, password);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
