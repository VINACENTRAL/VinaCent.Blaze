using VinaCent.Blaze.Debugging;

namespace VinaCent.Blaze
{
    public class BlazeConsts
    {
        public const string LocalizationSourceName = "Blaze";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "073b1ffb657e47cbad587cb425760834";
    }
}
