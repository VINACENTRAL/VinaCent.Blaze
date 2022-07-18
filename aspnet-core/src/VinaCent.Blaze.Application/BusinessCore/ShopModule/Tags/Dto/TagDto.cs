using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Localization;
using VinaCent.Blaze.BusinessCore.Shop.Tags;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Tags.Dto;

[AutoMap(typeof(Tag))]
public class TagDto : EntityDto<Guid>
{
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Title)]
    public string Title { get; set; }

    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.MetaTitle)]
    public string MetaTitle { get; set; }

    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Slug)]
    [AppRegex(RegexLib.SlugRegex)]
    public string Slug { get; set; }

    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
    public string Content { get; set; }
}