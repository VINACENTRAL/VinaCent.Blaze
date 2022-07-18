using Abp.Configuration;
using VinaCent.Blaze.Configuration;

namespace VinaCent.Blaze.Utilities
{
    public static class ConfigurationUtils
    {
        public static long GetAllowMaxFileSizeInMB(this ISettingManager setting)
        {
            return setting.GetSettingValue<long>(AppSettingNames.AllowedMaxFileSizeInMB);
        }

        public static long GetAllowMaxFileSizeInKB(this ISettingManager setting)
        {
            return setting.GetAllowMaxFileSizeInMB() * 1024;
        }

        public static long GetAllowMaxFileSizeInBytes(this ISettingManager setting)
        {
            return setting.GetAllowMaxFileSizeInKB() * 1024;
        }
        public static long GetAllowMaxFileSizeInGB(this ISettingManager setting)
        {
            return setting.GetAllowMaxFileSizeInMB() / 1024;
        }


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
