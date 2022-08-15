using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using VinaCent.Blaze.BusinessCore.Shop;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;

[AutoMapFrom(typeof(CategoryDto))]
[AutoMap(typeof(Category))]
public class UpdateCategoryDto : EntityDto, IPassivable
{
    /// <summary>
    /// The parent id to identify the parent category.
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ParentCategory)]
    public int? ParentId { get; set; }

    /// <summary>
    /// The category title.
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Slug)]
    public string Title { get; set; }

    /// <summary>
    /// The meta title to be used for browser title and SEO.
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.MetaTitle)]
    public string MetaTitle { get; set; }

    /// <summary>
    /// The category slug to form the URL.
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Slug)]
    [AppRegex(RegexLib.SlugRegex)]
    public string Slug { get; set; }

    /// <summary>
    /// The column used to store the category details.
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
    public string Content { get; set; }

    /// <summary>
    /// The column used to notify current category is visible or not
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IsActive)]
    public bool IsActive { get; set; }
}