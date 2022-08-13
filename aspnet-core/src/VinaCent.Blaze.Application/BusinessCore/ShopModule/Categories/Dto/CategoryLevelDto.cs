using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using VinaCent.Blaze.BusinessCore.Shop;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;

[AutoMapFrom(typeof(Category))]
public class CategoryLevelDto : EntityDto, IPassivable
{
    /// <summary>s
    /// The parent id to identify the parent category.
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// The category title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The meta title to be used for browser title and SEO.
    /// </summary>
    public string MetaTitle { get; set; }

    /// <summary>
    /// The category slug to form the URL.
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    /// The column used to store the category details.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// The column used to notify current category is visible or not
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Level of this category, start from 0 (Zero) as Parent
    /// </summary>
    public virtual int Level { get; set; }
}