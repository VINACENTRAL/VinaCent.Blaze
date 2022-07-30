using Abp.Dependency;

namespace VinaCent.Blaze.Helpers.Encryptions
{
    public interface IAESHelper : ITransientDependency
    {
        public string Encrypt(string clearText, string password = "");
        public string Decrypt(string cipherText, string password = "");
        public string Encrypt<T>(T clearObject, string password = "");
        public T Decrypt<T>(string cipherText, string password = "");
    }
}
