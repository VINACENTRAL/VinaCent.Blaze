using Abp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.Common;

namespace VinaCent.Blaze.AppCore.TextTemplates.Dto;

public class TestTextTemplateDto
{
    [Required]
    public Guid TextTemplateId { get; set; }

    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Parameters)]
    public string Parameters { get; set; }

    [RegularExpression(RegexLib.EmailChecker,
        ErrorMessage = "Invalid. Ex: email1@vinacent.com")]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Receiver)]
    public string Receiver { get; set; }
}
