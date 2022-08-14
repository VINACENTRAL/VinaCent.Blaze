namespace VinaCent.Blaze.Common
{
    // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
    public class RegexLib
    {
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";
        public const string EmailChecker = @"^[\w\-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        public const string EmailListSeprateByCommaChecker = @"(([\w\-\.]+@([\w-]+\.)+[\w-]{2,4}),? ?)+";
        public const string SlugRegex = @"^[a-z0-9]+(?:\-[a-z0-9]+)*$";
    }
}
