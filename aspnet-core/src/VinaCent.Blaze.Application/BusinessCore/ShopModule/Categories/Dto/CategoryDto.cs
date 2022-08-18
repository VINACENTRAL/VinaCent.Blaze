using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using System.Collections.Generic;
using VinaCent.Blaze.BusinessCore.Shop.Categories;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;

[AutoMapFrom(typeof(Category))]
public class CategoryDto : AuditedEntityDto, IPassivable
{
    /// <summary>
    /// The parent id to identify the parent category.
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// The category slug to form the URL.
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    /// The column used to notify current category is visible or not
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Level of this category
    /// </summary>
    public int Level { get; set; }

    public virtual CategoryDto ParentCategory { get; set; }

    public List<CategoryTranslationDto> Translations { get; set; }
}