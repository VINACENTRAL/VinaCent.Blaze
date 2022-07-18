using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using System.Collections.Generic;
using VinaCent.Blaze.BusinessCore.Shop.Categories;
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
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ParentCategory)]
    public int? ParentId { get; set; }

    /// <summary>
    /// The category slug to form the URL.
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Slug)]
    [AppRegex(RegexLib.SlugRegex)]
    public string Slug { get; set; }

    /// <summary>
    /// The column used to notify current category is visible or not
    /// </summary>
    [AppRequired]
    [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IsActive)]
    public bool IsActive { get; set; }

    /// <summary>
    /// Level of this category
    /// </summary>
    public int Level { get; set; }

    public List<CategoryTranslationDto> Translations { get; set; }
}