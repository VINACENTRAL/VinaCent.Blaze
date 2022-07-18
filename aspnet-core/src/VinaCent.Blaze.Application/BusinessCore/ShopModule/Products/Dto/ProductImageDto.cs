using System;
using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.BusinessCore.ShopModule.Products.Dto;

public class ProductImageDto : EntityDto<Guid>
{
    /// <summary>
    /// The category id to identify the category.
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// Link to resource 
    /// </summary>
    public string Uri { get; set; }

    /// <summary>
    /// Is feature image. Use to show in home page
    /// </summary>
    public bool IsFeature { get; set; }
}