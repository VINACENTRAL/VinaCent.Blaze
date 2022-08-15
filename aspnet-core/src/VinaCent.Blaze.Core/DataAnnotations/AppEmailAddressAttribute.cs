using Abp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class AppEmailAddressAttribute : DataTypeAttribute
    {
        public AppEmailAddressAttribute()
            : base(DataType.EmailAddress)
        {
            // Set DefaultErrorMessage not ErrorMessage, allowing user to set
            // ErrorMessageResourceType and ErrorMessageResourceName to use localized messages.
            ErrorMessage = LKConstants.EmailFormatNotCorrect;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is not string valueAsString)
            {
                return false;
            }

            // only return true if there is only 1 '@' character
            // and it is neither the first nor the last character
            int index = valueAsString.IndexOf('@');

            return
                index > 0 &&
                index != valueAsString.Length - 1 &&
                index == valueAsString.LastIndexOf('@');
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                LocalizationHelper.GetString(BlazeConsts.LocalizationSourceName, ErrorMessageString), name);
        }
    }
}
