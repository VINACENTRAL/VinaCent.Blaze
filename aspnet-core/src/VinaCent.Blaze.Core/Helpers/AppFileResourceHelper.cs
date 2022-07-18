namespace VinaCent.Blaze.Helpers
{
    public static class AppFileResourceHelper
    {
        public const string ResourcePathPrefix = "resources";

        public static string ResourceFullName(this string input)
        {
            input = input.Replace("\\", "/");
            if (input.StartsWith("/"))
                input = input.TrimStart('/');
            if (input.StartsWith(ResourcePathPrefix + "/"))
            {
                var firstIndexOfPathSeparator = input.IndexOf('/');
                if (firstIndexOfPathSeparator >= 0)
                {
                    input = input.Substring(firstIndexOfPathSeparator + 1);
                }
            }

            return input;
        }

        public static string ToResourcePath(this string fullname)
        {
            return StringHelper.TrueCombine("/resources", fullname);
        }
    }
}
