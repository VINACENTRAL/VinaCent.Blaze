using Abp.Localization;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.DataAnnotations
{
    public class AppStringLengthAttribute : StringLengthAttribute
    {
        public AppStringLengthAttribute(int maximumLength) : base(maximumLength)
        {
            ErrorMessage = LKConstants.TheField_X_MustBeAStringWithAMaximumLengthOf_Y;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                LocalizationHelper.GetString(BlazeConsts.LocalizationSourceName, ErrorMessageString), name,
                MaximumLength);
        }
    }
}
