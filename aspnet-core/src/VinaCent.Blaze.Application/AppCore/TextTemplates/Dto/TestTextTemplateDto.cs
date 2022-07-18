using Abp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.TextTemplates.Dto;

public class TestTextTemplateDto
{
    [AppRequired]
    public Guid TextTemplateId { get; set; }

    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Parameters)]
    public string Parameters { get; set; }

    [AppRegex(RegexLib.EmailChecker)]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Receiver)]
    public string Receiver { get; set; }
}
