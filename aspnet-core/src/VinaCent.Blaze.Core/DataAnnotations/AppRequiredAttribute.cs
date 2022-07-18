using Abp.Localization;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.DataAnnotations
{
    public class AppRequiredAttribute : RequiredAttribute
    {
        public AppRequiredAttribute()
        {
            ErrorMessage = LKConstants.Field_X_IsRequired;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                LocalizationHelper.GetString(BlazeConsts.LocalizationSourceName, ErrorMessageString), name);
        }
    }
}
