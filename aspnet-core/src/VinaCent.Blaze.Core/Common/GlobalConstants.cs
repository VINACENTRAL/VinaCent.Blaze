namespace VinaCent.Blaze.Common
{
    public class GlobalConstants
    {

        public static class SignInOptions
        {
            public const bool RequireConfirmedEmail = true;
            public const bool RequireConfirmedPhoneNumber = false;
            public const bool RequireConfirmedAccount = false;
        }

        public static class UserOptions
        {
            public const string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            public const bool RequireUniqueEmail = true;
        }
    }
}
