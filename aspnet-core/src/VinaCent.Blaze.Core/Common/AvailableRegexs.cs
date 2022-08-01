namespace VinaCent.Blaze.Common
{
    public class AvailableRegexs
    {
        public const string EmailChecker = @"^[\w\-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        public const string EmailListSeprateByCommaChecker = @"(([\w\-\.]+@([\w-]+\.)+[\w-]{2,4}),? ?)+";
    }
}
