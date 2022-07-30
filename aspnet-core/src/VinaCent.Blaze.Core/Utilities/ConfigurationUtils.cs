using Microsoft.Extensions.Configuration;
using VinaCent.Blaze.Encryptions;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Utilities
{
    public static class ConfigurationUtils
    {
        //public static string GetConnectionString(this IConfiguration configuration, string name, string password)
        //{
        //    var connectionString = configuration.GetConnectionString(name);
        //    if (connectionString == null) return "";
        //    return AESHelper.Decrypt(connectionString, password);
        //}

        //public static string GetEncryptedConnectionString(this IConfiguration configuration, string name)
        //{
        //    var password = configuration.GetValue<string>("StringEncryption:DefaultPassPhrase");
        //    return configuration.GetConnectionString(name, password);
        //}
    }
}
