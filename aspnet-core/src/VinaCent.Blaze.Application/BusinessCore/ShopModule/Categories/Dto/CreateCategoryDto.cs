using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.BusinessCore.Shop;
using VinaCent.Blaze.Common;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;

[AutoMapTo(typeof(Category))]
public class CreateCategoryDto : IPassivable
{
    /// <summary>
    /// The parent id to identify the parent category.
    /// </summary>
    [Required]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ParentCategory)]
    public int? ParentId { get; set; }

    /// <summary>
    /// The category title.
    /// </summary>
    [Required]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Title)]
    public string Title { get; set; }

    /// <summary>
    /// The meta title to be used for browser title and SEO.
    /// </summary>
    [Required]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.MetaTitle)]
    public string MetaTitle { get; set; }

    /// <summary>
    /// The category slug to form the URL.
    /// </summary>
    [Required]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Slug)]
    [RegularExpression(RegexLib.SlugRegex)]
    public string Slug { get; set; }

    /// <summary>
    /// The column used to store the category details.
    /// </summary>
    [Required]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
    public string Content { get; set; }

    /// <summary>
    /// The column used to notify current category is visible or not
    /// </summary>
    [Required]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IsActive)]
    public bool IsActive { get; set; }
}