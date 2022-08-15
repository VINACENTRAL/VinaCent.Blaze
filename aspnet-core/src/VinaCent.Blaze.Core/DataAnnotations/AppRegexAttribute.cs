using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VinaCent.Blaze.DataAnnotations
{
    public class AppRegexAttribute : RegularExpressionAttribute
    {
        public AppRegexAttribute([NotNull] string pattern) : base(pattern)
        {
            ErrorMessage = LKConstants.FormIsNotValidMessage;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                LocalizationHelper.GetString(BlazeConsts.LocalizationSourceName, ErrorMessageString), name);
        }
    }
}
